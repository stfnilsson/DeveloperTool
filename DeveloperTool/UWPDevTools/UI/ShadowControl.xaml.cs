using System.Linq;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Shapes;
using UWPDevTools.Helper;

namespace UWPDevTools.UI
{
    [ContentProperty(Name = nameof(ContentElement))]
    public sealed partial class ShadowControl : UserControl
    {
        private FrameworkElement _contentElement;

        public ShadowControl()
        {
            InitializeComponent();

            Loaded += ShadowControl_Loaded;
            Unloaded += ShadowControl_Unloaded;
        }

        private void ShadowControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ShadowControl_Loaded;
            Loaded -= ShadowControl_Unloaded;
        }

        private void ShadowControl_Loaded(object sender, RoutedEventArgs e)
        {
            Setup();
        }

        public FrameworkElement ContentElement
        {
            get { return _contentElement; }
            set
            {
                _contentElement = value;
            }
        }

        private void Setup()
        {
            var compositionSupportHelper = new CompositionSupportHelper(this);

            var visual = ElementCompositionPreview.GetElementVisual(_contentElement);

            var spriteVisual = visual.Compositor.CreateSpriteVisual();


            var brush = compositionSupportHelper.Compositor.CreateColorBrush(Colors.Transparent);
            spriteVisual.Brush = brush;

            spriteVisual.Size = new Vector2((float)_contentElement.ActualWidth, (float)_contentElement.ActualHeight);
            //spriteVisual.Offset = new Vector3(24, 24, 0);

            
            compositionSupportHelper.VisualRoot.Children.InsertAtTop(spriteVisual);
            compositionSupportHelper.VisualRoot.Children.InsertAtTop(visual);

            var shadow = compositionSupportHelper.Compositor.CreateDropShadow();
            shadow.BlurRadius = 4;
            shadow.Opacity = 0.6f;
            shadow.Color = Colors.Black;
            shadow.Offset = new Vector3(4, 4, 0);

            var shape = _contentElement as Shape;
            if (shape != null)
            {
                var mask = shape.GetAlphaMask();
                shadow.Mask = mask;
            }
            

            spriteVisual.Shadow = shadow;

        }
    }
}
