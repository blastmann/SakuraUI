using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Windows.Globalization.Fonts;
using Windows.UI;
using Windows.UI.Xaml.Media;
using SakuraUI.Annotations;

namespace SakuraUI.Themes
{
    public partial class ReactiveTheme
    {
        #region Private Brushes
        private const byte Opacity = 0x88;
        private static Brush _accentBrush;
        private static Brush _secondaryAccentBrush;
        private static Brush _accentTransparentBrush;
        private static Brush _pointerOverBrush;
        private static Brush _backgroundBrush;
        private static Brush _secondaryBackgroundBrush;
        private static Brush _foregroundBrush;
        private static Brush _foregroundTextBrush;
        private static Brush _contrastBackgroundBrush;
        private static Brush _contrastAccentBrush;
        private static Brush _subtleBrush = new SolidColorBrush(Color.FromArgb(0x08, 0x0, 0x0, 0x0));
        private static Brush _chromeBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x1F, 0x1F, 0x1F));
        private static Brush _disableBrush;
        private static FontFamily _appFontFamily = new FontFamily("Segoe UI");
        public readonly Brush SemiTransparentBrush = new SolidColorBrush(Color.FromArgb(0xAA, 0, 0, 0));
        public static readonly Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);
        #endregion

        public FontFamily AppFontFamily
        {
            get { return _appFontFamily; }
            set { RaiseAndSetIfChanged(ref _appFontFamily, value); }
        }

        public Brush AccentBrush
        {
            get { return _accentBrush; }
            set { RaiseAndSetIfChanged(ref _accentBrush, value); }
        }

        public Brush SecondaryAccentBrush
        {
            get { return _secondaryAccentBrush; }
            set { RaiseAndSetIfChanged(ref _secondaryAccentBrush, value); }
        }

        public Brush AccentTransparentBrush
        {
            get { return _accentTransparentBrush; }
            set { RaiseAndSetIfChanged(ref _accentTransparentBrush, value); }
        }

        public Brush PointerOverBrush
        {
            get { return _pointerOverBrush; }
            set { RaiseAndSetIfChanged(ref _pointerOverBrush, value); }
        }

        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { RaiseAndSetIfChanged(ref _backgroundBrush, value); }
        }

        public Brush SecondaryBackgroundBrush
        {
            get { return _secondaryBackgroundBrush; }
            set { RaiseAndSetIfChanged(ref _secondaryBackgroundBrush, value); }
        }

        public Brush ForegroundBrush
        {
            get { return _foregroundBrush; }
            set { RaiseAndSetIfChanged(ref _foregroundBrush, value); }
        }

        public Brush ForegroundTextBrush
        {
            get { return _foregroundTextBrush; }
            set { RaiseAndSetIfChanged(ref _foregroundTextBrush, value); }
        }

        public Brush ContrastBackgroundBrush
        {
            get { return _contrastBackgroundBrush; }
            set { RaiseAndSetIfChanged(ref _contrastBackgroundBrush, value); }
        }

        public Brush ContrastAccentBrush
        {
            get { return _contrastAccentBrush; }
            set { RaiseAndSetIfChanged(ref _contrastAccentBrush, value); }
        }

        public Brush SubtleBrush
        {
            get { return _subtleBrush; }
            set { RaiseAndSetIfChanged(ref _subtleBrush, value); }
        }

        public Brush DisableBrush
        {
            get { return _disableBrush; }
            set { RaiseAndSetIfChanged(ref _disableBrush, value); }
        }

        public Brush ChromeBrush
        {
            get { return _chromeBrush; }
            set { RaiseAndSetIfChanged(ref _chromeBrush, value); }
        }

        public Brush RandomColorBrush
        {
            get
            {
                var random = new Random((int)(DateTime.Now.Ticks * (DateTime.Now.Second + 1)));
                return new SolidColorBrush(Color.FromArgb(0xFF, (byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
            }
        }

        public Color AccentColor
        {
            get { return ((SolidColorBrush)AccentBrush).Color; }
            set
            {
                AccentBrush = new SolidColorBrush(value);
                AccentTransparentColor = Color.FromArgb(Opacity, value.R, value.G, value.B);
                RaisePropertyChanged();
            }
        }

        public Color AccentTransparentColor
        {
            get { return ((SolidColorBrush)AccentTransparentBrush).Color; }
            private set
            {
                AccentTransparentBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color SecondaryAccentColor
        {
            get { return ((SolidColorBrush)SecondaryAccentBrush).Color; }
            set
            {
                SecondaryAccentBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color PointerOverColor
        {
            get { return ((SolidColorBrush)PointerOverBrush).Color; }
            set
            {
                PointerOverBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color BackgroundColor
        {
            get { return ((SolidColorBrush)BackgroundBrush).Color; }
            set
            {
                BackgroundBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color SecondaryBackgroundColor
        {
            get { return ((SolidColorBrush)SecondaryBackgroundBrush).Color; }
            set
            {
                SecondaryBackgroundBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color ForegroundColor
        {
            get { return ((SolidColorBrush)ForegroundBrush).Color; }
            set
            {
                ForegroundBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color ForegroundTextColor
        {
            get { return ((SolidColorBrush)ForegroundTextBrush).Color; }
            set
            {
                ForegroundTextBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color ContrastBackgroundColor
        {
            get { return ((SolidColorBrush)ContrastBackgroundBrush).Color; }
            set
            {
                ContrastBackgroundBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color ContrastAccentColor
        {
            get { return ((SolidColorBrush)ContrastAccentBrush).Color; }
            set
            {
                ContrastAccentBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color SubtleColor
        {
            get { return ((SolidColorBrush)SubtleBrush).Color; }
            set
            {
                SubtleBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color DisableColor
        {
            get { return ((SolidColorBrush)DisableBrush).Color; }
            set
            {
                DisableBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color ChromeColor
        {
            get { return ((SolidColorBrush)ChromeBrush).Color; }
            set
            {
                ChromeBrush = new SolidColorBrush(value);
                RaisePropertyChanged();
            }
        }

        public Color SemiTransparentColor { get { return ((SolidColorBrush)SemiTransparentBrush).Color; } }

        static ReactiveTheme()
        {
            InitDefaultTheme();
        }

        static void InitDefaultTheme()
        {
            var accentColor = Color.FromArgb(255, 0xFF, 0xB6, 0xC1);
            var secondaryAccentColor = accentColor;
            var backgroundColor = Colors.White;
            var secondaryBackgroundColor = backgroundColor;
            var foregroundColor = Colors.Black;
            var foregroundTextColor = Color.FromArgb(0xC8, 80, 80, 80);
            var disableColor = Color.FromArgb(0x66, 0x00, 0x00, 0x00);

            _accentBrush = new SolidColorBrush(accentColor);
            _accentTransparentBrush = new SolidColorBrush(Color.FromArgb(Opacity, accentColor.R, accentColor.G, accentColor.B));
            _secondaryAccentBrush = new SolidColorBrush(secondaryAccentColor);
            _backgroundBrush = new SolidColorBrush(backgroundColor);
            _secondaryBackgroundBrush = new SolidColorBrush(secondaryBackgroundColor);
            _foregroundBrush = new SolidColorBrush(foregroundColor);
            _foregroundTextBrush = new SolidColorBrush(foregroundTextColor);
            _disableBrush = new SolidColorBrush(disableColor);
            _pointerOverBrush = new SolidColorBrush(accentColor);
            _subtleBrush = new SolidColorBrush(Color.FromArgb(72, 72, 72, 72));
            _contrastAccentBrush = new SolidColorBrush(Colors.White);

            var g = new LanguageFontGroup(CultureInfo.CurrentUICulture.Name);
            _appFontFamily = new FontFamily(g.UITextFont.FontFamily);
        }
    }

    public partial class ReactiveTheme : INotifyPropertyChanged
    {
        private T RaiseAndSetIfChanged<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return newValue;

            oldValue = newValue;
            OnPropertyChanged(propertyName);
            return newValue;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
