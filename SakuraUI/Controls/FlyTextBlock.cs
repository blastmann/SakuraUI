using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace SakuraUI.Controls
{
    public sealed partial class FlyTextBlock : Control
    {
        public FlyTextBlock()
        {
            DefaultStyleKey = typeof(FlyTextBlock);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _newTextBlock = (TextBlock)GetTemplateChild("NewTextBlock");
            _oldTextBlock = (TextBlock)GetTemplateChild("OldTextBlock");
            _boundary = (RectangleGeometry)GetTemplateChild("Boundary");

            if (IsTextTrimming)
            {
                _newTextBlock.TextTrimming = TextTrimming.WordEllipsis;
                _oldTextBlock.TextTrimming = TextTrimming.WordEllipsis;
            }

            if (Foreground != null) { _newTextBlock.Foreground = _oldTextBlock.Foreground = Foreground; }
            _boundary.Rect = (new Rect(0.0, 0.0, Window.Current.Bounds.Width, BoundaryHeight));

            InitilazingAnimation();

            if (_newValue != null)
            {
                SetValue(NewTextProperty, _newValue + string.Empty);
            }
        }

        private TextBlock _oldTextBlock;
        private TextBlock _newTextBlock;
        private RectangleGeometry _boundary;
        private object _newValue;
        private TimeSpan _animationDuration = TimeSpan.FromMilliseconds(250);

        public static readonly DependencyProperty NewTextProperty = DependencyProperty.Register("NewText", typeof(string), typeof(FlyTextBlock), new PropertyMetadata(string.Empty, TextChangedCallback));
        public static readonly DependencyProperty OldTextProperty = DependencyProperty.Register("OldText", typeof(string), typeof(FlyTextBlock), null);
        public static readonly DependencyProperty BoundaryHeightProperty = DependencyProperty.Register("BoundaryHeight", typeof(double), typeof(FlyTextBlock), new PropertyMetadata(20.2));

        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = (FlyTextBlock)d;
            if (e.OldValue != null)
            {
                me.OldText = e.OldValue.ToString();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(me.OldText))
                {
                    me.OldText = e.NewValue.ToString();
                }
            }

            me.NewText = e.NewValue.ToString();
            if (me._newTextBlock != null && me._oldTextBlock != null)
            {
                me._newValue = null;
                me.RollingUpAnimation();
                me.InvalidateMeasure();
                me.UpdateLayout();
                return;
            }
            me._newValue = e.NewValue;
        }

        public string NewText
        {
            get { return (string)GetValue(NewTextProperty); }
            set { SetValue(NewTextProperty, value); }
        }

        public string OldText
        {
            get { return (string)GetValue(OldTextProperty); }
            set { SetValue(OldTextProperty, value); }
        }

        public double BoundaryHeight
        {
            get { return (double)GetValue(BoundaryHeightProperty); }
            set { SetValue(BoundaryHeightProperty, value); }
        }

        public bool IsTextTrimming
        {
            get;
            set;
        }

        public TimeSpan AnimationDuration
        {
            get { return _animationDuration; }
            set { _animationDuration = value; }
        }

    }

    public partial class FlyTextBlock
    {
        private readonly EasingFunctionBase _acceFunction = new PowerEase { EasingMode = EasingMode.EaseIn };

        private void InitilazingAnimation()
        {
            _newTextBlock.Opacity = (0.0);
            _oldTextBlock.RenderTransform = (new CompositeTransform());
            var doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();

            var easingDoubleKeyFrame = new EasingDoubleKeyFrame { KeyTime = (TimeSpan.Zero), Value = (0.0), };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, _oldTextBlock);
            var storyboard = new Storyboard { Duration = AnimationDuration };
            storyboard.Children.Add(doubleAnimationUsingKeyFrames);
            storyboard.Begin();
        }

        private void RollingUpAnimation()
        {
            _oldTextBlock.RenderTransform = (new CompositeTransform());
            var doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();

            var easingDoubleKeyFrame = new EasingDoubleKeyFrame
            {
                KeyTime = (TimeSpan.Zero),
                Value = 0.0,
            };
            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame);

            var easingDoubleKeyFrame2 = new EasingDoubleKeyFrame
            {
                KeyTime = AnimationDuration,
                Value = (_newTextBlock.ActualHeight * -1.0),
                EasingFunction = _acceFunction,
            };

            doubleAnimationUsingKeyFrames.KeyFrames.Add(easingDoubleKeyFrame2);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTarget(doubleAnimationUsingKeyFrames, _oldTextBlock);
            var storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimationUsingKeyFrames);
            storyboard.Begin();

            _newTextBlock.Opacity = (1.0);
            _newTextBlock.RenderTransform = (new CompositeTransform());
            var doubleAnimationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();

            var easingDoubleKeyFrame3 = new EasingDoubleKeyFrame
            {
                KeyTime = (TimeSpan.Zero),
                Value = (BoundaryHeight)
            };

            doubleAnimationUsingKeyFrames2.KeyFrames.Add(easingDoubleKeyFrame3);
            var easingDoubleKeyFrame4 = new EasingDoubleKeyFrame
            {
                KeyTime = AnimationDuration,
                EasingFunction = _acceFunction,
                Value = 0.0,
            };

            doubleAnimationUsingKeyFrames2.KeyFrames.Add(easingDoubleKeyFrame4);
            Storyboard.SetTargetProperty(doubleAnimationUsingKeyFrames2, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTarget(doubleAnimationUsingKeyFrames2, _newTextBlock);
            var storyboard2 = new Storyboard();
            storyboard2.Children.Add(doubleAnimationUsingKeyFrames2);
            storyboard2.Begin();
        }
    }
}
