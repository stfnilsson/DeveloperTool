using Windows.UI;

namespace UWPDevTools.UI.XamlGrid
{
    public class XamlGridLine
    {
        public XamlGridLine()
        {
            GridColor = Colors.DeepSkyBlue;
            GridSize = 1;
            HorizontalStep = 0;
            VerticalStep = 0;
            Opacity = 0.3f;
            ShowStepMarker = false;
        }

        public Color GridColor { get; set; }

        public int GridSize { get; set; }

        public int HorizontalStep { get; set; }

        public int VerticalStep { get; set; }

        public float Opacity { get; set; }

        public bool ShowStepMarker { get;set;}
    }
}