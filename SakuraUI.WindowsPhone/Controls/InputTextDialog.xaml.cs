using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using SakuraUI.WindowsPhone.Utilites;

namespace SakuraUI.WindowsPhone.Controls
{
    public sealed partial class InputBox
    {
        private readonly DialogService _service = new DialogService { AnimationType = DialogService.AnimationTypes.Fast };

        public InputBox()
        {
            InitializeComponent();
            ContentGrid.Opacity = 0;

            _service.Child = this;
            _service.Opened += (sender, args) => ShowStoryboard.Begin();
            _service.BackKeyPressed += (sender, args) => { args.Handled = true; HideStoryboard.Begin(); };

            ShowStoryboard.Completed += (sender, o) => InputTextBox.Focus(FocusState.Programmatic);
            HideStoryboard.Completed += (sender, o) => _service.Hide();

            InputTextBox.KeyDown += (sender, args) => { if (args.Key != VirtualKey.Enter) return; OkOnClick(null, null); };
        }

        public void Show()
        {
            _service.Show();
        }

        public string Title { get { return TitleBlock.Text; } set { TitleBlock.Text = value; } }
        public string Message { get { return MessageBlock.Text; } set { MessageBlock.Text = value; } }
        public int MaxLength { get { return InputTextBox.MaxLength; } set { InputTextBox.MaxLength = value; } }

        public event EventHandler<string> OkClick;

        private void OnOkClick(string e)
        {
            var handler = OkClick;
            if (handler != null) handler(this, e);
        }

        private async void OkOnClick(object sender, RoutedEventArgs e)
        {
            HideStoryboard.Begin();
            OnOkClick(InputTextBox.Text.Trim());
        }

        private void CancelOnClick(object sender, RoutedEventArgs e)
        {
            HideStoryboard.Begin();
        }
    }
}
