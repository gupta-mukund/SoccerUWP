using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Soccer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        App _AppReference = App.Current as App;
        public HomePage()
        {
            this.InitializeComponent();


        }

        private void ListViewItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = (sender as ListView).SelectedItem.ToString();
            Service.CurrentTeam = value;
            _AppReference.service.Players = new List<Player>(_AppReference.service.Teams[value]);
            ListViewPlayers.ItemsSource = _AppReference.service.Players;
        }
    }
}
