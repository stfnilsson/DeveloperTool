using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace UWPDevTools.UI {
    public class XamlGridLine: DependencyObject
    {
        public XamlGridLine()
        {
            GridColor = Colors.DeepSkyBlue;
            GridSize = 1;
            HorizontalStep = 0;
            VerticalStep = 0;
            
        }

        public Color GridColor { get; set; }

        public int GridSize { get; set; }

        public int HorizontalStep { get; set; }

        public int VerticalStep { get; set; }
    }
}