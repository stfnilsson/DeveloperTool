using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using UWPDevTools.Helper;

namespace UWPDevTools.UI.Developer.XamlGrid
{
    public class XamlGridRenderer
    {
        //TODO: Remove state
        private readonly List<SpriteVisual> _linesDrawn = new List<SpriteVisual>();

        private readonly CompositionSupportHelper _compositionSupportHelper;

        public XamlGridRenderer(Control mainControl)
        {
            _compositionSupportHelper = new CompositionSupportHelper(mainControl);
        }
        public XamlGridRenderer(CompositionSupportHelper compositionSupportHelper)
        {
            _compositionSupportHelper = compositionSupportHelper ?? throw new ArgumentNullException(nameof(compositionSupportHelper));
        }

        public List<XamlGridLine> GridLines { get; set; }


        private SpriteVisual measurementSprite;
        public void ShowMeasurement(FrameworkElement element)
        {
            if (measurementSprite != null)
            {
                return;
            }

          
            var ttv = element.TransformToVisual(Window.Current.Content);
            Point screenCoords = ttv.TransformPoint(new Point(0, 0));
            
            float x = (float)(screenCoords.X + element.ActualWidth -10);
            float y = (float)(screenCoords.Y + -10.0);
            Vector3 offset = new Vector3(x, y,0);
         

            measurementSprite = _compositionSupportHelper.Compositor.CreateSpriteVisual();

            var brush = _compositionSupportHelper.Compositor.CreateColorBrush(Colors.Black);
            measurementSprite.Brush = brush;

            measurementSprite.Size = new Vector2(20,20);
            measurementSprite.Offset = offset;

           
            label.Foreground = new SolidColorBrush(Colors.White);
            label.Text = "hej";
            

            label.TransformToVisual()
            var fontVisual = ElementCompositionPreview.GetElementVisual(label);


            _compositionSupportHelper.VisualRoot.Children.InsertAtTop(measurementSprite);
            _compositionSupportHelper.VisualRoot.Children.InsertAtTop(fontVisual);


            //       ElementCompositionPreview.SetElementChildVisual(element, compositionSupportHelper.VisualRoot);


        }

        public void HideMeasurement(FrameworkElement element)
        {
            if (measurementSprite == null)
            {
                return;
            }
          //  _compositionSupportHelper.VisualRoot.Children.Remove(measurementSprite);

            //measurementSprite = null;
            //var visual = ElementCompositionPreview.GetElementVisual(element);

            //compositionSupportHelper.VisualRoot.Children.InsertAtTop(visual);


        }

        public void Draw(Size newSize)
        {
            if (_compositionSupportHelper?.VisualRoot == null)
            {  
                return;
            }
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
                _compositionSupportHelper.VisualRoot.Children.InsertAtTop(visual);
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
                _compositionSupportHelper.VisualRoot.Children.Remove(drawnLine);
            }

            _linesDrawn.Clear();
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
            SpriteVisual line = _compositionSupportHelper.Compositor.CreateSpriteVisual();
            line.Brush = _compositionSupportHelper.Compositor.CreateColorBrush(gridLineColor);
            line.Opacity = opacity;
            line.Size = new Vector2(width, gridLineSize);
            line.Offset = new Vector3(0, yOffset, 0);

            return line;
        }

        private SpriteVisual CreateVerticalLine(float length, float xOffset, Color gridLineColor, int gridLineSize, float opacity)
        {
            SpriteVisual line = _compositionSupportHelper.Compositor.CreateSpriteVisual();
            line.Brush = _compositionSupportHelper.Compositor.CreateColorBrush(gridLineColor);
            line.Size = new Vector2(gridLineSize, length);
            line.Offset = new Vector3(xOffset, 0, 0);
            line.Opacity = opacity;

            return line;
        }
    }
}
