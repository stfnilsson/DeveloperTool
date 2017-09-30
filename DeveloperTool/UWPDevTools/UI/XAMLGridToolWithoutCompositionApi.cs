using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UWPDevTools.UI
{
    internal class XamlGridToolWithoutCompositionApi : XamlGridToolBase
    {
        private readonly Canvas _canvas;

        internal XamlGridToolWithoutCompositionApi(ContentControl element)
        {
            _canvas = new Canvas();

            element.Content = _canvas;

            element.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            element.VerticalContentAlignment = VerticalAlignment.Stretch;

            element.IsHitTestVisible = false;
            element.Opacity = Opacity;
        }

        public override void Draw(Size newSize)
        {
            _canvas.Children.Clear();

            var brush = new SolidColorBrush(GridColor);

            DrawHorizontalLines(newSize.Width, newSize.Height, brush);

            DrawVeriticalLines(newSize.Width, newSize.Height, brush);
        }

        private void DrawHorizontalLines(double width, double height, Brush brush)
        {
            for (double y = 0; y < height; y += VerticalStep)
            {
                var line = new Rectangle
                {
                    Width = width,
                    Height = GridLineSize,
                    Fill = brush
                };
                Canvas.SetTop(line, y);

                _canvas.Children.Add(line);
            }
        }

        private void DrawVeriticalLines(double width, double height, Brush brush)
        {
            for (double x = 0; x < width; x += HorizontalStep)
            {
                var line = new Rectangle
                {
                    Width = GridLineSize,
                    Height = height,
                    Fill = brush
                };
                Canvas.SetLeft(line, x);

                _canvas.Children.Add(line);
            }
        }
    }
}
