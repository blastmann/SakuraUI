using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraUI.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ThemePages : Page
    {
        public ThemePages()
        {
            this.InitializeComponent();
        }

        private void BlueOnClick(object sender, RoutedEventArgs e)
        {
            App.Theme.BackgroundColor = Colors.DodgerBlue;
        }

        private void PinkOnClick(object sender, RoutedEventArgs e)
        {
            App.Theme.BackgroundColor = Colors.HotPink;
        }

        private void GreenYellowOnClick(object sender, RoutedEventArgs e)
        {
            App.Theme.BackgroundColor = Colors.GreenYellow;
        }

        private void OrangeRedOnClick(object sender, RoutedEventArgs e)
        {
            App.Theme.BackgroundColor = Colors.OrangeRed;
        }

        private void WhiteOnClick(object sender, RoutedEventArgs e)
        {
            App.Theme.BackgroundColor = Colors.White;
        }
    }
}
