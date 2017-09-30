using Windows.Foundation;
using Windows.UI;

namespace UWPDevTools.UI
{
    internal interface IXamlGridTool
    {
        bool IsVisible { get; set; }
        Color GridLineColor { get; set; }
        int GridLineSize { get; set; }
        double HorizontalStep { get; set; }
        double VerticalStep { get; set; }

        void Draw(Size newSize);
    }
}