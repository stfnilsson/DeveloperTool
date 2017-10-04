using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPDevTools.UI.XamlGrid
{
    /// <summary>
    /// XamlGrid from XAML
    /// </summary>
    public class XamlGridControl : ContentControl, IDisposable
    {
        public static readonly DependencyProperty LinesProperty = DependencyProperty.Register("Lines",
            typeof(List<XamlGridLine>), typeof(XamlGridControl), new PropertyMetadata(null));

        private XamlGridRenderer _renderer;

        public XamlGridControl()
        {
            SizeChanged += (_, s) =>
            {
                if (Lines == null || !Lines.Any())
                {
                    return;
                }
                _renderer?.Draw(s.NewSize);
            };

            if (!IsCompositionApiSupported)
            {
                throw new NotSupportedException("Composition API not supoorted");
            }
            _renderer = new XamlGridRenderer(this);
        }

        public List<XamlGridLine> Lines
        {
            get { return (List<XamlGridLine>) GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }

        private bool IsCompositionApiSupported
        {
            get { return ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3); }
        }

        public void Dispose()
        {
            _renderer?.Dispose();
            _renderer = null;
        }
    }
}
