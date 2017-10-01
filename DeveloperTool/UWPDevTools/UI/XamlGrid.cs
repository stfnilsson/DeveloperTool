using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPDevTools.UI
{
    public class XamlGrid : ContentControl, IDisposable
    {

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

        private List<XamlGridLine> _lines;

        public List<XamlGridLine> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                if (value == null)
                {
                    return;
                }
                _renderer.GridLines = value;
            }
        }

        //   private List<XamlGridLine> _gridLines;

        //public List<XamlGridLine> GridLines
        //{
        //    get { return _gridLines; }
        //    set
        //    {
        //        _gridLines = value;
        //        _renderer.GridLines = value.ToList();
        //    }
        //}
        //{

        //    get { return (List<XamlGridLine>)GetValue(GridLinesProperty); }

        //    set

        //    {

        //        SetValue(GridLinesProperty, value);

        //   //     _renderer.Draw(new Size(ActualWidth,ActualHeight));

        //    }

        //}

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
