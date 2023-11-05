using DataObjects;
using System;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for AddLeague.xaml
    /// </summary>
    public partial class AddLeague : Window
    {
        public AddLeague()
        {
            InitializeComponent();
        }

        private void btnAddLeague_Click(object sender, RoutedEventArgs e)
        {
            League newLeague = new League();
            newLeague.LeagueID = tbxLeagueName.Text;
            newLeague.Region = tbxLeagueRegion.Text;
            newLeague.Gender = tbxLeagueGender.Text;
            try
            {
                MainWindow._leaguemanager.createLeague(newLeague.LeagueID, newLeague.Region, newLeague.Gender);

                lblStatus.Content = "League Added";
                this.DialogResult = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                lblStatus.Content = "League not created";
                this.DialogResult = false;
            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }
    }
}
