using DataObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for UpdateLeague.xaml
    /// </summary>
    public partial class UpdateLeague : Window
    {
        public UpdateLeague()
        {
            InitializeComponent();
            fillComboBox();
        }

        private void fillComboBox()
        {

            List<string> LeagueNames = new List<string>();
            foreach (League _league in MainWindow.allLeagues)
            {
                LeagueNames.Add(_league.LeagueID);


            }
            cbxoldLeagueName.ItemsSource = LeagueNames;
            cbxoldLeagueName.SelectedIndex = 0;
            tbxoldLeagueGender.IsEnabled = false;
            tbxoldLeagueRegion.IsEnabled = false;
            tbxnewLeagueName.Text = cbxoldLeagueName.SelectedItem.ToString();
            tbxnewLeagueName.IsEnabled = false;
            foreach (League _league in MainWindow.allLeagues)
            {
                if (_league.LeagueID == cbxoldLeagueName.SelectedItem.ToString())
                {
                    tbxoldLeagueGender.Text = _league.Gender;
                    tbxoldLeagueRegion.Text = _league.Region;


                }


            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxoldLeagueName.SelectedIndex = 1;
            fillComboBox();

        }

        private void btnUpdateLeague_Click(object sender, RoutedEventArgs e)
        {
            string oldId = cbxoldLeagueName.Text;
            string oldregion = tbxoldLeagueRegion.Text;
            string oldGender = tbxoldLeagueGender.Text;
            string newId = oldId;
            string newregion = tbxnewLeagueRegion.Text;
            string newgender = tbxnewLeagueGender.Text;


            try
            {
                MainWindow._leaguemanager.updateLeague(oldId, oldregion, oldGender, newId, newregion, newgender);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {

                this.DialogResult = false;
            }

        }

        private void cbxoldLeagueName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (League _league in MainWindow.allLeagues)
            {
                if (_league.LeagueID == cbxoldLeagueName.SelectedItem.ToString())
                {
                    tbxoldLeagueGender.Text = _league.Gender;
                    tbxoldLeagueRegion.Text = _league.Region;
                    tbxnewLeagueName.Text = cbxoldLeagueName.SelectedItem.ToString();


                }


            }

        }
    }
}
