using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for Leagues.xaml
    /// </summary>
    public partial class LeaguesWindow : Window
    {
        public LeaguesWindow()
        {
            InitializeComponent();
        }

        private void btnCreateLeague_Click(object sender, RoutedEventArgs e)
        {
            Window AddLeague = new AddLeague();
            bool result = (bool)AddLeague.ShowDialog();
            if (result)
            {
                MessageBox.Show("League Added");
                MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
            }
            else
            {
                MessageBox.Show("League not added, pelase try again");
            }


        }

        private void btnViewLeague_Click(object sender, RoutedEventArgs e)
        {
            Window ViewLeague = new DeleteLeague(0);
            ViewLeague.Show();
        }

        private void btnUpdateLeague_Click(object sender, RoutedEventArgs e)
        {
            Window UpdateLeague = new UpdateLeague();
            bool result = (bool)UpdateLeague.ShowDialog();
            if (result)
            {
                MessageBox.Show("Update sucessful");
                MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();

            }
            else { MessageBox.Show("Update failed"); }
        }

        private void btnDeleteLeague_Click(object sender, RoutedEventArgs e)
        {
            Window DeleteLeague = new DeleteLeague(1);
            bool result = (bool)DeleteLeague.ShowDialog();
            if (result)
            {
                MessageBox.Show("Delete sucessful");
                MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();

            }
            else { MessageBox.Show("Delete failed"); }
        }
    }
}
