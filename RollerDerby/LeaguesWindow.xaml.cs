using DataObjects;
using System;
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
        { //open a new create league window
            Window createLeague = new UpdateLeague();
            bool result = (bool)createLeague.ShowDialog();
            if (result)
            {
                MessageBox.Show("create sucessful");
                MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                grabAllLeagues();

            }
            else { MessageBox.Show("Create failed"); }


        }

        private void btnViewLeague_Click(object sender, RoutedEventArgs e)
        {
            //not implemented
        }

        private void btnUpdateLeague_Click(object sender, RoutedEventArgs e)
        {
            //open a new update league window, then refresh the list
            if (datLeague.SelectedItems.Count != 0)
            {
                var _league = datLeague.SelectedItem as League;
                Window UpdateLeague = new UpdateLeague(_league);
                bool result = (bool)UpdateLeague.ShowDialog();
                if (result)
                {
                    MessageBox.Show("Update sucessful");
                    MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                    grabAllLeagues();

                }
                else { MessageBox.Show("Update failed"); }

            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }
        private void btnDeleteLeague_Click(object sender, RoutedEventArgs e)
        { //delete the selected league, then refresh the window
            if (datLeague.SelectedItems.Count != 0)
            {
                var _league = datLeague.SelectedItem as League;
                try
                {
                    int result = MainWindow._leaguemanager.deleteLeague(_league);
                    if (result > 0)
                    {
                        MessageBox.Show("Delete successful");
                        grabAllLeagues();
                    }
                    else { throw new ApplicationException("Delete failed"); }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Delete Failed");
                }
            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            grabAllLeagues();
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            grabAllLeagues();
        }
        public void grabAllLeagues()
        {
            //grab all leagues to fill the window.
            try
            {

                datLeague.ItemsSource = null;
                datLeague.ItemsSource = MainWindow._leaguemanager.getAllLeague();





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }
        }



        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //pull up the update league window
            if (datLeague.SelectedItems.Count != 0)
            {
                var _league = datLeague.SelectedItem as League;
                Window UpdateLeague = new UpdateLeague(_league);
                bool result = (bool)UpdateLeague.ShowDialog();
                if (result)
                {
                    MessageBox.Show("Update sucessful");
                    MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                    grabAllLeagues();

                }
                else { MessageBox.Show("Update failed"); }

            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
