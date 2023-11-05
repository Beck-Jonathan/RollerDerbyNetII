using DataObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for DeleteLeague.xaml
    /// </summary>
    public partial class DeleteLeague : Window
    {
        static int modeChoice;
        public DeleteLeague(int mode)
        {
            InitializeComponent();
            fillComboBox();
            modeChoice = mode;


        }

        private void btnDeleteLeague_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                League toDelete = new League();
                toDelete.LeagueID = cbxLeagueName.Text;
                toDelete.Gender = tbxLeagueGender.Text;
                toDelete.Region = tbxLeagueRegion.Text;
                if (MainWindow._leaguemanager.deleteLeague(toDelete.LeagueID) == 1)
                {
                    lblStatus.Content = "League Deleted";
                    MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                    this.DialogResult = true;
                }
                else { lblStatus.Content = "League does not exist"; }

            }
            catch (Exception ex)
            {

                lblStatus.Content = "Delete Failed";
                this.DialogResult = false;
            }


        }

        private void cbxLeagueName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                getLeagueDetails();
            }
            catch (Exception ex)
            {

                lblStatus.Content = "delete error (7)";
            }



        }



        private void getLeagueDetails()
        {


            try
            {
                string newLeagueName = cbxLeagueName.SelectedItem.ToString();
                League tempLeague = MainWindow._leaguemanager.getLeagueByPrimaryKey(newLeagueName);
                tbxLeagueGender.Text = tempLeague.Gender;
                tbxLeagueRegion.Text = tempLeague.Region;
            }
            catch (Exception ex)
            {

                lblStatus.Content = "database error";

            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxLeagueName.SelectedIndex = 1;
            getLeagueDetails();
            if (modeChoice == 0)
            {
                btnDeleteLeague.Visibility = Visibility.Hidden;


            }

        }
        private void fillComboBox()
        {

            List<string> LeagueNames = new List<string>();
            foreach (League _league in MainWindow.allLeagues)
            {
                LeagueNames.Add(_league.LeagueID);


            }
            cbxLeagueName.ItemsSource = LeagueNames;
            cbxLeagueName.SelectedIndex = 0;
            tbxLeagueGender.IsEnabled = false;
            tbxLeagueRegion.IsEnabled = false;
        }


    }
}
