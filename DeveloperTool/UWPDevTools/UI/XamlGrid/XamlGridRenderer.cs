using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace UWPDevTools.UI.XamlGrid
{
    public class XamlGridRenderer : IDisposable
    {
        private readonly Compositor _compositor;
        private readonly ContainerVisual _control;

        //TODO: Remove state
        private readonly List<SpriteVisual> _linesDrawn = new List<SpriteVisual>();

        public XamlGridRenderer(UIElement element)
        {
            if (!IsCompositionApiSupported)
            {
                throw  new PlatformNotSupportedException("Composition API support requried");
            }
            _compositor = ElementCompositionPreview.GetElementVisual(element).Compositor;
            _control = _compositor.CreateContainerVisual();

            ElementCompositionPreview.SetElementChildVisual(element, _control);
        }

        public List<XamlGridLine> GridLines { get; set; }

        public void Dispose()
        {
            Clear();

            _compositor?.Dispose();
            _control?.Dispose();
        }

        public void Draw(Size newSize)
        {
            Clear();

            if (GridLines == null || !GridLines.Any())
            {
                return;
            }
            if (newSize.Width <= 0)
            {
                return;
            }
            if (newSize.Height <= 0)
            {
                return;
            }

            var width = (float) newSize.Width;
            var height = (float) newSize.Height;
            List<XamlGridLine> gridLines = GridLines;

            gridLines.Reverse();

            foreach (XamlGridLine gridLine in gridLines)
            {
                List<SpriteVisual> visuals = DrawGrid(gridLine, width, height);
                _linesDrawn.AddRange(visuals);
            }

            foreach (SpriteVisual visual in _linesDrawn)
            {
                _control.Children.InsertAtTop(visual);
            }
        }

        public bool IsEnabled
        {
            get { return _linesDrawn != null && _linesDrawn.Any(); }
        }

        public void Clear()
        {
            if (!IsEnabled)
            {
                return;
            }
            foreach (SpriteVisual drawnLine in _linesDrawn)
            {
                _control.Children.Remove(drawnLine);
            }

            _linesDrawn.Clear();
        }

        private bool IsCompositionApiSupported
        {
            get { return ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 3); }
        }


        private List<SpriteVisual> DrawGrid(XamlGridLine gridLine, float controlWidth, float controlHeight)
        {
            var visuals = new List<SpriteVisual>();

            if (gridLine.VerticalStep != 0)
            {
                int currentYOffset = gridLine.VerticalStep;
                while (currentYOffset <= controlHeight)
                {
                    visuals.Add(
                        CreateHorizontalLine(controlWidth, currentYOffset, gridLine.GridColor, gridLine.GridSize, gridLine.Opacity));
                    currentYOffset = currentYOffset + gridLine.VerticalStep;
                }
            }

            if (gridLine.HorizontalStep != 0)
            {
                int currentXOffset = gridLine.HorizontalStep;
                while (currentXOffset <= controlWidth)
                {
                    visuals.Add(CreateVerticalLine(controlHeight, currentXOffset, gridLine.GridColor,
                        gridLine.GridSize,gridLine.Opacity));

                    //if (gridLine.ShowStepMarker)
                    //{
                    //    var shadow = _compositor.CreateDropShadow();
                    //    shadow.Opacity = 0.5f;

                    //    SpriteVisual marker = _compositor.CreateSpriteVisual();
                    //    marker.Brush = _compositor.CreateColorBrush(gridLine.GridColor);
                    //    marker.CenterPoint = new Vector3(currentXOffset, controlHeight -12,0);
                    //    marker.Size = new Vector2(7, 7);
                    //    marker.Shadow = shadow;
                    //    marker.Offset = new Vector3(currentXOffset - 3, controlHeight - 12, 0);

                        
                    //    visuals.Add(marker);

                    //}

                    currentXOffset = currentXOffset + gridLine.HorizontalStep;
                }
              

            }
            return visuals;
        }

        private SpriteVisual CreateHorizontalLine(float width, float yOffset, Color gridLineColor, int gridLineSize, float opacity)
        {
            SpriteVisual line = _compositor.CreateSpriteVisual();
            line.Brush = _compositor.CreateColorBrush(gridLineColor);
            line.Opacity = opacity;
            line.Size = new Vector2(width, gridLineSize);
            line.Offset = new Vector3(0, yOffset, 0);

            return line;
        }

        private SpriteVisual CreateVerticalLine(float length, float xOffset, Color gridLineColor, int gridLineSize, float opacity)
        {
            SpriteVisual line = _compositor.CreateSpriteVisual();
            line.Brush = _compositor.CreateColorBrush(gridLineColor);
            line.Size = new Vector2(gridLineSize, length);
            line.Offset = new Vector3(xOffset, 0, 0);
            line.Opacity = opacity;

            return line;
        }
    }
}
