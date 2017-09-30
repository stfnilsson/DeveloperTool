using Windows.Foundation;
using Windows.UI;

namespace UWPDevTools.UI {
    public abstract class XamlGridToolBase : IXamlGridTool
    {
        protected XamlGridToolBase()
        {
            GridColor = Colors.DeepSkyBlue;
            HorizontalStep = 12;
            VerticalStep = 12;
            GridLineSize = 1;
            Opacity = 0.5;
        }

        public Color GridColor { get; set; }
        public double HorizontalStep { get; set; }
        public double VerticalStep { get; set; }
        public int GridLineSize { get; set; }
        public double Opacity { get; set; }

        public abstract void Draw(Size newSize);
    }
}