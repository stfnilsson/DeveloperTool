using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPDevTools.UI
{
    public class XamlGridTool : ContentControl
    {
        public static readonly DependencyProperty GridLineColorProperty =
            DependencyProperty.Register("GridLineColor", typeof(Color), typeof(XamlGridTool),
                new PropertyMetadata(Colors.DeepSkyBlue));

        public static readonly DependencyProperty GridLineSizeProperty =
            DependencyProperty.Register("GridLineSize", typeof(int), typeof(XamlGridTool),
                new PropertyMetadata(1));

        public static readonly DependencyProperty HorizontalStepProperty =
            DependencyProperty.Register("HorizontalStep", typeof(double), typeof(XamlGridTool),
                new PropertyMetadata(12.0));

        public static readonly DependencyProperty VerticalStepProperty =
            DependencyProperty.Register("VerticalStep", typeof(double), typeof(XamlGridTool),
                new PropertyMetadata(12.0));

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(XamlGridTool),
                new PropertyMetadata(true));


        private readonly IXamlGridTool _devGridBase;

        public XamlGridTool()
        {
            SizeChanged += (_ , s) => { _devGridBase.Draw(s.NewSize); };

           
            if (IsCompositionApiSupported)
            {
          
                _devGridBase = new XamlGridToolUsingCompositionApi(this);
            }
            else
            {
                _devGridBase = new XamlGridToolWithoutCompositionApi(this);
            }
            _devGridBase.GridLineColor = GridLineColor;
            _devGridBase.GridLineSize = GridLineSize;
            _devGridBase.HorizontalStep = HorizontalStep;
            _devGridBase.VerticalStep = VerticalStep;
            
        }


        public static bool IsCompositionApiSupported
        {
            get { return ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3); }
        }

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }

            set
            {
                SetValue(IsVisibleProperty, value);

                if (_devGridBase == null || _devGridBase.IsVisible == value)
                {
                    return;
                }
                _devGridBase.IsVisible = value;
                _devGridBase.Draw(new Size(ActualWidth, ActualHeight));
            }
        }

        public Color GridLineColor
        {
            get { return (Color) GetValue(GridLineColorProperty); }

            set
            {
                SetValue(GridLineColorProperty, value);
                if (_devGridBase == null || _devGridBase.GridLineColor == value)
                {
                    return;
                }
                _devGridBase.GridLineColor = value;
                _devGridBase.Draw(new Size(ActualWidth,ActualHeight));
            }
        }

        public int GridLineSize
        {
            get { return (int) GetValue(GridLineSizeProperty); }

            set
            {
                SetValue(GridLineSizeProperty, value);
                
                if (_devGridBase == null || _devGridBase.GridLineSize == value)
                {
                    return;
                }
                _devGridBase.GridLineSize = value;
                _devGridBase.Draw(new Size(ActualWidth, ActualHeight));
            }
        }

        public double HorizontalStep
        {
            get { return (double) GetValue(HorizontalStepProperty); }

            set
            {
                SetValue(HorizontalStepProperty, value);
                if (_devGridBase == null || _devGridBase.HorizontalStep == value)
                {
                    return;
                }
                _devGridBase.HorizontalStep = value;
                _devGridBase.Draw(new Size(ActualWidth, ActualHeight));
            }
        }

        public double VerticalStep
        {
            get { return (double) GetValue(VerticalStepProperty); }

            set
            {
                SetValue(VerticalStepProperty, value);
                if (_devGridBase == null || _devGridBase.VerticalStep == value)
                {
                    return;
                }
                _devGridBase.VerticalStep = value;
                _devGridBase.Draw(new Size(ActualWidth, ActualHeight));
            }
        }

       
    }
}
