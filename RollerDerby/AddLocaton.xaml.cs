using DataObjects;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for AddLocaton.xaml
    /// </summary>
    public partial class AddLocaton : Window
    {
        public AddLocaton()
        {
            InitializeComponent();
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            string LocationID = tbxLocationName.Text;
            string LeagueID = cbxLeagueAffil.Text;
            string ContactPhone = tbxContactPhone.Text;
            string City = tbxCity.Text;
            string State = tbxState.Text;
            string Zip = tbxZip.Text;


            try
            {
                MainWindow._locationmanager.AddLocation(LocationID, LeagueID, ContactPhone, City, State, Zip); ;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Create Failed");
                this.DialogResult = false;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> allLeagues = new List<String>();
            foreach (League _league in MainWindow.allLeagues)
            {
                string LeagueId = _league.LeagueID;
                allLeagues.Add(LeagueId);


            }
            cbxLeagueAffil.ItemsSource = allLeagues;
        }
    }
}
