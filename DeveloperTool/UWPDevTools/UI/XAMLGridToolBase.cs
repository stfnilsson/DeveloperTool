using Windows.Foundation;
using Windows.UI;

namespace UWPDevTools.UI {
    internal abstract class XamlGridToolBase : IXamlGridTool
    {
        public Color GridLineColor { get; set; }
        public double HorizontalStep { get; set; }
        public double VerticalStep { get; set; }
        public int GridLineSize { get; set; }

        public abstract void Draw(Size newSize);
    }
}