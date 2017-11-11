using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using UWPDevTools.Helper;
using UWPDevTools.UI;
using UWPDevTools.UI.Developer.XamlGrid;

namespace SampleApp
{
    public sealed partial class AppShell : Page
    {
        public static CompositionSupportHelper CompositionSupport;

        private XamlGridRenderer _xamlGridRenderer;
        public AppShell()
        {
            InitializeComponent();

            var app = Application.Current as ICustomApplication;
            if (app != null)
            {
                app.AppShell = NavigationFrame;
            }

            ContentFrame.Navigate(typeof(MainPage));

#if DEBUG
             SetUpXamlGrid(); 
#endif
        }

        public Frame NavigationFrame
        {
            get { return ContentFrame; }
            set { ContentFrame = value; }
        }

        private void SetUpXamlGrid()
        {
            CompositionSupport = new CompositionSupportHelper(ContentFrame);

            _xamlGridRenderer = new XamlGridRenderer(CompositionSupport);

            var xamlGridLines = new List<XamlGridLine>
            {
                new XamlGridLine
                {
                    GridColor = Colors.Red,
                    VerticalStep = 0,
                    HorizontalStep = 24,
                    ShowStepMarker = true
                },
                new XamlGridLine
                {
                    GridColor = Colors.Gold,
                    VerticalStep = 48,
                    HorizontalStep = 0,
                    Opacity = 0.4f
                },
                new XamlGridLine
                {
                    GridColor = Colors.LightGray,
                    VerticalStep = 12,
                    HorizontalStep = 12
                }
            };
            _xamlGridRenderer.GridLines = xamlGridLines;

            CompositionSupport.SizeChanged = newSize =>
            {
                if (_xamlGridRenderer?.GridLines == null || !_xamlGridRenderer.GridLines.Any())
                {
                    return;
                }
                _xamlGridRenderer?.Draw(newSize);
            };
    
            ContentFrame.RightTapped += (_, e) =>
            {
                if (_xamlGridRenderer.IsEnabled)
                {
                    _xamlGridRenderer.Clear();
                }
                else
                {
                    _xamlGridRenderer.Draw(new Size(ActualWidth, ActualWidth));
                }
            };

            CompositionSupport.ControlInFocus = _xamlGridRenderer.ShowMeasurement;

            CompositionSupport.ControlLostFocus = _xamlGridRenderer.HideMeasurement;
        }

        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e) { }

        private void ContentFrame_OnNavigating(object sender, NavigatingCancelEventArgs e) { }
    }
}
