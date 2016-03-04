﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XGraph.Controls;
using XGraph.ViewModels;
using XGraphTestApp;

namespace XGraphTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.GraphView.DataContext = this.CreateTypeGraph();
            this.GraphView.SelectionChanged += GraphView_SelectionChanged;
            this.GraphView.SelectionMode = SelectionMode.Extended;
        }

        /// <summary>
        /// Creates the type graph.
        /// </summary>
        /// <returns></returns>
        public GraphViewModel CreateTypeGraph()
        {
            GraphViewModel lGraph = new GraphViewModel();
            NodeViewModel lNode0 = new TypeNodeViewModel(typeof(SampleClass));
            lGraph.AddNode(lNode0);

            NodeViewModel lNode1 = new TypeNodeViewModel(typeof(SampleClass1VeryTooMuchLong));
            lGraph.AddNode(lNode1);

            NodeViewModel lNode2 = new NodeViewModel();
            lNode2.DisplayString = "Empty node";
            lGraph.AddNode(lNode2);

            int i = 0;
            foreach (NodeViewModel lNode in lGraph.Nodes)
            {
                lNode.X = 300 * i;
                lNode.Y = 100 * i;
                i++;
            }

            ConnectionViewModel lConnectionViewModel = new ConnectionViewModel();
            lConnectionViewModel.Output = lGraph.Nodes.ElementAt(0).Ports.FirstOrDefault(pPort => pPort.Direction == PortDirection.Output);
            lConnectionViewModel.Input = lGraph.Nodes.ElementAt(1).Ports.FirstOrDefault(pPort => pPort.Direction == PortDirection.Input);
            lGraph.AddConnection(lConnectionViewModel);
            
            return lGraph;
        }

        /// <summary>
        /// Creates the graph.
        /// </summary>
        /// <param name="pNodeCount">The node count.</param>
        /// <returns>The graph view model.</returns>
        public GraphViewModel CreateRandomGraph(int pNodeCount)
        {
            GraphViewModel lGraph = new GraphViewModel();
            Random lRandom = new Random();

            for (int i = 0; i < pNodeCount; i++)
            {
                NodeViewModel lNode = this.CreateNode(string.Format("{0}", i), string.Format("NODE_{0}", i), lRandom.Next(1, 5), lRandom.Next(1, 3));
                lNode.X = 50 * i;
                lNode.Y = 100 * i;
                lGraph.AddNode(lNode);
            }

            return lGraph;
        }

        /// <summary>
        /// This method creates the node.
        /// </summary>
        /// <param name="pId">The identifier.</param>
        /// <param name="pTitle">The title.</param>
        /// <param name="pInputPort">The input port.</param>
        /// <param name="pOutputPort">The output port.</param>
        /// <returns>The initialized node</returns>
        public NodeViewModel CreateNode(string pId, string pTitle, int pInputPort, int pOutputPort)
        {
            NodeViewModel lNode = new NodeViewModel();
            lNode.DisplayString = pTitle;
            for (int i = 0; i < pInputPort; i++)
            {
                PortViewModel lPort = new PortViewModel();
                lPort.DisplayString = string.Format("IPort {0}", i);
                lPort.Direction = PortDirection.Input;

                lNode.Ports.Add(lPort);
            }

            for (int i = 0; i < pOutputPort; i++)
            {
                PortViewModel lPort = new PortViewModel();
                lPort.DisplayString = string.Format("OPort {0}", i);
                lPort.Direction = PortDirection.Output;

                lNode.Ports.Add(lPort);
            }
            return lNode;

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            GraphViewModel lRootViewModel = this.GraphView.DataContext as GraphViewModel;
            foreach (IGraphItemViewModel lItem in this.GraphView.SelectedItems.Cast<IGraphItemViewModel>().ToList())
            {
                ConnectionViewModel lConnection = lItem as ConnectionViewModel;
                if (lConnection != null)
                {
                    lRootViewModel.RemoveConnection(lConnection);
                }

                NodeViewModel lNode = lItem as NodeViewModel;
                if (lNode != null)
                {
                    lRootViewModel.RemoveNode(lNode);
                }
            }
        }

        private void IsActive_Button_Click(object sender, RoutedEventArgs e)
        {
            GraphViewModel lRootViewModel = this.GraphView.DataContext as GraphViewModel;
            NodeViewModel lSelectedItem = this.GraphView.SelectedItems.OfType<NodeViewModel>().FirstOrDefault();
            if (lSelectedItem != null)
            {
                lSelectedItem.IsActive = !lSelectedItem.IsActive;
            }

            //GraphViewModel lRootViewModel = this.GraphView.DataContext as GraphViewModel;
            //NodeViewModel lSelectedItem = this.GraphView.SelectedItems.OfType<NodeViewModel>().FirstOrDefault();
            //if (lSelectedItem != null)
            //{
            //    NodeView lNodeView = this.GraphView.GetContainerForViewModel<NodeViewModel, NodeView>(lSelectedItem);
            //    if (lNodeView != null)
            //    {
            //        lNodeView.Background = Brushes.Red;
            //    }
            //}
            
        }


        private void GraphView_SelectionChanged(object sender, SelectionChangedEventArgs pEventArgs)
        {
            NodeViewModel lSelectedItem = this.GraphView.SelectedItems.OfType<NodeViewModel>().FirstOrDefault();
            if (lSelectedItem != null)
            {
                this.mIsActiveButton.Content = "IsActive : " + lSelectedItem.DisplayString;
                this.mIsActiveButton.IsChecked = lSelectedItem.IsActive;
            }

            if (this.GraphView.SelectedItems.OfType<NodeViewModel>().Count() == 0)
            {
                this.mIsActiveButton.Content = "IsActive";
            }
        }

    }
}
