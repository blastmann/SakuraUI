using System;
using Windows.UI.Xaml;
using SakuraUI.WindowsPhone.Utilites;

namespace SakuraUI.WindowsPhone.Controls
{
    public sealed partial class MessageBox
    {
        readonly DialogService _service = new DialogService { AnimationType = DialogService.AnimationTypes.Fast };
        public MessageBox()
        {
            InitializeComponent();

            ContentGrid.Opacity = 0;
            HideStoryboard.Completed += (sender, o) => _service.Hide();

            _service.Child = this;
            _service.Opened += (sender, args) => ShowStoryboard.Begin();
            _service.Closed += Closed;
            _service.BackKeyPressed += (sender, args) =>
            {
                args.Handled = true;
                HideStoryboard.Begin();
            };
        }

        public string Title { get { return TitleBlock.Text; } set { TitleBlock.Text = value; } }
        public string Message { get { return MessageBlock.Text; } set { MessageBlock.Text = value; } }

        public void Show()
        {
            _service.Show();
        }

        public event EventHandler Closed;

        public event EventHandler<object> OkClick;

        private void OnOkClick(object e)
        {
            var handler = OkClick;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<object> CancelClick;

        private void OnCancelClick(object e)
        {
            var handler = CancelClick;
            if (handler != null) handler(this, e);
        }

        private void OkOnClick(object sender, RoutedEventArgs e)
        {
            HideStoryboard.Begin();
            OnOkClick(e);
        }

        private void CancelOnClick(object sender, RoutedEventArgs e)
        {
            HideStoryboard.Begin();
            OnCancelClick(e);
        }
    }
}
