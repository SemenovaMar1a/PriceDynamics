using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace PriceDynamics
{
    public class DrawingVisualHost : FrameworkElement
    {
        private readonly DrawingVisual _drawingVisual;

        public DrawingVisualHost(DrawingVisual drawingVisual)
        {
            _drawingVisual = drawingVisual;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _drawingVisual;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
    }
}
