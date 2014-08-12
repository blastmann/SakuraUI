using System;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace SakuraUI.WindowsPhone.Utilites
{
    public class DialogService
    {
        private static int _showCount;
        public static bool IsDialogShow
        {
            get
            {
                return _showCount != 0;
            }
            private set
            {
                if (value) _showCount++;
                else _showCount--;
            }
        }

        public FrameworkElement Child { get; set; }
        public bool IsOpen { get; private set; }
        public AnimationTypes AnimationType { get; set; }

        public event EventHandler Closed;
        public event EventHandler Opened;
        public event EventHandler<BackPressedEventArgs> BackKeyPressed;

        protected virtual void OnBackKeyPressed(BackPressedEventArgs e)
        {
            EventHandler<BackPressedEventArgs> handler = BackKeyPressed;
            if (handler != null) handler(this, e);
        }

        private Frame _rootVisual;
        internal Frame RootVisual { get { return _rootVisual ?? (_rootVisual = Window.Current.Content as Frame); } }

        private Panel _popupContainer;
        private Grid _childPanel;
        private bool _deferredShowToLoaded;

        internal Panel PopupContainer
        {
            get
            {
                if (_popupContainer != null) return _popupContainer;

                var presenters = RootVisual.GetLogicalChildrenByType<ContentPresenter>(false).ToList();
                for (var i = 0; i < presenters.Count; i++)
                {
                    var panels = presenters.ElementAt(i).GetLogicalChildrenByType<Panel>(false).ToList();
                    if (panels.Any() == false) continue;
                    _popupContainer = panels.First();
                    break;
                }
                return _popupContainer;
            }
        }

        private void InitializePopup()
        {
            // Add overlay which is the size of RootVisual
            _childPanel = CreateGrid();

            // Initialize popup to draw the context menu over all controls
            if (PopupContainer != null)
            {
                PopupContainer.Children.Add(_childPanel);
                _childPanel.Children.Add(Child);
            }
            else
            {
                _deferredShowToLoaded = true;
                RootVisual.Loaded += RootVisualDeferredShowLoaded;
            }
        }

        void RootVisualDeferredShowLoaded(object sender, RoutedEventArgs e)
        {
            RootVisual.Loaded -= RootVisualDeferredShowLoaded;
            _deferredShowToLoaded = false;

            Show();
        }

        public void Show()
        {
            IsOpen = true;

            InitializePopup();

            if (_deferredShowToLoaded)
                return;

            IsDialogShow = true;
            RunShowStoryboard(_childPanel, AnimationType, TimeSpan.Zero, () =>
            {
                if (Opened != null) Opened.Invoke(this, null);
            });

            HardwareButtons.BackPressed += BackKeyPress;
        }

        private void BackKeyPress(object sender, BackPressedEventArgs e)
        {
            OnBackKeyPressed(e);
            if (e.Handled) return;
            e.Handled = true;
            Hide();
        }

        public void Hide()
        {
            if (!IsOpen)
                return;

            IsDialogShow = false;
            RunHideStoryboard(_childPanel, AnimationType);
        }

        private Grid CreateGrid()
        {
            var grid = new Grid { Name = Guid.NewGuid().ToString(), Opacity = 0 };

            Grid.SetColumnSpan(grid, int.MaxValue);
            Grid.SetRowSpan(grid, int.MaxValue);

            return grid;
        }

        private void RunShowStoryboard(UIElement grid, AnimationTypes animation, TimeSpan delay, Action completedAction = null)
        {
            if (grid == null)
                return;

            Storyboard storyboard;
            switch (animation)
            {
                case AnimationTypes.SlideHorizontal:
                    storyboard = XamlReader.Load(SlideHorizontalInStoryboard) as Storyboard;
                    grid.RenderTransform = new TranslateTransform();
                    break;
                case AnimationTypes.Fast:
                    storyboard = XamlReader.Load(FastInStoryboard) as Storyboard;
                    break;
                case AnimationTypes.Slide:
                    storyboard = XamlReader.Load(SlideUpStoryboard) as Storyboard;
                    grid.RenderTransform = new TranslateTransform();
                    break;
                case AnimationTypes.Fade:
                    storyboard = XamlReader.Load(FadeInStoryboard) as Storyboard;
                    break;
                default:
                    storyboard = XamlReader.Load(SwivelInStoryboard) as Storyboard;
                    grid.Projection = new PlaneProjection();
                    break;
            }

            if (storyboard != null)
            {
                foreach (var storyboardAnimation in storyboard.Children)
                {
                    if (!(storyboardAnimation is DoubleAnimationUsingKeyFrames))
                        continue;

                    var doubleKey = storyboardAnimation as DoubleAnimationUsingKeyFrames;

                    foreach (var frame in doubleKey.KeyFrames)
                    {
                        frame.KeyTime = KeyTime.FromTimeSpan(frame.KeyTime.TimeSpan.Add(delay));
                    }
                }

                PopupContainer.Dispatcher.RunIdleAsync(args =>
                {
                    foreach (var t in storyboard.Children)
                        Storyboard.SetTarget(t, grid);

                    if (completedAction != null) storyboard.Completed += (sender, o) => completedAction();
                    storyboard.Begin();
                });
            }
        }
        void RunHideStoryboard(Grid grid, AnimationTypes animation)
        {
            if (grid == null)
                return;

            Storyboard storyboard;

            switch (animation)
            {
                case AnimationTypes.SlideHorizontal:
                    storyboard = XamlReader.Load(SlideHorizontalOutStoryboard) as Storyboard;
                    break;
                case AnimationTypes.Slide:
                    storyboard = XamlReader.Load(SlideDownStoryboard) as Storyboard;
                    break;
                case AnimationTypes.Fade:
                    storyboard = XamlReader.Load(FadeOutStoryboard) as Storyboard;
                    break;
                case AnimationTypes.Fast:
                    storyboard = XamlReader.Load(FastOutStoryboard) as Storyboard;
                    break;
                default:
                    storyboard = XamlReader.Load(SwivelOutStoryboard) as Storyboard;
                    break;
            }

            try
            {
                if (storyboard == null) return;

                storyboard.Completed += HideStoryboardCompleted;

                foreach (var t in storyboard.Children)
                    Storyboard.SetTarget(t, grid);

                storyboard.Begin();
            }
            catch (Exception)
            {
                // chances are user nav'ed away
                // attempting to be extremely robust here
                // if this fails, go straight to complete
                // and attempt to remove it from the visual tree
                HideStoryboardCompleted(null, null);
            }
        }

        void HideStoryboardCompleted(object sender, object o)
        {
            IsOpen = false;
            HardwareButtons.BackPressed -= BackKeyPress;

            try
            {
                if (PopupContainer != null && PopupContainer.Children != null)
                {
                    PopupContainer.Children.Remove(_childPanel);
                }

                _childPanel.Children.Clear();
            }
            catch
            {
                // chances are user nav'ed away
                // attempting to be extremely robust here
                // if this fails, go straight to complete
                // and attempt to remove it from the visual tree
            }

            try
            {
                if (Closed != null)
                    Closed(this, null);

            }
            catch
            {
                // chances are user nav'ed away
                // attempting to be extremely robust here
                // if this fails, go straight to complete
                // and attempt to remove it from the visual tree
            }
        }


        public enum AnimationTypes
        {
            Slide,
            SlideHorizontal,
            Swivel,
            SwivelHorizontal,
            Fade,
            Fast,
        }

        private const string SlideUpStoryboard = @"
        <Storyboard  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.RenderTransform).(TranslateTransform.Y)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""150""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:0.35"" Value=""0"">
                    <EasingDoubleKeyFrame.EasingFunction>
                        <ExponentialEase EasingMode=""EaseOut"" Exponent=""6""/>
                    </EasingDoubleKeyFrame.EasingFunction>
                </EasingDoubleKeyFrame>
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimation Storyboard.TargetProperty=""(UIElement.Opacity)"" From=""0"" To=""1"" Duration=""0:0:0.350"">
                <DoubleAnimation.EasingFunction>
                    <ExponentialEase EasingMode=""EaseOut"" Exponent=""6""/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>";

        private const string SlideHorizontalInStoryboard = @"
        <Storyboard  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.RenderTransform).(TranslateTransform.X)"" >
                    <EasingDoubleKeyFrame KeyTime=""0"" Value=""-150""/>
                    <EasingDoubleKeyFrame KeyTime=""0:0:0.35"" Value=""0"">
                        <EasingDoubleKeyFrame.EasingFunction>
                            <ExponentialEase EasingMode=""EaseOut"" Exponent=""6""/>
                        </EasingDoubleKeyFrame.EasingFunction>
                    </EasingDoubleKeyFrame>
                </DoubleAnimationUsingKeyFrames>
            <DoubleAnimation Storyboard.TargetProperty=""(UIElement.Opacity)"" From=""0"" To=""1"" Duration=""0:0:0.350"" >
                <DoubleAnimation.EasingFunction>
                    <ExponentialEase EasingMode=""EaseOut"" Exponent=""6""/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>";

        private const string SlideHorizontalOutStoryboard = @"
        <Storyboard  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.RenderTransform).(TranslateTransform.X)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""0""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:0.25"" Value=""150"">
                    <EasingDoubleKeyFrame.EasingFunction>
                        <ExponentialEase EasingMode=""EaseIn"" Exponent=""6""/>
                    </EasingDoubleKeyFrame.EasingFunction>
                </EasingDoubleKeyFrame>
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimation Storyboard.TargetProperty=""(UIElement.Opacity)"" From=""1"" To=""0"" Duration=""0:0:0.25"">
                <DoubleAnimation.EasingFunction>
                    <ExponentialEase EasingMode=""EaseIn"" Exponent=""6""/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>";

        private const string SlideDownStoryboard = @"
        <Storyboard  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.RenderTransform).(TranslateTransform.Y)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""0""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:0.25"" Value=""150"">
                    <EasingDoubleKeyFrame.EasingFunction>
                        <ExponentialEase EasingMode=""EaseIn"" Exponent=""6""/>
                    </EasingDoubleKeyFrame.EasingFunction>
                </EasingDoubleKeyFrame>
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimation Storyboard.TargetProperty=""(UIElement.Opacity)"" From=""1"" To=""0"" Duration=""0:0:0.25"">
                <DoubleAnimation.EasingFunction>
                    <ExponentialEase EasingMode=""EaseIn"" Exponent=""6""/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>";

        private const string SwivelInStoryboard =
        @"<Storyboard xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimation 
				To="".5""
                Storyboard.TargetProperty=""(UIElement.Projection).(PlaneProjection.CenterOfRotationY)"" />
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.Projection).(PlaneProjection.RotationX)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""-30""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:0.35"" Value=""0"">
                    <EasingDoubleKeyFrame.EasingFunction>
                        <ExponentialEase EasingMode=""EaseOut"" Exponent=""6""/>
                    </EasingDoubleKeyFrame.EasingFunction>
                </EasingDoubleKeyFrame>
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.Opacity)"">
                <DiscreteDoubleKeyFrame KeyTime=""0"" Value=""1"" />
            </DoubleAnimationUsingKeyFrames>
        </Storyboard>";

        private const string SwivelOutStoryboard =
        @"<Storyboard xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimation BeginTime=""0:0:0"" Duration=""0"" 
                                Storyboard.TargetProperty=""(UIElement.Projection).(PlaneProjection.CenterOfRotationY)"" 
                                To="".5""/>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.Projection).(PlaneProjection.RotationX)"">
                <EasingDoubleKeyFrame KeyTime=""0"" Value=""0""/>
                <EasingDoubleKeyFrame KeyTime=""0:0:0.25"" Value=""45"">
                    <EasingDoubleKeyFrame.EasingFunction>
                        <ExponentialEase EasingMode=""EaseIn"" Exponent=""6""/>
                    </EasingDoubleKeyFrame.EasingFunction>
                </EasingDoubleKeyFrame>
            </DoubleAnimationUsingKeyFrames>
            <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty=""(UIElement.Opacity)"">
                <DiscreteDoubleKeyFrame KeyTime=""0"" Value=""1"" />
                <DiscreteDoubleKeyFrame KeyTime=""0:0:0.267"" Value=""0"" />
            </DoubleAnimationUsingKeyFrames>
        </Storyboard>";

        private const string FadeInStoryboard =
        @"<Storyboard xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimation 
				Duration=""0:0:0.2"" 
				Storyboard.TargetProperty=""(UIElement.Opacity)"" 
                To=""1""/>
        </Storyboard>";

        private const string FadeOutStoryboard =
        @"<Storyboard xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimation 
				Duration=""0:0:0.2""
				Storyboard.TargetProperty=""(UIElement.Opacity)"" 
                To=""0""/>
        </Storyboard>";

        private const string FastOutStoryboard =
             @"<Storyboard xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimation 
				Duration=""0:0:0.001"" 
				Storyboard.TargetProperty=""(UIElement.Opacity)"" 
                To=""0""/>
        </Storyboard>";

        private const string FastInStoryboard =
            @"<Storyboard xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            <DoubleAnimation 
				Duration=""0:0:0.001"" 
				Storyboard.TargetProperty=""(UIElement.Opacity)"" 
                To=""1""/>
        </Storyboard>";
    }
}
