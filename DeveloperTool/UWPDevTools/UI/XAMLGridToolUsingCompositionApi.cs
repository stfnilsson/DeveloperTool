using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace UWPDevTools.UI
{
    internal class XamlGridToolUsingCompositionApi : XamlGridToolBase, IDisposable
    {
        private readonly Compositor _compositor;
        private readonly ContainerVisual _control;
        private readonly List<SpriteVisual> _linesDrawn = new List<SpriteVisual>();

        internal XamlGridToolUsingCompositionApi(ContentControl element)
        {
            _compositor = ElementCompositionPreview.GetElementVisual(element).Compositor;
            _control = _compositor.CreateContainerVisual();

            ElementCompositionPreview.SetElementChildVisual(element, _control);
        }

        public void Dispose()
        {
            Reset();

            _compositor.Dispose();
            _control.Dispose();
        }

        public override void Draw(Size newSize)
        {
            Reset();

            if (!IsVisible)
            {
                return;
            }
            var width = (float) newSize.Width;
            var height = (float) newSize.Height;

            var verticalStep = (float) VerticalStep;
            var horizontalStep = (float) HorizontalStep;

            Reset();

            float currentYOffet = verticalStep;
            while (currentYOffet < height)
            {
                _linesDrawn.Add(CreateHorizontalLine(width, currentYOffet));
                currentYOffet = currentYOffet + verticalStep;
            }
            float currentXOffet = horizontalStep;
            while (currentXOffet < width)
            {
                _linesDrawn.Add(CreateVerticalLine(height, currentXOffet));
                currentXOffet = currentXOffet + horizontalStep;
            }

            foreach (SpriteVisual visual in _linesDrawn)
            {
                visual.Opacity = 0;
                _control.Children.InsertAtTop(visual);
                StartShowAnimation(visual);
            }
        }

        private void StartShowAnimation(SpriteVisual sprite)
        {
            ScalarKeyFrameAnimation animation = _compositor.CreateScalarKeyFrameAnimation();

            animation.InsertKeyFrame(0.0f, 0.00f);

            animation.InsertKeyFrame(0.2f, 0.40f);

            animation.Duration = TimeSpan.FromMilliseconds(2000);

            sprite.StartAnimation(nameof(sprite.Opacity), animation);
        }

        private void StartHideAnimation(SpriteVisual sprite)
        {
            ScalarKeyFrameAnimation animation = _compositor.CreateScalarKeyFrameAnimation();

            animation.InsertKeyFrame(0.4f, 0.40f);

            animation.InsertKeyFrame(0.2f, 0.00f);

            animation.Duration = TimeSpan.FromMilliseconds(2000);

            sprite.StartAnimation(nameof(sprite.Opacity), animation);
        }

        private void Reset()
        {
            foreach (SpriteVisual drawnLine in _linesDrawn)
            {
                StartHideAnimation(drawnLine);
                _control.Children.Remove(drawnLine);

            }

            _linesDrawn.Clear();
        }

        private SpriteVisual CreateHorizontalLine(float width, float yOffset)
        {
            SpriteVisual line = _compositor.CreateSpriteVisual();
            line.Brush = _compositor.CreateColorBrush(GridLineColor);
            line.Opacity = 0.3f;
            line.Size = new Vector2(width, GridLineSize);
            line.Offset = new Vector3(0, yOffset, 0);
            return line;
        }

        private SpriteVisual CreateVerticalLine(float length, float xOffset)
        {
            SpriteVisual line = _compositor.CreateSpriteVisual();
            line.Brush = _compositor.CreateColorBrush(GridLineColor);
            line.Size = new Vector2(GridLineSize, length);
            line.Offset = new Vector3(xOffset, 0, 0);
            return line;
        }

    }
}
