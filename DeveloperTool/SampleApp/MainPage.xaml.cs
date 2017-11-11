using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace SampleApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("goToDetails", MasterContentPane);


            var app = Application.Current as ICustomApplication;
            app?.AppShell.Navigate(typeof(DetailPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ConnectedAnimationService service = ConnectedAnimationService.GetForCurrentView();
            ConnectedAnimation animation = service.GetAnimation("goToMain");
            if (animation != null)
            {
                animation.IsScaleAnimationEnabled = true;
                animation.TryStart(MasterContentPane);
            }
            ShadowControlInstance.PointerEntered -= ShadowControlInstance_PointerEntered;
            ShadowControlInstance.PointerEntered += ShadowControlInstance_PointerEntered;

            ShadowControlInstance.PointerExited -= ShadowControlInstance_PointerExited;
            ShadowControlInstance.PointerExited += ShadowControlInstance_PointerExited;

            base.OnNavigatedTo(e);
        }

        private void ShadowControlInstance_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            AppShell.CompositionSupport.ControlLostFocus?.Invoke(ShadowControlInstance);
        }

        private void ShadowControlInstance_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
          
            AppShell.CompositionSupport.ControlInFocus?.Invoke(ShadowControlInstance);
        }
    }
}
