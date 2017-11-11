using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


namespace SampleApp
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("goToMain", ContentGrid);

            var app = Application.Current as ICustomApplication;
            app?.AppShell.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ConnectedAnimationService service = ConnectedAnimationService.GetForCurrentView();
            ConnectedAnimation animation = service.GetAnimation("goToDetails");
            animation.IsScaleAnimationEnabled = true;
            animation.TryStart(ContentGrid);

            base.OnNavigatedTo(e);
        }
    }
}
