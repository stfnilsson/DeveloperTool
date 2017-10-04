using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using UWPDevTools.UI;
using UWPDevTools.UI.XamlGrid;

namespace SampleApp
{
    public sealed partial class AppShell : Page
    {
        private XamlGridRenderer _xamlGridRenderer;

        public AppShell()
        {
            InitializeComponent();

#if DEBUG
            SetUpXamlGrid();
#endif

            var app = Application.Current as ICustomApplication;
            if (app != null)
            {
                app.AppShell = NavigationFrame;
            }

            ContentFrame.Navigate(typeof(MainPage));
        }

        public Frame NavigationFrame
        {
            get { return ContentFrame; }
            set { ContentFrame = value; }
        }

        private void SetUpXamlGrid()
        {
            _xamlGridRenderer = new XamlGridRenderer(ContentFrame);

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

            SizeChanged += (_, e) =>
            {
                if (_xamlGridRenderer?.GridLines == null || !_xamlGridRenderer.GridLines.Any())
                {
                    return;
                }
                _xamlGridRenderer?.Draw(e.NewSize);
            };

            ContentFrame.RightTapped += ContentFrame_RightTapped;
        }

       

        private void ContentFrame_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ToogleXamlGrid();
        }

        private void ToogleXamlGrid()
        {
            if (_xamlGridRenderer.IsEnabled)
            {
                _xamlGridRenderer.Clear();
            }
            else
            {
                _xamlGridRenderer.Draw(new Size(ActualWidth, ActualWidth));
            }
        }

        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e) { }

        private void ContentFrame_OnNavigating(object sender, NavigatingCancelEventArgs e) { }
    }
}
