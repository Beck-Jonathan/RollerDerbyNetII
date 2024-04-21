using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for SkaterWindow.xaml
    /// </summary>
    public partial class SkaterWindow : Window
    {
        static SkaterManager _skatermanager;
        List<SkaterVM> allSkaters;
        int mode;
        public SkaterWindow()
        {
            //if you are logged in as admin, you have full functionality
            mode = 1;
            InitializeComponent();

        }

        public SkaterWindow(Skater coach)
        {//if you pass in a coach object, you can only view the skaters on this team
            //you can not create, delete, or update skaters
            mode = 1;
            InitializeComponent();
            List<SkaterVM> thisTeamsSkaters = new List<SkaterVM>();
            grabAllSkaters();
            btnCreateSkater.Visibility = Visibility.Hidden;
            btnDeleteSkater.Visibility = Visibility.Hidden;
            btnUpdateSkater.Visibility = Visibility.Hidden;
            foreach (SkaterVM skater in allSkaters)
            {
                if (skater.TeamID == coach.TeamID)
                {
                    thisTeamsSkaters.Add(skater);

                }


            }
            datSkater.ItemsSource = thisTeamsSkaters;

        }

        private void btnCreateSkater_Click(object sender, RoutedEventArgs e)
        {//open new create skater window
            Window skater = new SkaterAddEditDelete();
            bool result = (bool)skater.ShowDialog();
            if (result)
            {
                MessageBox.Show("added");
                grabAllSkaters();
            }
            else { MessageBox.Show("Lmao, nope"); }
        }

        private void btnUpdateSkater_Click(object sender, RoutedEventArgs e)
        {//open new update skater window
            if (datSkater.SelectedItems.Count != 0)
            {
                var _skater = datSkater.SelectedItem as Skater;
                Window UpdateSkater = new SkaterAddEditDelete(_skater);
                bool result = (bool)UpdateSkater.ShowDialog();
                if (result)
                {
                    MessageBox.Show("Update sucessful");
                    //MainWindow.allSkaters = MainWindow._skatermanager.getAllSkater();
                    grabAllSkaters();

                }
                else { MessageBox.Show("Update failed"); }

            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }

        private void btnDeleteSkater_Click(object sender, RoutedEventArgs e)
        { //delete current skater
            if (datSkater.SelectedItems.Count != 0)
            {
                var _skater = datSkater.SelectedItem as Skater;
                try
                {
                    int result = _skatermanager.purgeSkater(_skater);
                    if (result > 0)
                    {
                        MessageBox.Show("Delete Successful");
                        grabAllSkaters();
                    }
                    else { throw new ApplicationException(); }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("delete failed");
                }



            }
            else { MessageBox.Show("Pick something, ya goon!"); }

        }

        private void datSkater_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {//open edit skater
            if (datSkater.SelectedItems.Count != 0)
            {
                var _skater = datSkater.SelectedItem as Skater;
                Window UpdateSkater = new SkaterAddEditDelete(_skater);
                bool result = (bool)UpdateSkater.ShowDialog();
                if (result)
                {
                    MessageBox.Show("Update sucessful");

                    grabAllSkaters();

                }
                else { MessageBox.Show("Update failed"); }

            }
            else { MessageBox.Show("Pick something, ya goon!"); }
        }



        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void grabAllSkaters()
        {

            try
            {

                datSkater.ItemsSource = null;
                allSkaters = MainWindow._skatermanager.getAllSkater();

                datSkater.ItemsSource = allSkaters;







            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _skatermanager = new SkaterManager();
            if (mode != 1)
            {
                grabAllSkaters();
            }

        }
    }
}
