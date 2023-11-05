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

        private void btnCreateLocation_Click(object sender, RoutedEventArgs e)
        {
            Window AddLocation = new AddLocaton();
            var result = (bool)AddLocation.ShowDialog();
            if (result) { MessageBox.Show("Location Added"); }
            else { MessageBox.Show("Add Locaiton Failed"); }
        }

        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            Window delete = new deleteLocation(0);
            var result = (bool)delete.ShowDialog();
            if (result)
            {
                MessageBox.Show("Location Deleted");
                MainWindow.allLocations = MainWindow._locationmanager.SelectAllLocations();
            }
            else { MessageBox.Show("Delete Locaiton Failed"); }
        }

        private void btnViewLocation_Click(object sender, RoutedEventArgs e)
        {
            Window view = new deleteLocation(1);
            var result = (bool)view.ShowDialog();
        }
    }
}
