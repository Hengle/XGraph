﻿using System.Windows;
using System.Windows.Input;
using XGraph.Extensions;

namespace XGraph.Controls
{
    /// <summary>
    /// Class defining an input connector.
    /// </summary>
    public class OutputConnector : AConnector
    {
        #region Fields

        /// <summary>
        /// This field stores the drag start point, relative to the DesignerCanvas 
        /// </summary>
        private Point? mDragStartPoint;

        #endregion // Fields.

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="OutputConnector"/> class.
        /// </summary>
        static OutputConnector()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(OutputConnector), new FrameworkPropertyMetadata(typeof(OutputConnector)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputConnector"/> class.
        /// </summary>
        public OutputConnector()
        {
        }

        #endregion // Constructors.

        #region Methods

        /// <summary>
        /// This method is called each time the mouse is moved over the constrol.
        /// </summary>
        /// <param name="pEventArgs">The event arguments.</param>
        protected override void OnMouseDown(MouseButtonEventArgs pEventArgs)
        {
            base.OnMouseMove(pEventArgs);

            SimpleGraphView lParentView = this.FindVisualParent<SimpleGraphView>();
            if (this.mDragStartPoint.HasValue == false)
            {
                if (lParentView != null)
                {
                    // Position relative to DesignerCanvas.
                    this.mDragStartPoint = pEventArgs.GetPosition(lParentView);
                    pEventArgs.Handled = true;
                }
            }

            if (this.mDragStartPoint.HasValue)
            {
                // Create connection.
                if (lParentView != null)
                {
                    lParentView.ConnectionCreationBehavior.StartCreation(this);
                    pEventArgs.Handled = true;
                }
            }
        }

        #endregion // Methods.
    }
}
