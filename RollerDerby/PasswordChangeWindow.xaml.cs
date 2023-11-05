using DataObjects;
using LogicLayer;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        static SkaterManager _sm;
        static Skater _skt;
        public PasswordChangeWindow(Skater loggedinSkater, SkaterManager skatermanager)
        {
            InitializeComponent();
            _sm = skatermanager;
            _skt = loggedinSkater;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            string oldPW = pwdOldPW.Password;
            string newPW1 = pwdNewPW.Password;
            string newPW2 = pwdConfirmPW.Password;
            try
            {
                if (newPW1 == "newUser")
                {
                    MessageBox.Show("You can not set this as your password");
                    this.DialogResult = false;
                }
                if (newPW1 == oldPW) { MessageBox.Show("You can not reuse your old password"); }


                else if (newPW1 == newPW2)
                {
                    if (_sm.resetPassword(_skt.SkaterID, oldPW, newPW1))
                    {
                        this.DialogResult = true;
                    };
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.DialogResult = false;

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
