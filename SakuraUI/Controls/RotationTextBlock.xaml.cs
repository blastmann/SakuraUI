using Windows.UI.Xaml;

namespace SakuraUI.Controls
{
    public sealed partial class RotationTextBlock
    {
        public RotationTextBlock()
        {
            InitializeComponent();

            HideStoryboard.Completed += (sender, o) =>
            {
                RotationTextContainer.Text = Text;
                ShowStoryboard.Begin();
            };
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(RotationTextBlock), new PropertyMetadata(string.Empty, OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var me = (RotationTextBlock)d;
            if (string.IsNullOrEmpty(me.Text)) return;
            if (args.NewValue != null && args.NewValue.Equals(args.OldValue)) return;

            me.HideStoryboard.Begin();
        }

        public string Text { get { return (string)GetValue(TextProperty); } set { SetValue(TextProperty, value); } }
    }
}
