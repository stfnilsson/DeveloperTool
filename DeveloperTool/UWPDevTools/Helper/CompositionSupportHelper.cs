using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace UWPDevTools.Helper
{
    public class CompositionSupportHelper : IDisposable
    {
        private readonly WeakReference<FrameworkElement> _mainControl;
        private Compositor _compositor;
        private ContainerVisual _visualRoot;

        public CompositionSupportHelper(FrameworkElement mainControl)
        {
            if (!IsSupported)
            {
                throw new PlatformNotSupportedException("Composition API support requried");
            }
           _mainControl = new WeakReference<FrameworkElement>(mainControl);

            AttachEvents();
        }

        public Compositor Compositor
        {
            get
            {
                if (_compositor == null && MainControl != null)
                {
                    _compositor = ElementCompositionPreview.GetElementVisual(MainControl).Compositor;
                    
                }

                return _compositor;
            }
        }

        public Action<Size> SizeChanged { get; set; }

        public Action<FrameworkElement> ControlInFocus { get; set; }

        public Action<FrameworkElement> ControlLostFocus { get; set; }

        public FrameworkElement MainControl
        {
            get
            {
                if (_mainControl == null)
                {
                    return null;
                }
                if (!_mainControl.TryGetTarget(out FrameworkElement mainControl))
                {
                    return null;
                }
                return mainControl;
            }
        }

        public ContainerVisual VisualRoot
        {
            get
            {
                if (_visualRoot == null)
                {
                    if (Compositor == null)
                    {
                        return null;
                    }
                    if (MainControl == null)
                    {
                        return null;
                    }
                    _visualRoot = Compositor.CreateContainerVisual();

                    if (_mainControl != null)
                    {
                        ElementCompositionPreview.SetElementChildVisual(MainControl, _visualRoot);
                    }
                    else
                    {
                        
                    }
                    
                }

                return _visualRoot;
            }
        }

        private static bool IsSupported
        {
            get { return ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3); }
        }

        public void Dispose()
        {
            DettachEvents();

            _visualRoot?.Dispose();
            _compositor?.Dispose();
        }

        private void AttachEvents()
        {
            if (MainControl == null)
            {
                return;
            }

            MainControl.SizeChanged -= _mainControl_SizeChanged;
            MainControl.SizeChanged += _mainControl_SizeChanged;
        }

        private void DettachEvents()
        {
            if (_mainControl == null)
            {
                return;
            }
            MainControl.SizeChanged -= _mainControl_SizeChanged;
        }

        private void _mainControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChanged?.Invoke(e.NewSize);
        }
    }
}
