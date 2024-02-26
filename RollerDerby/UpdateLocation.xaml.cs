using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for UpdateLocation.xaml
    /// </summary>
    public partial class UpdateLocation : Window
    {
        LocationManager _lm = MainWindow._locationmanager;
        Location _location = null;
        public UpdateLocation()
        {
            InitializeComponent();
            fillComboBox();
            btnUpdateLocation.Visibility = Visibility.Hidden;
            btnAddLocation.Content = "Save Add";
        }

        public UpdateLocation(Location L)
        {
            //if an object is passed in, start in edit mode
            InitializeComponent();
            fillComboBox();
            _location = L;

            tbxoldLocationName.IsEnabled = false;
            tbxoldLocationCity.IsEnabled = false;
            cbxoldLocationLeague.IsEnabled = false;
            tbxoldLocationPhone.IsEnabled = false;
            cbxoldLocationState.IsEnabled = false;
            tbxoldLocationZip.IsEnabled = false;
            tbxoldLocationCity.Text = _location.City;
            tbxoldLocationName.Text = _location.LocationId;
            tbxoldLocationPhone.Text = _location.ContactPhone;
            cbxoldLocationState.Text = _location.State;
            tbxoldLocationZip.Text = _location.ZipCode;
            cbxoldLocationLeague.Text = _location.LeagueID;





        }

        private void fillComboBox()
        {//grab the needed FK data

            List<string> LeagueNames = new List<string>();
            foreach (League _league in MainWindow.allLeagues)
            {
                LeagueNames.Add(_league.LeagueID);


            }
            cbxoldLocationLeague.ItemsSource = LeagueNames;
            String[] states = States.Names();
            cbxoldLocationState.ItemsSource = states;






        }




        private void btnUpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            //if text is update location
            //change text to save update, unlock all fields, block out add location
            //else, grab all fields, make a new location, and pass it and the original to the manager in a try catch
            if ((string)btnUpdateLocation.Content == "Update Location")
            {
                //clear fields, unlock drop down
                btnUpdateLocation.Content = "Save Update";
                tbxoldLocationCity.IsEnabled = true;
                cbxoldLocationLeague.IsEnabled = true;
                tbxoldLocationPhone.IsEnabled = true;
                cbxoldLocationState.IsEnabled = true;
                tbxoldLocationZip.IsEnabled = true;

                btnAddLocation.IsEnabled = false;







            }
            else
            {
                if (validInputs())
                {
                    Location _newLocation = new Location();
                    _newLocation.City = tbxoldLocationCity.Text;
                    _newLocation.LocationId = tbxoldLocationName.Text;
                    _newLocation.State = cbxoldLocationState.Text;
                    _newLocation.ZipCode = tbxoldLocationZip.Text;
                    _newLocation.LeagueID = cbxoldLocationLeague.Text;
                    _newLocation.ContactPhone = tbxoldLocationPhone.Text;

                    try
                    {
                        int result = MainWindow._locationmanager.updateLocation(_location, _newLocation);
                        if (result > 0)
                        {
                            this.DialogResult = true;
                        }
                        else { throw new ApplicationException("update failed"); }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.DialogResult = false;
                    }
                }
                else { MessageBox.Show("invalid inputs"); }





            }








        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {



            //if text is add location
            //change text to save add, unlock all fields, block out edit location
            //else, grab all fields, make a new location, and pass it to the manager in a try catch
            if ((string)btnAddLocation.Content == "Add Location")
            {
                btnAddLocation.Content = "Save Add";
                tbxoldLocationName.IsEnabled = true;
                tbxoldLocationCity.IsEnabled = true;
                cbxoldLocationLeague.IsEnabled = true;
                tbxoldLocationPhone.IsEnabled = true;
                cbxoldLocationState.IsEnabled = true;
                tbxoldLocationZip.IsEnabled = true;
                btnUpdateLocation.IsEnabled = false;
                tbxoldLocationName.Text = "";
                tbxoldLocationCity.Text = "";
                cbxoldLocationLeague.Text = "";
                tbxoldLocationPhone.Text = "";
                cbxoldLocationState.Text = "";
                tbxoldLocationZip.Text = "";









            }
            else
            {
                if (validInputs())
                {
                    Location _newLocation = new Location();
                    _newLocation.City = tbxoldLocationCity.Text;
                    _newLocation.LocationId = tbxoldLocationName.Text;
                    _newLocation.State = cbxoldLocationState.Text;
                    _newLocation.ZipCode = tbxoldLocationZip.Text;
                    _newLocation.LeagueID = cbxoldLocationLeague.Text;
                    _newLocation.ContactPhone = tbxoldLocationPhone.Text;

                    try
                    {
                        bool result = _lm.AddLocation(_newLocation);
                        if (result)
                        {
                            this.DialogResult = true;
                        }
                        else { throw new ApplicationException("add failed"); }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.DialogResult = false;
                    }
                }
                else { MessageBox.Show("invalid inputs"); }





            }
        }
        private bool validInputs()
        {


            return tbxoldLocationCity.Text.isValidNVarChar100()
                && tbxoldLocationName.Text.isValidNVarChar100()
                && tbxoldLocationPhone.Text.isValidPhone()
                && tbxoldLocationZip.Text.isValidzip();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
