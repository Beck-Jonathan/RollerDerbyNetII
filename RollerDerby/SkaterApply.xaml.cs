using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;


namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for SkaterApply.xaml
    /// </summary>
    public partial class SkaterApply : Window
    {
        TeamManager _tm = null;
        Skater _skt = null;
        TeamApplicationManager TAM = null;
        List<String> TeamNames = new List<String>();

        public SkaterApply()
        {
            InitializeComponent();
            _tm = new TeamManager();
            TAM = new TeamApplicationManager();
            List<Team> allTeams = _tm.getAllTeam();

            foreach (Team t in allTeams)
            {
                TeamNames.Add(t.TeamId);
            }
        }
        public SkaterApply(Skater skt)
        {
            InitializeComponent();
            _skt = skt;
            _tm = new TeamManager();
            TAM = new TeamApplicationManager();
            List<Team> allTeams = _tm.getAllTeam();
            //fill combo box with all teams
            foreach (Team t in allTeams)
            {
                TeamNames.Add(t.TeamId);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            cbxAvailableTeams.ItemsSource = TeamNames;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (cbxAvailableTeams.Text != null && cbxAvailableTeams.Text != "")
            {
                //create new team application object in the databse
                TeamApplication TA = new TeamApplication();
                TA.ApplicationTime = DateTime.Now;
                TA.SkaterID = _skt.SkaterID;
                TA.TeamName = cbxAvailableTeams.Text;

                try
                {
                    int result = 0;
                    result = TAM.addTeamApplication(TA);
                    if (result == 0) { throw new ApplicationException("Application Failed"); }
                    else
                    {
                        MessageBox.Show("Application Added");
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Application Failed");
                }
            }
            else { MessageBox.Show("Plesae pick the team you wish to join"); }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
