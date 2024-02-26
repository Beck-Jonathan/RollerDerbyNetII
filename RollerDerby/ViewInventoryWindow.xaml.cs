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
    /// Interaction logic for ViewInventoryWindow.xaml
    /// </summary>
    public partial class ViewInventoryWindow : Window
    {
        GearInventoryManager GIM;
        public ViewInventoryWindow()
        { //view all inventory
            GIM = new GearInventoryManager();
            List<GearInventory> gear=  new List<GearInventory>();
            
            InitializeComponent();
            try
            {
                gear=GIM.getAllGearInventory();
                if(gear.Count == 0 ) { throw new ApplicationException("inventory not found"); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            datInventory.ItemsSource = gear;
            
            
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
