using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for SkaterviewApplications.xaml
    /// </summary>
    public partial class SkaterviewApplications : Window
    {
        int modeselect;
        public SkaterviewApplications(Skater skt, int mode)
        {
            Skater _skater = skt;
            modeselect = mode;
            if (mode == 0)
            {
                //if mode 0, that means you are logged in as  skater, and can view your gear
                //and team applications
             
                String skatername = skt.SkaterID;
                InitializeComponent();
                btnViewInventory.Visibility = Visibility.Hidden;
                datTeamApplications.Visibility = Visibility.Visible;
                datGearApplication.Visibility = Visibility.Visible;

                System.Windows.Controls.Grid.SetRow(datGearApplication, 1);
                System.Windows.Controls.Grid.SetRowSpan(datGearApplication, 5);
                System.Windows.Controls.Grid.SetRow(datTeamApplications, 6);
                System.Windows.Controls.Grid.SetRowSpan(datGearApplication, 5);




                
                TeamApplicationManager TAM = new TeamApplicationManager();
                GearApplicationManager GM = new GearApplicationManager();
                List<GearApplication> allGearApplications = new List<GearApplication>();
                try
                {
                    allGearApplications = GM.getAllGearApplication();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                List<TeamApplication> allTeamApplicaitons = new List<TeamApplication>();
                try
                {
                    allTeamApplicaitons = TAM.getAllTeamApplication();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                List<GearApplication> parsedGearApplications = new List<GearApplication>();
                List<TeamApplication> prasedTeamApplicaitons = new List<TeamApplication>();
                //filter so it only shows requests for the currnet skater
                foreach (GearApplication request in allGearApplications)
                {
                    if (request.SkaterID == skatername)
                    {
                        parsedGearApplications.Add(request);
                    }
                }
                foreach (TeamApplication teamApplication in allTeamApplicaitons)
                {
                    if (teamApplication.SkaterID == skatername)
                    {
                        prasedTeamApplicaitons.Add(teamApplication);
                    }
                }





                datGearApplication.ItemsSource = parsedGearApplications;
                datTeamApplications.ItemsSource = prasedTeamApplicaitons;
            }
            if (mode==1)
            {//if mode 1 you are logged in as a league admin, and you can see all gear requetss
                
                String teamname = _skater.TeamID;
                InitializeComponent();
                btnViewInventory.Visibility = Visibility.Hidden;
                datGearApplication.Visibility = Visibility.Hidden;
                datTeamApplications.Visibility = Visibility.Visible;
                System.Windows.Controls.Grid.SetRow(datTeamApplications, 1);
                System.Windows.Controls.Grid.SetRowSpan(datGearApplication, 10);
                TeamApplicationManager TAM = new TeamApplicationManager();
                List<TeamApplication> allTeamApplicaitons = new List<TeamApplication>();
                try
                {
                    allTeamApplicaitons = TAM.getAllTeamApplication();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
               
                List<TeamApplication> prasedTeamApplicaitons = new List<TeamApplication>();
                
                foreach (TeamApplication teamApplication in allTeamApplicaitons)
                {
                    if (teamApplication.TeamName == teamname)
                    {
                        prasedTeamApplicaitons.Add(teamApplication);
                    }
                }





               
                datTeamApplications.ItemsSource = prasedTeamApplicaitons;
            }
            if (mode == 2)
            {//if mode 2, you are logged in as ateam admin, and can see all skater requests
               
                String skatername = skt.SkaterID;
                InitializeComponent();
                btnViewInventory.Visibility = Visibility.Visible;
                System.Windows.Controls.Grid.SetRow(datGearApplication, 1);
                System.Windows.Controls.Grid.SetRowSpan(datGearApplication, 10);
                datTeamApplications.Visibility = Visibility.Hidden;
                datGearApplication.Visibility = Visibility.Visible;

                GearApplicationManager GM = new GearApplicationManager();
                List<GearApplication> allGearApplications = new List<GearApplication>();
                try
                {
                    allGearApplications = GM.getAllGearApplication();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                datGearApplication.ItemsSource = allGearApplications;
               






            }




        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void datTeamApplications_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (modeselect == 1) {
                //if you are a team admin, you can view the detail of the team request

                {
                    if (datTeamApplications.SelectedItems.Count != 0)
                    {
                        var _application = datTeamApplications.SelectedItem as TeamApplication;
                        Window UpdateApplication = new UpdateTeamApplicationWindow(_application);
                        bool result = (bool)UpdateApplication.ShowDialog();
                        if (result)
                        {
                            MessageBox.Show("Update sucessful");
                            MainWindow.allLeagues = MainWindow._leaguemanager.getAllLeague();
                            this.Close();

                        }
                        else { MessageBox.Show("Update failed"); }

                    }
                    else { MessageBox.Show("Pick something, ya goon!"); }
                }


            }
        }

        private void btnViewInventory_Click(object sender, RoutedEventArgs e)
        {
            Window ViewInventory = new ViewInventoryWindow();
            var result = ViewInventory.ShowDialog();
        }

        private void datGearRequets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (modeselect == 2)
            {//if you are a league admin, you can view the gear requests

                {
                    if (datGearApplication.SelectedItems.Count != 0)
                    {
                        var _gearRequest = datGearApplication.SelectedItem as GearApplication;
                        Window RequestDetail = new UpdateGearRequest(_gearRequest);
                        bool result = (bool)RequestDetail.ShowDialog();
                        if (result)
                        {
                            MessageBox.Show("Update sucessful");
                            
                            this.Close();

                        }
                        else { MessageBox.Show("Update failed"); }

                    }
                    else { MessageBox.Show("Pick something, ya goon!"); }
                }


            }
        }
    }
}
