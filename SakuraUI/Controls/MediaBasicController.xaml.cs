using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace SakuraUI.Controls
{
    public sealed partial class MediaBasicController
    {
        public MediaBasicController()
        {
            InitializeComponent();

            TurnToPlayStoryboard.Completed += (sender, args) =>
            {
                PlayButton.IsEnabled = true;
                PauseButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                PreviousButton.IsEnabled = true;
            };

            TurnToPauseStoryboard.Completed += (sender, args) =>
            {
                PlayButton.IsEnabled = true;
                PauseButton.IsEnabled = true;
                NextButton.IsEnabled = true;
                PreviousButton.IsEnabled = true;
            };
        }

    }

    public partial class MediaBasicController
    {
        public static readonly DependencyProperty PlayCommandProperty = DependencyProperty.Register("PlayCommand", typeof(ICommand), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty PauseCommandProperty = DependencyProperty.Register("PauseCommand", typeof(ICommand), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty MoveNextCommandProperty = DependencyProperty.Register("MoveNextCommand", typeof(ICommand), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty MovePreviousCommandProperty = DependencyProperty.Register("MovePreviousCommand", typeof(ICommand), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty FastForwardCommandProperty = DependencyProperty.Register("FastForwardCommand", typeof(ICommand), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty FastRewindCommandProperty = DependencyProperty.Register("FastRewindCommand", typeof(ICommand), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty PlayCommandParameterProperty = DependencyProperty.Register("PlayCommandParameter", typeof(object), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty PauseCommandParameterProperty = DependencyProperty.Register("PauseCommandParameter", typeof(object), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty MoveNextCommandParameterProperty = DependencyProperty.Register("MoveNextCommandParameter", typeof(object), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty MovePreviousCommandParameterProperty = DependencyProperty.Register("MovePreviousCommandParameter", typeof(object), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty FastForwardCommandParameterProperty = DependencyProperty.Register("FastForwardCommandParameter", typeof(object), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty FastRewindCommandParameterProperty = DependencyProperty.Register("FastRewindCommandParameter", typeof(object), typeof(MediaBasicController), new PropertyMetadata(null));
        public static readonly DependencyProperty MediaStateProperty = DependencyProperty.Register("MediaState", typeof(MediaElementState), typeof(MediaBasicController), new PropertyMetadata(MediaElementState.Stopped, MediaStateChangedCallback));
        public static new readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(MediaBasicController), new PropertyMetadata(new SolidColorBrush(Colors.LightPink), ForegroundChangedBrush));
        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.Register("ForegroundColor", typeof(Color), typeof(MediaBasicController), new PropertyMetadata(Colors.LightPink, null));
        private static void ForegroundChangedBrush(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var me = (MediaBasicController)d;
            var brush = (SolidColorBrush)args.NewValue;
            me.PlayButton.Foreground = me.PauseButton.Foreground = me.NextButton.Foreground = me.PreviousButton.Foreground = brush;
            me.ForegroundColor = brush.Color;
        }

        private MediaElementState _lastAvailableState = MediaElementState.Stopped;
        private static void MediaStateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var me = (MediaBasicController)d;
            if (args.NewValue == null) return;
            var state = (MediaElementState)args.NewValue;

            if (state == MediaElementState.Buffering || state == MediaElementState.Opening || state == MediaElementState.Closed) return;

            if (state == MediaElementState.Playing && me._lastAvailableState != MediaElementState.Playing)
            {
                me.TurnToPauseStoryboard.Begin();
                me._lastAvailableState = state;
                return;
            }

            if (state == MediaElementState.Playing) return;
            me.TurnToPlayStoryboard.Begin();
            me._lastAvailableState = state;
        }

        public MediaElementState MediaState
        {
            get { return (MediaElementState)GetValue(MediaStateProperty); }
            set { SetValue(MediaStateProperty, value); }
        }

        public ICommand PlayCommand
        {
            get { return (ICommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }

        public ICommand PauseCommand
        {
            get { return (ICommand)GetValue(PauseCommandProperty); }
            set { SetValue(PauseCommandProperty, value); }
        }

        public ICommand MoveNextCommand
        {
            get { return (ICommand)GetValue(MoveNextCommandProperty); }
            set { SetValue(MoveNextCommandProperty, value); }
        }

        public ICommand MovePreviousCommand
        {
            get { return (ICommand)GetValue(MovePreviousCommandProperty); }
            set { SetValue(MovePreviousCommandProperty, value); }
        }

        public ICommand FastForwardCommand
        {
            get { return (ICommand)GetValue(FastForwardCommandProperty); }
            set { SetValue(FastForwardCommandProperty, value); }
        }

        public ICommand FastRewindCommand
        {
            get { return (ICommand)GetValue(FastRewindCommandProperty); }
            set { SetValue(FastRewindCommandProperty, value); }
        }

        public object PlayCommandParameter
        {
            get { return GetValue(PlayCommandParameterProperty); }
            set { SetValue(PlayCommandParameterProperty, value); }
        }

        public object PauseCommandParameter
        {
            get { return GetValue(PauseCommandParameterProperty); }
            set { SetValue(PauseCommandParameterProperty, value); }
        }

        public object MoveNextCommandParameter
        {
            get { return GetValue(MoveNextCommandParameterProperty); }
            set { SetValue(MoveNextCommandParameterProperty, value); }
        }

        public object MovePreviousCommandParameter
        {
            get { return GetValue(MovePreviousCommandParameterProperty); }
            set { SetValue(MovePreviousCommandParameterProperty, value); }
        }

        public object FastForwardCommandParameter
        {
            get { return GetValue(FastForwardCommandParameterProperty); }
            set { SetValue(FastForwardCommandParameterProperty, value); }
        }

        public object FastRewindCommandParameter
        {
            get { return GetValue(FastRewindCommandParameterProperty); }
            set { SetValue(FastRewindCommandParameterProperty, value); }
        }

        public Color ForegroundColor { get { return (Color)GetValue(ForegroundColorProperty); } set { SetValue(ForegroundColorProperty, value); } }
        public new Brush Foreground { get { return (Brush)GetValue(ForegroundProperty); } set { SetValue(ForegroundProperty, value); } }
    }

    public partial class MediaBasicController
    {
        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PauseCommand == null) return;
            PauseCommand.Execute(PauseCommandParameter);
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PlayCommand == null) return;
            PlayCommand.Execute(PlayCommandParameter);
        }

        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MovePreviousCommand == null) return;
            MovePreviousCommand.Execute(MovePreviousCommandParameter);
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MoveNextCommand == null) return;
            MoveNextCommand.Execute(MoveNextCommandParameter);
        }

        private void NextButton_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (FastForwardCommand == null) return;
            FastForwardCommand.Execute(FastForwardCommandParameter);
        }

        private void PreviousButton_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (FastRewindCommand == null) return;
            FastRewindCommand.Execute(FastRewindCommandParameter);
        }
    }
}
