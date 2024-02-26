using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for SkaterAddEditDelete.xaml
    /// </summary>
    public partial class SkaterAddEditDelete : Window
    {
        public SkaterManager _Sm = null;
        public Skater _skater = null;
        public SkaterAddEditDelete()
        {
            InitializeComponent();
            _Sm = new SkaterManager();
            btnAddSkater.Content = "Save Add";
        }

        public SkaterAddEditDelete(Skater s)
        {
            //fill all combo boxes from databse, and fill all comboboxes and textboxes with the passed object
            InitializeComponent();
            fillcombobox();
            _skater = s;
            _Sm = new SkaterManager();
            tbxSkaterSkaterID.IsEnabled = false;
            btnAddSkater.Visibility = Visibility.Hidden;
            btnUpdateSkater.Content = "Save Edit";
            tbxSkateremail.Text = s.Email;
            tbxSkaterFamilyName.Text = s.FamilyName;
            tbxSkaterGivenName.Text = s.GivenName;
            tbxSkaterPhone.Text = s.Phone;
            tbxSkaterSkaterID.Text = s.SkaterID;
            cbxSkaterTeamID.Text = s.TeamID;
            if (_skater != null)
            {
                cbxSkaterTeamID.Text = _skater.TeamID;
            }

        }




        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAddSkater_Click(object sender, RoutedEventArgs e)
        {
            //if this is the first click, clear all the fields and dsable the edit skater button
            if ((string)btnAddSkater.Content == "Add Skater")
            {
                btnUpdateSkater.IsEnabled = false;
                btnAddSkater.Content = "Save Add";
                tbxSkaterSkaterID.IsEnabled = true;
                tbxSkaterSkaterID.Text = "";
                cbxSkaterTeamID.IsEnabled = true;
                cbxSkaterTeamID.SelectedItem = null;
                tbxSkaterGivenName.IsEnabled = true;
                tbxSkaterGivenName.Text = "";
                tbxSkaterFamilyName.IsEnabled = true;
                tbxSkaterFamilyName.Text = "";
                tbxSkaterPhone.IsEnabled = true;
                tbxSkaterPhone.Text = "";
                tbxSkateremail.IsEnabled = true;
                tbxSkateremail.Text = "";

                chkSkateractive.IsEnabled = true;
                chkSkateractive.IsChecked = true;
            }
            else
            //if this is the second click, save the created skater to the db
            {
                if (validInputs())
                {
                    SkaterVM newSkater = new SkaterVM();
                    newSkater.SkaterID = tbxSkaterSkaterID.Text;
                    newSkater.TeamID = cbxSkaterTeamID.Text;
                    newSkater.GivenName = tbxSkaterGivenName.Text;
                    newSkater.FamilyName = tbxSkaterFamilyName.Text;
                    newSkater.Phone = tbxSkaterPhone.Text;
                    newSkater.Email = tbxSkateremail.Text;

                    newSkater.Active = true;
                    try
                    {
                        bool result = (1 == _Sm.addSkater(newSkater));
                        if (result)
                        {
                            MessageBox.Show("added!");
                            this.DialogResult = true;
                        }
                        else
                        {
                            throw new ApplicationException();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("add failed");
                        this.DialogResult = false;
                    }
                }
                else
                {
                    MessageBox.Show("invalid inputs");
                }
            }
        }
        public bool validInputs()
        {
            //test that the user has filled in valid inputs
            return tbxSkaterSkaterID.Text.IsValidDerbyName() &&

                tbxSkaterGivenName.Text.isValidNVarChar50() &&
                tbxSkaterFamilyName.Text.isValidNVarChar50() &&
            tbxSkaterPhone.Text.isValidPhone() &&
           tbxSkateremail.Text.isValidEmail();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillcombobox();

        }

        private void btnUpdateSkater_Click(object sender, RoutedEventArgs e)
        {
            //if this is the first click, unlock all edit fields, and lock add button
            if ((string)btnUpdateSkater.Content == "Edit Skater")
            {
                btnAddSkater.IsEnabled = false;

                btnUpdateSkater.Content = "Save Edit";
                tbxSkaterSkaterID.IsEnabled = false;
                tbxSkaterSkaterID.Text = "";
                cbxSkaterTeamID.IsEnabled = true;
                //cbxSkaterTeamID.SelectedItem = null;
                tbxSkaterGivenName.IsEnabled = true;
                //tbxSkaterGivenName.Text = "";
                tbxSkaterFamilyName.IsEnabled = true;
                //tbxSkaterFamilyName.Text = "";
                tbxSkaterPhone.IsEnabled = true;
                //tbxSkaterPhone.Text = "";
                tbxSkateremail.IsEnabled = true;
                //tbxSkateremail.Text = "";

                chkSkateractive.IsEnabled = true;
                chkSkateractive.IsChecked = true;
            }
            else
            {//if this is the second click, try to pass this to the manager and then the database
                if (validInputs())
                {
                    SkaterVM newSkater = new SkaterVM();
                    newSkater.SkaterID = tbxSkaterSkaterID.Text;
                    newSkater.TeamID = cbxSkaterTeamID.Text;
                    newSkater.GivenName = tbxSkaterGivenName.Text;
                    newSkater.FamilyName = tbxSkaterFamilyName.Text;
                    newSkater.Phone = tbxSkaterPhone.Text;
                    newSkater.Email = tbxSkateremail.Text;

                    newSkater.Active = true;
                    try
                    {
                        bool result = (1 == _Sm.editSkater(_skater, newSkater));
                        if (result)
                        {
                            MessageBox.Show("Edited!");
                            this.DialogResult = true;
                        }
                        else
                        {
                            throw new ApplicationException();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Edit failed");
                        this.DialogResult = false;
                    }
                }
                else
                {
                    MessageBox.Show("invalid inputs");
                }
            }
        }
        public void fillcombobox()
        {//fill all combo boxes
            List<Team> allTeams = MainWindow._teamManager.getAllTeam();
            List<String> allTeamNames = new List<string>();
            foreach (Team t in allTeams)
            {
                allTeamNames.Add(t.TeamId);
            }
            cbxSkaterTeamID.ItemsSource = allTeamNames;
        }

    }

}
