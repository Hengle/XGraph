﻿using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PropertyChanged;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XGraph.Annotations;

namespace XGraph.ViewModels
{
    /// <summary>
    /// This class represents the view model of a graph node.
    /// A node is composed by :
    ///     - Ports
    ///     - Connections between ports.
    /// </summary>
    /// <!-- NBY -->
    public class NodeViewModel : IGraphItemViewModel, IPositionable, INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeViewModel"/> class.
        /// </summary>
        public NodeViewModel()
        {
            this.Ports = new PortViewModelCollection(this);
        }

        #endregion // Constructors.

        #region Properties

        /// <summary>
        /// Gets the parent graph.
        /// </summary>
        public AGraphViewModel ParentGraph
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public virtual double X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y position of the node.
        /// </summary>
        public virtual double Y
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        public virtual bool IsSelected
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ports.
        /// </summary>
        public PortViewModelCollection Ports 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets or sets the display string.
        /// </summary>
        public virtual string DisplayString 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public virtual ImageSource Icon
        {
            get
            {
                return Themes.ThemeManager.Instance.FindResource("Node_Icon") as BitmapImage;
            }
            set
            {
                // Nothing to do.
            }
        }

        /// <summary>
        /// Gets the data template.
        /// </summary>
        public virtual DataTemplate DataTemplate
        {
            get
            {
                return Themes.ThemeManager.Instance.FindResource("NodeViewDefaultDataTemplate") as DataTemplate;
            }
        }

        /// <summary>
        /// Gets the style to apply to the container.
        /// </summary>
        public virtual Style ContainerStyle
        {
            get
            {
                return Themes.ThemeManager.Instance.FindResource("GraphItemNodeViewDefaultStyleKey") as Style;
            }
        }

        /// <summary>
        /// Gets the Z index ordering the associated control.
        /// </summary>
        public virtual int ZIndex
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Gets or sets the warnings of the node.
        /// </summary>
        public object Warnings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the errors of the node.
        /// </summary>
        public object Errors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the warnings data template.
        /// </summary>
        public virtual DataTemplate WarningsDataTemplate
        {
            get
            {
                return Themes.ThemeManager.Instance.FindResource("MessageIndicatorTooltipDefaultDataTemplate") as DataTemplate;
            }
        }

        /// <summary>
        /// Gets or sets the errors data template.
        /// </summary>
        public virtual DataTemplate ErrorsDataTemplate
        {
            get
            {
                return Themes.ThemeManager.Instance.FindResource("MessageIndicatorTooltipDefaultDataTemplate") as DataTemplate;
            }
        }

        #endregion // Properties.

        #region Events

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion // Events.

        #region Methods

        /// <summary>
        /// Notifies when a property is changed.
        /// </summary>
        /// <param name="pPropertyName"></param>
        public void OnPropertyChanged(string pPropertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(pPropertyName));
            }
        }

        #endregion Methods.
    }
}
