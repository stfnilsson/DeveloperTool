using Windows.Foundation;
using Windows.UI;

namespace UWPDevTools.UI
{
    public interface IXamlGridTool
    {
        Color GridColor { get; set; }
        int GridLineSize { get; set; }
        double HorizontalStep { get; set; }
        double Opacity { get; set; }
        double VerticalStep { get; set; }

        void Draw(Size newSize);
    }
}