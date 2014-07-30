using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace SakuraUI.Utilities
{
    public static class AnimationCreator
    {
        public static Storyboard CreateTranslateRotationEffects(
            DependencyObject target,
            TimeSpan beginTime,
            Duration duration,
            double translateXFrom,
            double translateXTo,
            double translateYFrom,
            double translateYTo,
            double rotationTo,
            double opacityTo = 0)
        {
            var translateX = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = translateXFrom,
                To = translateXTo,
            };

            var translateY = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = translateYFrom,
                To = translateYTo,
            };

            var opacity = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = opacityTo,
            };

            var rotation = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = rotationTo,
            };

            Storyboard.SetTarget(translateX, target);
            Storyboard.SetTargetProperty(translateX, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
            Storyboard.SetTarget(translateY, target);
            Storyboard.SetTargetProperty(translateY, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTarget(opacity, target);
            Storyboard.SetTargetProperty(opacity, "Opacity");
            Storyboard.SetTarget(rotation, target);
            Storyboard.SetTargetProperty(rotation, "(UIElement.RenderTransform).(CompositeTransform.Rotation)");

            var sb = new Storyboard();
            sb.Children.Add(translateX);
            sb.Children.Add(translateY);
            sb.Children.Add(opacity);
            sb.Children.Add(rotation);
            return sb;
        }

        public static Storyboard CreateFadeInEffects(UIElement target, TimeSpan beginTime, Duration duration, double from = 0, double to = 1)
        {
            var t1 = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = to,
                From = from > 0 ? from : target.Opacity,
            };

            Storyboard.SetTarget(t1, target);
            Storyboard.SetTargetProperty(t1, "Opacity");
            var sb = new Storyboard();
            sb.Children.Add(t1);
            return sb;
        }

        public static Storyboard CreateTranslateYFadeInEffects(UIElement target, TimeSpan beginTime, Duration duration, double translateFrom, double translateTo, double opacityFrom = 0, double opacityTo = 1)
        {
            var t1 = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = opacityTo,
                From = opacityFrom
            };

            Storyboard.SetTarget(t1, target);
            Storyboard.SetTargetProperty(t1, "Opacity");

            var translateY = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = translateFrom,
                To = translateTo,
            };

            Storyboard.SetTarget(translateY, target);
            Storyboard.SetTargetProperty(translateY, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");

            var sb = new Storyboard();
            sb.Children.Add(t1);
            sb.Children.Add(translateY);

            return sb;
        }

        public static Storyboard CreateRandomFadeInEffects(UIElement target, TimeSpan duration, double from = 0, double to = 1)
        {
            var r = new Random((int)DateTime.Now.Ticks);
            var milliseconds = r.Next(500, 1200);
            return CreateFadeInEffects(target, TimeSpan.FromMilliseconds(milliseconds), duration, from, to);
        }

        public static Storyboard CreateFadeOutEffects(UIElement target, TimeSpan beginTime, TimeSpan duration)
        {
            return CreateFadeOutEffects(target, beginTime, duration, target.Opacity, 0);
        }

        public static Storyboard CreateFadeOutEffects(DependencyObject target, TimeSpan beginTime, TimeSpan duration, double from, double to)
        {
            var t1 = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = to
            };

            if (from > 0)
            {
                t1.From = from;
            }

            Storyboard.SetTarget(t1, target);
            Storyboard.SetTargetProperty(t1, "Opacity");
            var sb = new Storyboard();
            sb.Children.Add(t1);
            return sb;
        }

        public static Storyboard CreateTranslateXWithFadeEffects(UIElement target, TimeSpan beginTime, TimeSpan duration, double from, double to, double opacityFrom = 0, double opacityTo = 1, EasingFunctionBase easing = null)
        {
            target.Opacity = opacityFrom;

            var t1 = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = to,
                EasingFunction = easing,
            };

            if (from >= 0)
            {
                t1.From = from;
            }

            Storyboard.SetTarget(t1, target);
            Storyboard.SetTargetProperty(t1, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");


            var opacity = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = opacityFrom,
                To = opacityTo,
            };

            Storyboard.SetTarget(opacity, target);
            Storyboard.SetTargetProperty(opacity, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(t1);
            sb.Children.Add(opacity);
            return sb;
        }

        public static Storyboard CreateTranslateYWithFadeEffects(UIElement target, TimeSpan beginTime, TimeSpan duration, double from, double to, double opacityFrom = 1, double opacityTo = 0)
        {
            var t1 = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = to,
                EasingFunction = new PowerEase { Power = 2.5 },
            };

            if (from >= 0)
            {
                t1.From = from;
            }

            Storyboard.SetTarget(t1, target);
            Storyboard.SetTargetProperty(t1, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");

            var opacity = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = opacityFrom,
                To = opacityTo,
            };

            Storyboard.SetTarget(opacity, target);
            Storyboard.SetTargetProperty(opacity, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(t1);
            sb.Children.Add(opacity);
            return sb;
        }

        public static Storyboard CreateScaleYEffects(DependencyObject target, TimeSpan beginTime, TimeSpan duration, double from = 0, double to = 1, EasingFunctionBase easing = null)
        {
            var t1 = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = from,
                To = to,
                EasingFunction = easing
            };

            Storyboard.SetTarget(t1, target);
            Storyboard.SetTargetProperty(t1, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");
            var sb = new Storyboard();
            sb.Children.Add(t1);
            return sb;
        }

        public static Storyboard CreateScaleFadeInEffects(UIElement target, TimeSpan beginTime, TimeSpan duration, double opacityFrom = 0, double opacityTo = 1)
        {
            var scaleX = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = new PowerEase(),
                To = 1,
            };

            Storyboard.SetTarget(scaleX, target);
            Storyboard.SetTargetProperty(scaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");

            var scaleY = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = new PowerEase(),
                To = 1,
            };

            Storyboard.SetTarget(scaleY, target);
            Storyboard.SetTargetProperty(scaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            var opacityIn = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = opacityFrom,
                To = opacityTo,
            };

            Storyboard.SetTarget(opacityIn, target);
            Storyboard.SetTargetProperty(opacityIn, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(scaleX);
            sb.Children.Add(scaleY);
            sb.Children.Add(opacityIn);
            return sb;
        }

        public static Storyboard CreateScaleFadeOutEffects(UIElement target, TimeSpan beginTime, TimeSpan duration)
        {
            var scaleX = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = new PowerEase(),
                To = 0,
            };

            Storyboard.SetTarget(scaleX, target);
            Storyboard.SetTargetProperty(scaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");

            var scaleY = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = new PowerEase(),
                To = 0,
            };

            Storyboard.SetTarget(scaleY, target);
            Storyboard.SetTargetProperty(scaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            var opacityIn = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseIn },
                To = 0,
            };

            Storyboard.SetTarget(opacityIn, target);
            Storyboard.SetTargetProperty(opacityIn, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(scaleX);
            sb.Children.Add(scaleY);
            sb.Children.Add(opacityIn);
            return sb;
        }

        public static Storyboard CreateScaleWithFadeEffects(UIElement target, TimeSpan beginTime, TimeSpan duration,
            double scaleFrom, double scaleTo, double opacityFrom, double opacityTo, EasingFunctionBase easing = null)
        {
            var scaleX = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = easing,
                From = scaleFrom,
                To = scaleTo,
            };

            Storyboard.SetTarget(scaleX, target);
            Storyboard.SetTargetProperty(scaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");

            var scaleY = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                EasingFunction = easing,
                From = scaleFrom,
                To = scaleTo,
            };

            Storyboard.SetTarget(scaleY, target);
            Storyboard.SetTargetProperty(scaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            var opacityIn = new DoubleAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                From = opacityFrom,
                To = opacityTo,
            };

            Storyboard.SetTarget(opacityIn, target);
            Storyboard.SetTargetProperty(opacityIn, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(scaleX);
            sb.Children.Add(scaleY);
            sb.Children.Add(opacityIn);
            return sb;
        }

        public static Storyboard CreateShapeFillTransition(UIElement uiElement, TimeSpan beginTime, TimeSpan duration, Color toColor)
        {
            var colorAnimation = new ColorAnimation
            {
                BeginTime = beginTime,
                Duration = duration,
                To = toColor,
            };

            Storyboard.SetTarget(colorAnimation, uiElement);
            Storyboard.SetTargetProperty(colorAnimation, "(UIElement.Fill).(SolidColorBrush.Color)");

            var sb = new Storyboard();
            sb.Children.Add(colorAnimation);
            return sb;
        }

        public static Storyboard CreateColorTransitionEffect(UIElement uiElement, TimeSpan delay, TimeSpan duration, Color toColor, Color fromColor = default (Color))
        {
            var colorAnim = new ColorAnimation { To = toColor, Duration = TimeSpan.FromMilliseconds(500), };
            if (fromColor != default(Color)) colorAnim.From = fromColor;

            Storyboard.SetTarget(colorAnim, uiElement);
            Storyboard.SetTargetProperty(colorAnim, "(UIElement.Foreground).(SolidColorBrush.Color)");

            var sb = new Storyboard();
            sb.Children.Add(colorAnim);
            return sb;
        }
    }
}
