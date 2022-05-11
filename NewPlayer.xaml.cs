using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class NewPlayer : Page
    {
        public NewPlayer()
        {
            this.InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var photoUrl = PhotoUrl.Text.Trim();
            var name = Name.Text.Trim();
            var nationality = Nationality.Text.Trim();
            var team = Team.Text.Trim();
            var gender = Gender.SelectedValue == null ? "" : Gender.SelectedValue.ToString();
            var titleMessage = "Data not valid";

            string msg = string.IsNullOrWhiteSpace(photoUrl) ? "PhotoUrl" :
                string.IsNullOrWhiteSpace(name) ? "Name" :
                string.IsNullOrWhiteSpace(nationality) ? "Nationality" :
                string.IsNullOrWhiteSpace(team) ? "team" :
                string.IsNullOrWhiteSpace(gender) ? "Gender" : "";

            if (!string.IsNullOrWhiteSpace(msg))
            {
                await new MessageDialog(titleMessage, $"{msg} is empty").ShowAsync();
                return;
            }

            string LineToWrite = $"{nationality};{photoUrl};{name};{team};{gender}";
            File.AppendAllText(Service.FileUrl, LineToWrite);
            (App.Current as App).service.UpdatingData(new Player { Name = name, Nationality = nationality, Image = photoUrl, Team = team, Gender = gender });
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            PhotoUrl.Text = Name.Text = Nationality.Text = Team.Text = String.Empty;
            Gender.SelectedIndex = -1;
        }
    }
}
