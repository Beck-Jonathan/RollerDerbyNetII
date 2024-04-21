using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for GearRequestWindow.xaml
    /// </summary>
    public partial class GearRequestWindow : Window
    {
        GearApplicationManager manager;
        GearRequestManager requestManager;
        Skater _skt;
        GearRequest gr;
        public GearRequestWindow(Skater skt)
        {
            _skt = skt;
            InitializeComponent();
            manager = new GearApplicationManager();
            requestManager = new GearRequestManager();
            gr = new GearRequest();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //fill all combo boxes with avialable sizes
            List<string> normalSizes = new List<string>();
            normalSizes.Add("ExtraLarge");
            normalSizes.Add("Large");
            normalSizes.Add("Medium");
            normalSizes.Add("Small");
            normalSizes.Add("ExtraSmall");
            normalSizes.Add("NA");
            cbxElbowSize.ItemsSource = normalSizes;
            cbxHelmSize.ItemsSource = normalSizes;
            cbxKneeSize.ItemsSource = normalSizes;
            cbxWristSize.ItemsSource = normalSizes;
            List<string> skateSizes = new List<string>();
            for (int i = 6; i < 14; i++)
            {
                skateSizes.Add(i.ToString());

            }
            skateSizes.Add("NA");
            cbxSkateSize.ItemsSource = skateSizes;

        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            //great a new gear request, and link it to a new gear applicaiton
            if (isValid())
            {
                int RequestResult = 0;
                int ApplicationResult = 0;

                gr.ElbowPadSize = cbxElbowSize.Text;
                gr.KneePadSize = cbxKneeSize.Text;
                gr.WristGuardSize = cbxWristSize.Text;
                gr.SkateSize = cbxSkateSize.Text;
                gr.HelmSize = cbxHelmSize.Text;
                GearApplication GA = new GearApplication();
                GA.SkaterID = _skt.SkaterID;


                try
                {
                    RequestResult = requestManager.addGearRequest(gr);
                    if (RequestResult == 0)
                    {
                        throw new ApplicationException();
                    }
                    GA.GearReuestID = RequestResult;
                    ApplicationResult = manager.addGearApplication(GA);
                    if (ApplicationResult == 0) { throw new ApplicationException(); }
                    else
                    {
                        MessageBox.Show("Your Request has been added");
                        this.DialogResult = true;
                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }



            }

            else
            {
                MessageBox.Show("Please select a size for each piece of equipment. \n If you " +
                "do not need that gear, please select \"NA\" ");
            }

        }
        public bool isValid()
        {
            //validate a user has chosen something for each box
            bool a = cbxElbowSize.Text != "";
            bool b = cbxHelmSize.Text != "";
            bool c = cbxKneeSize.Text != "";
            bool d = cbxWristSize.Text != "";
            bool e = cbxSkateSize.Text != "";

            return a && b && c && d && e;


        }
    }
}
