using DataObjects;
using System;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for ArenasWindow.xaml
    /// </summary>
    public partial class ArenasWindow : Window
    {
        public ArenasWindow()
        {
            InitializeComponent();
        }






        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grabAllLocations();

        }
        public void grabAllLocations()
        {

            try
            {
                datLocations.ItemsSource = null;
                datLocations.ItemsSource = MainWindow._locationmanager.SelectAllLocations();
                datLocations.Columns.RemoveAt(0);





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }
        }

     

        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (datLocations.SelectedItems.Count != 0)
            {
                var _Location = datLocations.SelectedItem as Location;
                Window UpdateLocation = new UpdateLocation(_Location);
                bool result = (bool)UpdateLocation.ShowDialog();
                if (result)
                {
                    MessageBox.Show("Update sucessful");
                    MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                    grabAllLocations();

                }
                else { MessageBox.Show("Update failed"); }

            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }

        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            if (datLocations.SelectedItems.Count != 0)
            {
                var _Location = datLocations.SelectedItem as Location;
                try
                {
                    int result = MainWindow._locationmanager.deleteLocation(_Location.LocationId);
                    if (result == 1) { MessageBox.Show("Locaiton Deleted");
                        grabAllLocations();
                    }
                    else { throw new ApplicationException("Delete failed"); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Faied");

                }

            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreateLocation_Click(object sender, RoutedEventArgs e)
        {
            Window CreateLocation = new UpdateLocation();
            bool result = (bool)CreateLocation.ShowDialog();
            if (result)
            {
                MessageBox.Show("create sucessful");
                MainWindow.allLocations = MainWindow._locationmanager.SelectAllLocations();
                grabAllLocations();

            }
            else { MessageBox.Show("Create failed"); }


        }

        private void btnUpdateLocation_Click_1(object sender, RoutedEventArgs e)
        {
            if (datLocations.SelectedItems.Count != 0)
            {
                var _location = datLocations.SelectedItem as Location;
                Window UpdateLocation = new UpdateLocation(_location);
                bool result = (bool)UpdateLocation.ShowDialog();
                if (result)
                {
                    MessageBox.Show("Update sucessful");
                    MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                    grabAllLocations();

                }
                else { MessageBox.Show("Update failed"); }

            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }

    }
    

}
