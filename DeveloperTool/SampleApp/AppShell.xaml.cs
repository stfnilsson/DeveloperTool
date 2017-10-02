using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using UWPDevTools.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleApp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            InitializeComponent();

            XamlGridControl.Lines = new List<XamlGridLine>
            {
                new XamlGridLine
                {
                    GridColor = Colors.Red,
                    VerticalStep = 0,
                    HorizontalStep = 24
                },
                new XamlGridLine
                {
                    GridColor = Colors.Gold,
                    VerticalStep = 48,
                    HorizontalStep = 0
                },
                new XamlGridLine
                {
                    GridColor = Colors.LightGray,
                    VerticalStep = 12,
                    HorizontalStep = 12
                }
            };

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

        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e) { }

        private void ContentFrame_OnNavigating(object sender, NavigatingCancelEventArgs e) { }
    }
}
