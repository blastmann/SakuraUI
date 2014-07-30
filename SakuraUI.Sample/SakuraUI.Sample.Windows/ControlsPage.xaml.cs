using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SakuraUI.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ControlsPage : Page
    {
        public ControlsPage()
        {
            this.InitializeComponent();
        }

        private void CurrentTimeOnClick(object sender, RoutedEventArgs e)
        {
            FlyBlock.NewText = DateTime.Now.ToString();
        }

        private void RotationTextOnClick(object sender, RoutedEventArgs e)
        {
            RotationTextBlock.Text = (RotationTextBlock.Text.Length > 12) ? "Hello world" : DateTime.Now.ToString();
        }
    }
}
