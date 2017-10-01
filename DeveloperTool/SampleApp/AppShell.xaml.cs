using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWPDevTools.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell: Page
    {
        public Frame NavigationFrame
        {
            get { return ContentFrame; }
            set { ContentFrame = value; }
        }

        

        public AppShell()
        {
            this.InitializeComponent();

            XamlGridControl.Lines = new List<XamlGridLine>()
            {

                new XamlGridLine()
                {
                    GridColor = Colors.Red,
                    VerticalStep = 48,
                    HorizontalStep = 48

                },
                new XamlGridLine()
                {
                    GridColor = Colors.Gold,
                    VerticalStep = 12,
                    HorizontalStep = 12

                }

            };

            var app = App.Current as ICustomApplication;
            app.AppShell = NavigationFrame;

            ContentFrame.Navigate(typeof(MainPage));

        }


        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            
        }

        private void ContentFrame_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            
        }
    }
}
