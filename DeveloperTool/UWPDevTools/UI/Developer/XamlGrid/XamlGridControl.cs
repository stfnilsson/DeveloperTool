using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPDevTools.Helper;

namespace UWPDevTools.UI.Developer.XamlGrid
{
    /// <summary>
    ///     XamlGrid from XAML
    /// </summary>
    public class XamlGridControl : ContentControl
    {
        public static readonly DependencyProperty LinesProperty = DependencyProperty.Register("Lines",
            typeof(List<XamlGridLine>), typeof(XamlGridControl), new PropertyMetadata(null));

        public XamlGridControl()
        {
            var compositionSupportHelper = new CompositionSupportHelper(this);

            var renderer = new XamlGridRenderer(compositionSupportHelper);

            compositionSupportHelper.SizeChanged = newSize =>
            {
                if (Lines == null || !Lines.Any())
                {
                    return;
                }
                renderer?.Draw(newSize);
            };
        }

        public List<XamlGridLine> Lines
        {
            get { return (List<XamlGridLine>) GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }
    }
}
