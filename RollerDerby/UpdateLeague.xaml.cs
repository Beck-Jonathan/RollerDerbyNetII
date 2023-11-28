using DataObjects;
using LogicLayer;
using System;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for UpdateLeague.xaml
    /// </summary>
    public partial class UpdateLeague : Window
    {
        League _league = null;
        LeagueManager _lm = MainWindow._leaguemanager;
        public UpdateLeague()
        {
            InitializeComponent();
            tbxoldLeagueGender.IsEnabled = true;
            tbxoldLeagueName.IsEnabled = true;
            tbxoldLeagueRegion.IsEnabled = true;
            tbxoldLeagueGender.Text = "";
            tbxoldLeagueName.Text = "";
            tbxoldLeagueRegion.Text = "";
            btnUpdateLeague.Visibility = Visibility.Collapsed;
            btnAddLeague.Content = "Save Add";





        }

        public UpdateLeague(League l)
        {

            InitializeComponent();

            _league = l;
            tbxoldLeagueName.IsEnabled = false;
            tbxoldLeagueGender.IsEnabled = false;
            tbxoldLeagueRegion.IsEnabled = false;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (_league != null)
            {
                tbxoldLeagueName.Text = _league.LeagueID;
                tbxoldLeagueGender.Text = _league.Gender;
                tbxoldLeagueRegion.Text = _league.Region;
            }

        }

        private void btnUpdateLeague_Click(object sender, RoutedEventArgs e)
        {


            //if content is edit,
            if ((string)btnUpdateLeague.Content == "Edit League")
            {
                tbxoldLeagueGender.IsEnabled = true;
                tbxoldLeagueName.IsEnabled = false;
                tbxoldLeagueRegion.IsEnabled = true;
                btnAddLeague.IsEnabled = false;
                btnUpdateLeague.Content = "Save Edit";

            }
            else
            {
                if ( validInputs() )
                {
                    tbxoldLeagueGender.IsEnabled = false;
                    tbxoldLeagueName.IsEnabled = false;
                    tbxoldLeagueRegion.IsEnabled = false;
                    btnAddLeague.IsEnabled = true;
                    btnUpdateLeague.Content = "Edit League";
                    League _newLeague = new League();
                    _newLeague.Region = tbxoldLeagueRegion.Text;
                    _newLeague.LeagueID = tbxoldLeagueName.Text;
                    _newLeague.Gender = tbxoldLeagueGender.Text;

                    try
                    {
                        int result = _lm.updateLeague(_league, _newLeague);
                        if (result == 1) { this.DialogResult = true; }
                        else { throw new ApplicationException("update failed"); }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("update failed \n\n");
                    }
                }
                else { MessageBox.Show("invalid inputs, please try again"); }



            }




        }


        private void btnAddLeague_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnAddLeague.Content == "Add League")
            {
                tbxoldLeagueGender.IsEnabled = true;
                tbxoldLeagueName.IsEnabled = true;
                tbxoldLeagueRegion.IsEnabled = true;
                tbxoldLeagueGender.Text = "";
                tbxoldLeagueName.Text = "";
                tbxoldLeagueRegion.Text = "";
                btnUpdateLeague.IsEnabled = false;
                btnAddLeague.Content = "Save Add";

            }
            else
            {
                if (
                    validInputs()
                    )
                {
                    tbxoldLeagueGender.IsEnabled = false;
                    tbxoldLeagueName.IsEnabled = false;
                    tbxoldLeagueRegion.IsEnabled = false;
                    btnUpdateLeague.IsEnabled = true;
                    btnAddLeague.Content = "Add League";
                    League _newLeague = new League();
                    _newLeague.Region = tbxoldLeagueRegion.Text;
                    _newLeague.LeagueID = tbxoldLeagueName.Text;
                    _newLeague.Gender = tbxoldLeagueGender.Text;

                    try
                    {
                        bool result = _lm.createLeague(_newLeague);
                        if (result) { this.DialogResult = true; }
                        else { throw new ApplicationException("update failed"); }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("update failed \n\n");
                    }
                }
                else { MessageBox.Show("invalid inputs, please try again"); }



            }
        }
        public bool validInputs()
        {

            return tbxoldLeagueRegion.Text.isValidNVarChar100() &&
                        tbxoldLeagueName.Text.isValidNVarChar100() &&
                            tbxoldLeagueGender.Text.isValidNVarChar100();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
