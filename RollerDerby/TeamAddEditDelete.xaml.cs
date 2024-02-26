using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for TeamAddEditDelete.xaml
    /// </summary>
    public partial class TeamAddEditDelete : Window
    {
        public Team _team = null;
        public TeamManager _tm = null;
        public TeamAddEditDelete(Team t)
        {
            //if you pass in a team , you are already in edit mode
            InitializeComponent();
            _team = t;

            _tm = new TeamManager();
            cbxLeagueName.IsEnabled = false;
            tbxTeamID.IsEnabled = false;
            tbxTeamCity.IsEnabled = false;
            tbxTeamZip.IsEnabled = false;
            tbxMonthlyDue.IsEnabled = false;
            cbxLeagueName.IsEnabled = false;
            cbxTeamActive.IsEnabled = true;
            cbxTeamState.IsEnabled = false;
        }
        public TeamAddEditDelete()
        { //if you pass in nothing, you are in the view mode
            InitializeComponent();
            _tm = new TeamManager();
            cbxLeagueName.IsEnabled = true;
            tbxTeamID.IsEnabled = true;
            tbxTeamCity.IsEnabled = true;
            tbxTeamZip.IsEnabled = true;
            tbxMonthlyDue.IsEnabled = true;
            cbxLeagueName.IsEnabled = true;
            cbxTeamActive.IsEnabled = false;
            cbxTeamState.IsEnabled = true;
            cbxLeagueName.SelectedItem = null;
            tbxTeamID.Text = "";
            tbxTeamCity.Text = "";
            tbxTeamZip.Text = "";
            tbxMonthlyDue.Text = "";

            cbxTeamActive.IsChecked = true;
            cbxTeamState.Text = "";
            btnAdd.Content = "Save Add";
            btnAdd.IsEnabled = true;
            btnEdit.Visibility = Visibility.Hidden;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { //grab all combobox data. If a team exists, fill the combo boxes and text boxes with the data
            List<League> leagues = MainWindow.allLeagues;
            String[] states = States.Names();

            List<string> leagueNames = new List<string>();
            foreach (League _league in leagues)
            {
                leagueNames.Add(_league.LeagueID);


            }
            cbxLeagueName.ItemsSource = leagueNames;
            cbxTeamState.ItemsSource = states;
            if (_team != null)
            {
                tbxTeamID.Text = _team.TeamId;
                tbxTeamCity.Text = _team.City;
                tbxTeamZip.Text = _team.Zip;
                tbxMonthlyDue.Text = _team.MonthlyDue.ToString();
                cbxLeagueName.Text = _team.LeagueID;
                cbxTeamActive.IsChecked = true;
                cbxTeamState.Text = _team.State;
            }





            //on click edit, enable everything and change edit to save edit, disable add
            // on click add, enable everything, clear it, change add to save add, disable edit
            //on edit, call the team manager edit and close window
            //on add, call team manager add, and close window
            //show a litle active box, when clicked, call deactivate or reactivate, as needed, and close window

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //if this is the first click, unlock all fields and lock the add button
            if ((string)btnEdit.Content == "Edit")
            {
                cbxLeagueName.IsEnabled = true;
                tbxTeamID.IsEnabled = false;
                tbxTeamCity.IsEnabled = true;
                tbxTeamZip.IsEnabled = true;
                tbxMonthlyDue.IsEnabled = true;
                cbxLeagueName.IsEnabled = true;
                cbxTeamActive.IsEnabled = false;
                cbxTeamState.IsEnabled = true;
                btnEdit.Content = "Save Edit";
                btnAdd.IsEnabled = false;
            }
            else
            //if this is your second click, validate the inputs, then pass the edit call to the database
            if (validInputs())
            {
                {
                    Team _updatedteam = new Team();
                    _updatedteam.MonthlyDue = Decimal.Parse(tbxMonthlyDue.Text);
                    _updatedteam.City = tbxTeamCity.Text;
                    _updatedteam.Zip = tbxTeamZip.Text;
                    _updatedteam.State = cbxTeamState.Text;
                    _updatedteam.LeagueID = cbxLeagueName.Text;
                    _updatedteam.TeamId = tbxTeamID.Text;
                    _updatedteam.isActive = true;
                    cbxLeagueName.IsEnabled = false;
                    tbxTeamID.IsEnabled = false;
                    tbxTeamCity.IsEnabled = false;
                    tbxTeamZip.IsEnabled = false;
                    tbxMonthlyDue.IsEnabled = false;
                    cbxLeagueName.IsEnabled = false;
                    cbxTeamActive.IsEnabled = false;
                    cbxTeamState.IsEnabled = false;
                    btnEdit.Content = "Edit";
                    btnAdd.IsEnabled = true;
                    try
                    {
                        int result = _tm.editTeam(_team, _updatedteam);
                        if (result == 1)
                        {
                            MessageBox.Show("Updated!");
                            this.DialogResult = true;
                        }
                        else { throw new ApplicationException(); }

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Update Failed");
                        this.DialogResult = false;
                    }


                }
            }
            else { MessageBox.Show("invaldi inputs"); }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        { //if this is your first click, clear the fields and lock the edit button
            if ((string)btnAdd.Content == "Add")
            {
                cbxLeagueName.IsEnabled = true;
                tbxTeamID.IsEnabled = true;
                tbxTeamCity.IsEnabled = true;
                tbxTeamZip.IsEnabled = true;
                tbxMonthlyDue.IsEnabled = true;
                cbxLeagueName.IsEnabled = true;
                cbxTeamActive.IsEnabled = false;
                cbxTeamState.IsEnabled = true;
                cbxLeagueName.SelectedItem = null;
                tbxTeamID.Text = "";
                tbxTeamCity.Text = "";
                tbxTeamZip.Text = "";
                tbxMonthlyDue.Text = "";

                cbxTeamActive.IsChecked = true;
                cbxTeamState.Text = "";
                btnAdd.Content = "Save Add";
                btnAdd.IsEnabled = true;
                btnEdit.IsEnabled = false;
            }
            else
            { //if this is your second click, validate input and pass the object down to the database
                if (validInputs())
                {
                    Team newTeam = new Team();
                    newTeam.MonthlyDue = Decimal.Parse(tbxMonthlyDue.Text);
                    newTeam.City = tbxTeamCity.Text;
                    newTeam.Zip = tbxTeamZip.Text;
                    newTeam.State = cbxTeamState.Text;
                    newTeam.LeagueID = cbxLeagueName.Text;
                    newTeam.TeamId = tbxTeamID.Text;
                    newTeam.isActive = true;
                    cbxLeagueName.IsEnabled = false;
                    tbxTeamID.IsEnabled = false;
                    tbxTeamCity.IsEnabled = false;
                    tbxTeamZip.IsEnabled = false;
                    tbxMonthlyDue.IsEnabled = false;

                    cbxTeamActive.IsEnabled = false;
                    cbxTeamState.IsEnabled = false;
                    btnAdd.Content = "Add";
                    btnEdit.IsEnabled = true;
                    try
                    {
                        bool result = _tm.addTeam(newTeam);
                        if (result)
                        {
                            MessageBox.Show("added!");
                            this.DialogResult = true;
                        }
                        else { throw new ApplicationException(); }

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("add Failed");
                        this.DialogResult = false;
                    }


                }
                else { MessageBox.Show("invalid inputs"); }
            }

        }

        private bool validInputs()
        { //ensure the user has filled in each field properly

            return tbxTeamID.Text.isValidNVarChar50()
            && tbxTeamCity.Text.isValidNVarChar100()
            && tbxTeamZip.Text.isValidzip()
            && tbxMonthlyDue.Text.isValidDecimal();

        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
