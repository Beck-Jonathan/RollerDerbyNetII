using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

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
            List<GearInventory> gear = new List<GearInventory>();

            InitializeComponent();
            try
            {
                gear = GIM.getAllGearInventory();
                if (gear.Count == 0) { throw new ApplicationException("inventory not found"); }
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
