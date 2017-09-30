using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace UWPDevTools.UI
{
    public class XamlGridTool : ContentControl
    {

        protected static bool IsCompositionApiSupported
        {
            get { return ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3); }
        }

        public Color GridColor { get; set; }

        private readonly IXamlGridTool _devGridBase;

        public XamlGridTool()
        { 
            SizeChanged += (_, s) =>
            {
                _devGridBase.Draw(s.NewSize);
            };

            if (IsCompositionApiSupported)
            {
                _devGridBase = new XamlGridToolUsingCompositionApi(this);
            }
            else
            {
                _devGridBase = new XamlGridToolWithoutCompositionApi(this);
            }
        }
    }
}
