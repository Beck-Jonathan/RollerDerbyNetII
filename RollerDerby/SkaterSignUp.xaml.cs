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
using DataObjects;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for SkaterSignUp.xaml
    /// </summary>
    public partial class SkaterSignUp : Window
    {
        SkaterManager _sm = null;
        public SkaterSignUp()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sm = new SkaterManager();
        }

        private bool isValid() {
            //test all inputs were filled in proerply
            return tbxSkateremail.Text.isValidEmail()
                    && tbxSkaterFamilyName.Text.isValidNVarChar50()
                    && tbxSkaterGivenName.Text.isValidNVarChar50()
                    && tbxSkaterPhone.Text.isValidPhone()
                    && tbxSkaterSkaterID.Text.isValidNVarChar50();
        
        
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //validate inputs, then create new user
            if (isValid()) {
                int result = 0;
                SkaterVM _skater = new SkaterVM();
                _skater.SkaterID = tbxSkaterSkaterID.Text;
                _skater.Email = tbxSkateremail.Text;
                _skater.GivenName = tbxSkaterGivenName.Text;
                _skater.FamilyName = tbxSkaterFamilyName.Text;
                _skater.Phone = tbxSkaterPhone.Text;
                try
                {
                    result = _sm.addSkater(_skater);

                    if (result == 0) { throw new ApplicationException(); }
                    else { MessageBox.Show("User Registered, please sign in"); }
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Unable to register user");
                    this.DialogResult = false;
                }
            
            
            
            }
            else { MessageBox.Show("Invalid inputs"); }
        }
    }
}
