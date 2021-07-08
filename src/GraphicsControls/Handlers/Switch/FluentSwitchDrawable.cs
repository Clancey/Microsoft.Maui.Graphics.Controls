using Microsoft.Maui.Animations;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float FluentThumbOffPosition = 10f;
        const float FluentThumbOnPosition = 30f;
        const float FluentSwitchBackgroundWidth = 40;

		public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();


            Color onColor = view.TrackColor.WithDefault(Fluent.Color.Primary.ThemePrimary);
            Color offColor = Fluent.Color.Primary.ThemePrimary.ToColor();
            bool useBackground = false;
            if (view.Background != null)
            {
                useBackground = true;
                if (view.Background is SolidPaint sp)
                {
                    offColor = sp.Color;
                    useBackground = false;
                }
                //If it's not a solid color, how do we do that?
            }

            var fillPaint = offColor.Lerp(onColor, AnimationPercent);

            if (view.IsEnabled)
            {
                if (useBackground)
                    canvas.SetFillPaint(view.Background, dirtyRect);
                else
                    canvas.FillColor = fillPaint;
            }
            else
                canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = 20;
            var width = FluentSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 10);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            canvas.FillColor = view.ThumbColor.WithDefault(Fluent.Color.Foreground.White);

            var margin = 4;
            var radius = 6;

            var y = dirtyRect.Y + margin + radius;

            var fluentThumbPosition = FluentThumbOffPosition.Lerp(FluentThumbOnPosition, AnimationPercent);
            canvas.FillCircle(fluentThumbPosition, y, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 20f);
    }
}