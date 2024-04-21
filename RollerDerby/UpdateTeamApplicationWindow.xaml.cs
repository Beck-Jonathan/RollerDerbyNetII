using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for UpdateTeamApplicationWindow.xaml
    /// </summary>
    public partial class UpdateTeamApplicationWindow : Window
    {
        SkaterManager _sm = null;
        TeamApplication application;
        TeamApplicationManager TAM;
        Skater oldskater;
        SkaterManager skaterManager;
        public UpdateTeamApplicationWindow(TeamApplication _application)
        { //if an application is passed in, start in edit mode
            application = _application;
            skaterManager = new SkaterManager();
            oldskater = skaterManager.GetSkaterVMByDerbyName(_application.SkaterID);
            TAM = new TeamApplicationManager();
            InitializeComponent();
            _sm = new SkaterManager();
            tbxTeamApplicationApplicationTime.Text = application.ApplicationTime.ToString();
            tbxTeamApplicationSkaterID.Text = application.SkaterID;
            tbxTeamApplicationTeamApplicationID.Text = application.TeamApplicationID.ToString();
            tbxTeamApplicationTeamName.Text = application.TeamName;
            tbxTeamApplicationApplicationTime.IsEnabled = false;
            tbxTeamApplicationSkaterID.IsEnabled = false;
            tbxTeamApplicationTeamApplicationID.IsEnabled = false;
            tbxTeamApplicationTeamName.IsEnabled = false;
            List<string> allStatus = new List<string>();
            try
            {
                allStatus = _sm.getAllApplicationStatus();
                if (allStatus.Count == 0) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            cbxTeamApplicationApplicationStatus.ItemsSource = allStatus;
            cbxTeamApplicationApplicationStatus.Text = _application.ApplicationStatus;



        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateTeamApplication_Click(object sender, RoutedEventArgs e)
        {
            //update the application status, and if approved, assign the skater to the team
            if (cbxTeamApplicationApplicationStatus.Text != "")
            {
                int approveresult = 0;
                int assignresult = 0;
                TeamApplication _newTeamApplicaiton = new TeamApplication();
                _newTeamApplicaiton.TeamApplicationID = Int32.Parse(tbxTeamApplicationTeamApplicationID.Text);
                _newTeamApplicaiton.ApplicationTime = DateTime.Parse(tbxTeamApplicationApplicationTime.Text);
                _newTeamApplicaiton.ApplicationStatus = cbxTeamApplicationApplicationStatus.Text;
                _newTeamApplicaiton.SkaterID = tbxTeamApplicationSkaterID.Text;
                _newTeamApplicaiton.TeamName = tbxTeamApplicationTeamName.Text;
                Skater _newSkater = new Skater();
                _newSkater.SkaterID = oldskater.SkaterID;
                _newSkater.Email = oldskater.Email;
                _newSkater.GivenName = oldskater.GivenName;
                _newSkater.FamilyName = oldskater.FamilyName;
                _newSkater.TeamID = _newTeamApplicaiton.TeamName;
                _newSkater.Active = true;
                _newSkater.Phone = oldskater.Phone;


                try
                {
                    approveresult = TAM.editTeamApplication(application, _newTeamApplicaiton);

                    if (approveresult == 0) { throw new ApplicationException(); }
                    else { MessageBox.Show("Application Updated"); }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                if (_newTeamApplicaiton.ApplicationStatus == "Approved")
                {
                    try
                    {
                        assignresult = skaterManager.editSkater(oldskater, _newSkater);
                        if (assignresult == 0) { throw new ApplicationException(); }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }


                }
            }
            else { MessageBox.Show("Please pick a status"); }

        }
    }
}
