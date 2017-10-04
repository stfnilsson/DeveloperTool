using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Composition.Interactions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPDevTools.UI
{
    public class XamlGrid : ContentControl, IDisposable
    {
        public static readonly DependencyProperty LinesProperty = DependencyProperty.Register("Lines",
            typeof(List<XamlGridLine>), typeof(XamlGrid), new PropertyMetadata(null));


        public List<XamlGridLine> Lines
        {
            get { return (List<XamlGridLine>)GetValue(LinesProperty); }
            set
            {
                SetValue(LinesProperty, value);
            }
        }

        private XamlGridRenderer _renderer;

        public XamlGrid()
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
