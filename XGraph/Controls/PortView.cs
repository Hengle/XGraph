﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XGraph.ViewModels;

namespace XGraph.Controls
{
    /// <summary>
    /// Class representing a port.
    /// </summary>
    [TemplatePart(Name = PART_CONNECTORS_PRESENTER, Type = typeof(AConnectorPresenter))]
    public class PortView : ContentControl
    {
        #region Dependencies

        /// <summary>
        /// Identifies the Direction dependency property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(PortDirection), typeof(PortView), new FrameworkPropertyMetadata(PortDirection.Output));

        #endregion // Dependencies.

        #region Fields

        /// <summary>
        /// Name of the parts that have to be in the control template.
        /// </summary>
        private const string PART_CONNECTORS_PRESENTER = "PART_ConnectorPresenter";

        /// <summary>
        /// Stores the connectors presenter.
        /// </summary>
        private AConnectorPresenter mConnectorPresenter;

        #endregion // Fields.

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="PortContainer"/> class.
        /// </summary>
        static PortView()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PortView), new FrameworkPropertyMetadata(typeof(PortView)));
        }

        #endregion // Constructors.

        #region Properties

        /// <summary>
        /// Gets or sets direction.
        /// </summary>
        public PortDirection Direction
        {
            get
            {
                return (PortDirection)this.GetValue(DirectionProperty);
            }
            set
            {
                this.SetValue(DirectionProperty, value);
            }
        }

        /// <summary>
        /// Gets the port connector.
        /// </summary>
        public AConnector Connector
        {
            get
            {
                return this.mConnectorPresenter.Connector;
            }
        }

        #endregion // Properties.

        #region Methods
        
        /// <summary>
        /// Method called when the control content changed.
        /// </summary>
        /// <param name="pOldContent">The previous content.</param>
        /// <param name="pNewContent">The new content.</param>
        protected override void OnContentChanged(object pOldContent, object pNewContent)
        {
            base.OnContentChanged(pOldContent, pNewContent);

            BindingOperations.ClearAllBindings(this);

            // The content is the view model.
            PortViewModel lNewContent = pNewContent as PortViewModel;
            if (lNewContent != null)
            {
                // Stting the content data template.
                this.ContentTemplate = lNewContent.DataTemplate;

                // Binding the direction.
                Binding lDirectionBinding = new Binding("Direction") { Source = lNewContent, Mode = BindingMode.TwoWay };
                this.SetBinding(PortView.DirectionProperty, lDirectionBinding);

                // Binding the tooltip.
                Binding lTooltipBinding = new Binding("ToolTip") { Source = lNewContent, Mode = BindingMode.TwoWay };
                this.SetBinding(PortView.ToolTipProperty, lTooltipBinding);
            }
        }

        /// <summary>
        /// Method called when the control template is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Getting the parts of the control.
            this.mConnectorPresenter = this.GetTemplateChild(PART_CONNECTORS_PRESENTER) as AConnectorPresenter;

            if (this.mConnectorPresenter == null)
            {
                throw new Exception("PortView control template not correctly defined.");
            }

            if (this.Connector != null)
            {
                this.CreateConnectorBindings();
            }
            else
            {
                this.mConnectorPresenter.TemplateApplied += this.OnConnectorPresenterTemplateApplied;
            }
        }

        /// <summary>
        /// Delegate called when the connector presenter control template is applied.
        /// </summary>
        private void OnConnectorPresenterTemplateApplied()
        {
            this.CreateConnectorBindings();
        }

        /// <summary>
        /// Creates the bindings on the loaded connector.
        /// </summary>
        private void CreateConnectorBindings()
        {
            // The content of the PortView is the PortViewModel.
            Binding lPositionBinding = new Binding("Position");
            lPositionBinding.Source = this.Content;
            lPositionBinding.Mode = BindingMode.OneWayToSource;
            this.Connector.SetBinding(AConnector.PositionProperty, lPositionBinding);
        }

        #endregion // Methods.
    }
}
