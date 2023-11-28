using DataObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for TeamsWindow.xaml
    /// </summary>
    public partial class TeamsWindow : Window
    {
        public TeamsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grabAllTeams();
        }

        public void grabAllTeams()
        {

            try
            {

                datTeam.ItemsSource = null;
                List<Team> allteams = MainWindow._teamManager.getAllTeam();
                foreach (Team team in allteams) {
                    team.MonthlyDue = Decimal.Round(team.MonthlyDue,2);
                
                }
                datTeam.ItemsSource = allteams;
                
              





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }
        }

        private void datTeam_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void datTeam_MouseDoubleClick_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (datTeam.SelectedItems.Count != 0)
            {
                Team team = datTeam.SelectedItem as Team;
                Window viewTeam = new TeamAddEditDelete(team);
                bool result = (bool)(viewTeam.ShowDialog());
                if (result) { grabAllTeams(); }
            }
            else { MessageBox.Show("Pick something, ya goon!"); }

        }

        private void btnDeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            if (datTeam.SelectedItems.Count != 0)
            {
                Team team = datTeam.SelectedItem as Team;
                try
                {
                    int result = MainWindow._teamManager.purgeTeam(team.TeamId);
                    if (result != 0)
                    {
                        MessageBox.Show("Delete successful");
                        grabAllTeams();
                    }
                    else { throw new ApplicationException("Delete Failed"); }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Delete Failed");
                }





            }
            else { MessageBox.Show("Pick something , ya goon"); }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdateTeam_Click(object sender, RoutedEventArgs e)
        {
            if (datTeam.SelectedItems.Count != 0)
            {
                Team team = datTeam.SelectedItem as Team;
                try
                {
                    Window updateTeam = new TeamAddEditDelete(team);
                   bool result= (bool)updateTeam.ShowDialog();
                   
                    if (result)
                    {
                        MessageBox.Show("Update successful");
                        grabAllTeams();
                    }
                    else { throw new ApplicationException("Update Failed"); }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Update Failed");
                }





            }
            else { MessageBox.Show("Pick something , ya goon"); }
        }

        private void btnCreateTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window updateTeam = new TeamAddEditDelete();
                bool result = (bool)updateTeam.ShowDialog();

                if (result)
                {
                    MessageBox.Show("Add successful");
                    grabAllTeams();
                }
                else { throw new ApplicationException("Add Failed"); }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Add Failed");
            }
        }
    }
    
}