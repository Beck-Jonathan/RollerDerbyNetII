using DataObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for deleteLocation.xaml
    /// </summary>
    public partial class deleteLocation : Window
    {
        public deleteLocation(int mode)
        {
            InitializeComponent();
            fillComboBox();
            if (mode == 1) { btnDeleteLocation.Visibility = Visibility.Hidden; }
        }

        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            string locationId = cbxLocationName.Text;
            try
            {
                MainWindow._locationmanager.deleteLocation(locationId);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {

                this.DialogResult = false;
            }
        }
        private void fillComboBox()
        {

            List<string> LeagueNames = new List<string>();
            foreach (Location _location in MainWindow.allLocations)
            {
                LeagueNames.Add(_location.LocationId);


            }
            cbxLocationName.ItemsSource = LeagueNames;
            cbxLocationName.SelectedIndex = 0;

        }

        private void cbxLocationName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                getLocationDetails();
            }
            catch (Exception ex)
            {

                //lblStatus.Content = "Data Parse error (7)";
            }
        }
        private void getLocationDetails()
        {


            try
            {

                //cbxLocationName.
                string newLocationName = cbxLocationName.SelectedItem.ToString();
                Location tempLocation = MainWindow._locationmanager.SelectLocationByLocationID(newLocationName);
                tbxLeagueAffil.Text = tempLocation.LeagueID;
                tbxContactPhone.Text = tempLocation.ContactPhone;
                tbxCity.Text = tempLocation.City;
                tbxState.Text = tempLocation.State;
                tbxZip.Text = tempLocation.ZipCode;

            }
            catch (Exception ex)
            {

                //lblStatus.Content = "database error";

            }



        }
    }
}
