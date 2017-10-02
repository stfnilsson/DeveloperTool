using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace UWPDevTools.UI
{
    internal class XamlGridRenderer : IDisposable
    {
        private readonly Compositor _compositor;
        private readonly ContainerVisual _control;
        private readonly List<SpriteVisual> _linesDrawn = new List<SpriteVisual>();

        public XamlGridRenderer(ContentControl element)
        {
            _compositor = ElementCompositionPreview.GetElementVisual(element).Compositor;
            _control = _compositor.CreateContainerVisual();

            ElementCompositionPreview.SetElementChildVisual(element, _control);
        }

        public List<XamlGridLine> GridLines { get; set; }

        public void Dispose()
        {
            Reset();

            _compositor?.Dispose();
            _control?.Dispose();
        }

        public void Draw(Size newSize)
        {
            Reset();

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

        private List<SpriteVisual> DrawGrid(XamlGridLine gridLine, float controlWidth, float controlHeight)
        {
            var visuals = new List<SpriteVisual>();

            if (gridLine.VerticalStep != 0)
            {
                int currentYOffet = gridLine.VerticalStep;
                while (currentYOffet <= controlHeight)
                {
                    visuals.Add(
                        CreateHorizontalLine(controlWidth, currentYOffet, gridLine.GridColor, gridLine.GridSize));
                    currentYOffet = currentYOffet + gridLine.VerticalStep;
                }
            }

            if (gridLine.HorizontalStep != 0)
            {
                int currentXOffet = gridLine.HorizontalStep;
                while (currentXOffet <= controlWidth)
                {
                    visuals.Add(CreateVerticalLine(controlHeight, currentXOffet, gridLine.GridColor,
                        gridLine.GridSize));
                    currentXOffet = currentXOffet + gridLine.HorizontalStep;
                }
            }
            return visuals;
        }

        private SpriteVisual CreateHorizontalLine(float width, float yOffset, Color gridLineColor, int gridLineSize)
        {
            SpriteVisual line = _compositor.CreateSpriteVisual();
            line.Brush = _compositor.CreateColorBrush(gridLineColor);
            line.Opacity = 0.3f;
            line.Size = new Vector2(width, gridLineSize);
            line.Offset = new Vector3(0, yOffset, 0);

            return line;
        }

        private SpriteVisual CreateVerticalLine(float length, float xOffset, Color gridLineColor, int gridLineSize)
        {
            SpriteVisual line = _compositor.CreateSpriteVisual();
            line.Brush = _compositor.CreateColorBrush(gridLineColor);
            line.Size = new Vector2(gridLineSize, length);
            line.Offset = new Vector3(xOffset, 0, 0);
            line.Opacity = 0.3F;

            return line;
        }

        private void Reset()
        {
            if (_linesDrawn == null || !_linesDrawn.Any())
            {
                return;
            }
            foreach (SpriteVisual drawnLine in _linesDrawn)
            {
                _control.Children.Remove(drawnLine);
            }

            _linesDrawn.Clear();
        }
    }
}
