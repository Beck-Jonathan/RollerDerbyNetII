using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SkaterManager _skatermanager = null;
        public static LeagueManager _leaguemanager = null;
        public static LocationManager _locationmanager = null;
        public static TeamManager _teamManager = null;


        public static List<Skater> allSkaters = null;
        public static List<League> allLeagues = null;
        public static List<LocationVM> allLocations = null;
        public static List<Team> allTeams = null;
        private SkaterVM loggedinSkater = null;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void hideAllTabs()
        {
            foreach (var tab in tabsetMain.Items)
            {
                ((TabItem)tab).Visibility = Visibility.Hidden;
            }
            tabsetMain.Visibility = Visibility.Collapsed;
            tabContainer.Visibility = Visibility.Collapsed;
            mnuEdit.Visibility = Visibility.Collapsed;
        }
        private void updateUIForSkater()
        {
            mnuEdit.Visibility = Visibility.Visible;
            string roleslist = "";
            for (int i = 0; i < loggedinSkater.Roles.Count; i++)
            {
                roleslist += " " + loggedinSkater.Roles[i];
                if (i == loggedinSkater.Roles.Count - 2)
                {
                    if (loggedinSkater.Roles.Count > 2)
                    {
                        roleslist += ",";
                    }

                    roleslist += "and";
                }
                else if (i < loggedinSkater.Roles.Count - 2)
                {
                    roleslist += ",";
                }
            }
            string greeting = "Greetings " + loggedinSkater.GivenName + " you are logged in as " + roleslist + ".";
            lblGreeting.Content = greeting;
            status_message.Content = "Logged in on " + DateTime.Now.ToLongDateString() + " at " +
                DateTime.Now.ToShortTimeString() + ". Please remember to log out.";
            txtDerbyName.Text = "";
            txtDerbyName.Visibility = Visibility.Hidden;
            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Hidden;
            lblDerbyName.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;
            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;
            showTabsForRoles();
            tabsetMain.SelectedIndex = 3;


        }
        private void updateUIForLogout()
        {
            string greeting = "You are not currently logged in";
            lblGreeting.Content = greeting;
            status_message.Content = "Logged out on " + DateTime.Now.ToLongDateString() + "at" +
                DateTime.Now.ToShortTimeString() + "please come back!";
            txtDerbyName.Text = "";
            txtDerbyName.Visibility = Visibility.Visible;
            pwdPassword.Password = "";
            pwdPassword.Visibility = Visibility.Visible;
            lblDerbyName.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            btnLogin.Content = "Log in";
            btnLogin.IsDefault = true;
            hideAllTabs();

        }

        private void showTabsForRoles()
        {
            //loop through user roles 
            //('League_Admin'),
            //('Team_Admin'),
            //('Coach'),
            //('Skater')
            foreach (var role in loggedinSkater.Roles)
            {
                switch (role)
                {
                    case "League_Admin":
                        tabLeagueAdmin.Visibility = Visibility.Visible;
                        tabTeamAdmin.Visibility = Visibility.Visible;
                        tabCoach.Visibility = Visibility.Visible;
                        tabSkater.Visibility = Visibility.Visible;
                        break;
                    case "Team_Admin":
                        tabTeamAdmin.Visibility = Visibility.Visible;
                        tabCoach.Visibility = Visibility.Visible;
                        tabSkater.Visibility = Visibility.Visible;
                        break;
                    case "Coach":
                        tabCoach.Visibility = Visibility.Visible;
                        tabSkater.Visibility = Visibility.Visible;
                        break;
                    case "Skater":
                        tabSkater.Visibility = Visibility.Visible;
                        break;


                }
                tabsetMain.Visibility = Visibility.Visible;
                tabContainer.Visibility = Visibility.Visible;



            }
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (btnLogin.Content.ToString() == "Log in")
            {
                var derbyName = txtDerbyName.Text;
                var password = pwdPassword.Password;

                //error checks
                if (!derbyName.IsValidDerbyName())
                {
                    MessageBox.Show("That is a not a valid derby name",
                        "Invalid derby name", MessageBoxButton.OK, MessageBoxImage.Hand);
                    txtDerbyName.Focus();
                    txtDerbyName.SelectAll();
                    return;
                }
                if (!password.isValidPassword())
                {
                    MessageBox.Show("That is a not a valid Password",
                        "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Hand);
                    pwdPassword.Focus();
                    pwdPassword.SelectAll();
                    return;
                }

                // try to log in the user
                try
                {
                    loggedinSkater = _skatermanager.LoginSkater(derbyName, password);

                    //we need to check for first login as newuser
                    if (pwdPassword.Password.ToString() == "newUser")
                    {

                        try
                        {
                            Window passwordWindow = new PasswordChangeWindow(loggedinSkater, _skatermanager);
                            var result = passwordWindow.ShowDialog();
                            if (result == true)
                            {


                                MessageBox.Show("Password Updated", "success!", MessageBoxButton.OK, MessageBoxImage.Information);


                            }
                            else
                            {
                                MessageBox.Show("Password Not Changed, you need to update password on first login", "Logging out", MessageBoxButton.OK, MessageBoxImage.Information);
                                loggedinSkater = null;
                                return;
                            }

                            updateUIForSkater();
                            return;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "update failed ", MessageBoxButton.OK, MessageBoxImage.Information);
                            updateUIForLogout();
                            loggedinSkater = null;
                        }
                        updateUIForSkater();


                    }



                    //update the UI
                    if (loggedinSkater != null)
                    {
                        updateUIForSkater();
                    }

                    else { lblGreeting.Content = "bad password"; }

                }
                catch (Exception ex)
                {
                    //you may never throw exceptions from the presentation layer
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "invalid Credetials", MessageBoxButton.OK, MessageBoxImage.Hand);
                    pwdPassword.Clear();
                    txtDerbyName.Clear();
                    txtDerbyName.Focus();
                    return;
                }
            }
            else { updateUIForLogout(); }

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //test code
            //_SkaterManager = new SkaterManager(new SkaterAccessorFake());

            //real code
            _skatermanager = new SkaterManager();
            _leaguemanager = new LeagueManager();
            _locationmanager = new LocationManager();
            _teamManager = new TeamManager();
            allLeagues = _leaguemanager.getAllLeague();
            allLocations = _locationmanager.SelectAllLocations();
            allTeams = _teamManager.getAllTeam();
            //allSkaters = _skatermanager.getAllSkater();

            txtDerbyName.Focus();
            btnLogin.IsDefault = true;
            hideAllTabs();


        }

        private void mnuResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (loggedinSkater == null)
            {
                MessageBox.Show("You must be logged in to update your password", "Login Required", MessageBoxButton.OK, MessageBoxImage.Question);

            }
            else
            {
                try
                {
                    Window passwordWindow = new PasswordChangeWindow(loggedinSkater, _skatermanager);
                    var result = passwordWindow.ShowDialog();
                    if (result == true)
                    {


                        MessageBox.Show("Password Updated", "success!", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else { MessageBox.Show("Password Not Changed", "Operation Aborted", MessageBoxButton.OK, MessageBoxImage.Information); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "update failed ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        private void btnLeague_Click(object sender, RoutedEventArgs e)
        {
            //load new League window
            Window Leagues = new LeaguesWindow();
            var result = Leagues.ShowDialog();
        }

        private void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            //load new arena window
            Window Arenas = new ArenasWindow();
            var result = Arenas.ShowDialog();
        }

        private void btnTeam_Click(object sender, RoutedEventArgs e)
        {
            Window Teams = new TeamsWindow();
            var result = Teams.ShowDialog();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSkater_Click(object sender, RoutedEventArgs e)
        {
            Window Skaters = new SkaterWindow();
            var result = Skaters.ShowDialog();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            Window Skaterapplyhere = new SkaterApply(loggedinSkater);
            //Window Apply = new SkaterApply(loggedinSkater);
           var result = Skaterapplyhere.ShowDialog();

        }

        private void btnGear_Click(object sender, RoutedEventArgs e)
        {
            Window GearRequest = new GearRequestWindow(loggedinSkater);
            var result = GearRequest.ShowDialog();
        }

        private void btnViewApplications_Click(object sender, RoutedEventArgs e)
        {
            Window SkaterViewApplications = new SkaterviewApplications(loggedinSkater,0);
            var result = SkaterViewApplications.ShowDialog();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Window SignUp = new SkaterSignUp();
            var result = SignUp.ShowDialog();
        }

        private void btnViewPending_Click(object sender, RoutedEventArgs e)
        {
            Window SkaterViewApplications = new SkaterviewApplications(loggedinSkater, 1);
            var result = SkaterViewApplications.ShowDialog();

        }

        private void btnViewTeam_Click(object sender, RoutedEventArgs e)
        {
            Window ViewTeam = new SkaterWindow(loggedinSkater);
            var result = ViewTeam.ShowDialog();
        }

        private void btnGearRequests_Click(object sender, RoutedEventArgs e)
        {
            Window SkaterViewApplications = new SkaterviewApplications(loggedinSkater, 2);
            var result = SkaterViewApplications.ShowDialog();

        }
    }
}
