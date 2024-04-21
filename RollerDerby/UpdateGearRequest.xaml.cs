using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for UpdateGearRequest.xaml
    /// </summary>
    public partial class UpdateGearRequest : Window
    {
        SkaterManager _sm;
        GearRequestManager _grm;
        GearRequest _gear;
        GearApplicationManager _gearApplicationManager;
        GearApplication _gearApplication;
        public UpdateGearRequest(GearApplication ga)
        {   //take the gear application and get the gear request associated with it
            _gearApplication = ga;
            _grm = new GearRequestManager();
            try
            {
                _gear = _grm.getGearRequestByPrimaryKey(ga.GearReuestID);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            _sm = new SkaterManager();
            _grm = new GearRequestManager();
            InitializeComponent();
            cbxElbowSize.Text = _gear.ElbowPadSize.ToString();
            cbxHelmSize.Text = _gear.HelmSize.ToString();
            cbxSkateSize.Text = _gear.SkateSize.ToString();
            cbxKneeSize.Text = _gear.KneePadSize.ToString();
            cbxWristSize.Text = _gear.WristGuardSize.ToString();





            cbxElbowSize.IsEnabled = false;
            cbxHelmSize.IsEnabled = false;
            cbxKneeSize.IsEnabled = false;
            cbxSkateSize.IsEnabled = false;
            cbxWristSize.IsEnabled = false;
            cbxStatus.IsEnabled = true;
            List<String> allStatus = new List<String>();
            try
            {
                allStatus = _sm.getAllApplicationStatus();
                if (allStatus.Count == 0) { throw new ApplicationException("status not found"); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            cbxStatus.ItemsSource = allStatus;
            cbxStatus.Text = ga.ApplicationStatus.ToString();
            //cbxStatus.Text=gr.




        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            //apply the updated status
            _gearApplicationManager = new GearApplicationManager();
            GearApplication newGA = new GearApplication();
            GearInventoryManager GIM = new GearInventoryManager();
            List<GearInventory> allGear = GIM.getAllGearInventory();
            newGA.ApplicationID = _gearApplication.ApplicationID;
            newGA.ApplicationTime = _gearApplication.ApplicationTime;
            newGA.GearReuestID = _gearApplication.GearReuestID;
            newGA.SkaterID = _gearApplication.SkaterID;
            newGA.ApplicationStatus = cbxStatus.Text;
            GearInventory oldelbow = new GearInventory();
            GearInventory newelbow = new GearInventory();
            if (cbxElbowSize.Text != "NA")
            {

                oldelbow.GearPart = "Elbow";
                oldelbow.GearSize = cbxElbowSize.Text;
                foreach (GearInventory gear in allGear)
                {
                    if (gear.GearPart == "Elbow" && gear.GearSize == oldelbow.GearSize)
                    {
                        oldelbow.GearCount = gear.GearCount; break;
                    }
                }

                newelbow.GearPart = "Elbow";
                newelbow.GearSize = cbxElbowSize.Text;
                newelbow.GearCount = oldelbow.GearCount - 1;

            }
            GearInventory oldhelm = new GearInventory();
            GearInventory newhelm = new GearInventory();
            if (cbxHelmSize.Text != "NA")
            {

                oldhelm.GearPart = "Helm";
                oldhelm.GearSize = cbxHelmSize.Text;
                foreach (GearInventory gear in allGear)
                {
                    if (gear.GearPart == "Helm" && gear.GearSize == oldhelm.GearSize)
                    {
                        oldhelm.GearCount = gear.GearCount; break;
                    }
                }

                newhelm.GearPart = "Helm";
                newhelm.GearSize = cbxHelmSize.Text;
                newhelm.GearCount = oldhelm.GearCount - 1;

            }
            GearInventory oldWrist = new GearInventory();
            GearInventory newWrist = new GearInventory();
            if (cbxWristSize.Text != "NA")
            {

                oldWrist.GearPart = "Wrist";
                oldWrist.GearSize = cbxElbowSize.Text;
                foreach (GearInventory gear in allGear)
                {
                    if (gear.GearPart == "Wrist" && gear.GearSize == oldWrist.GearSize)
                    {
                        oldWrist.GearCount = gear.GearCount; break;
                    }
                }

                newWrist.GearPart = "Wrist";
                newWrist.GearSize = cbxWristSize.Text;
                newWrist.GearCount = oldWrist.GearCount - 1;

            }
            GearInventory oldKnee = new GearInventory();
            GearInventory newKnee = new GearInventory();
            if (cbxElbowSize.Text != "NA")
            {

                oldKnee.GearPart = "Knee";
                oldKnee.GearSize = cbxKneeSize.Text;
                foreach (GearInventory gear in allGear)
                {
                    if (gear.GearPart == "Knee" && gear.GearSize == oldKnee.GearSize)
                    {
                        oldKnee.GearCount = gear.GearCount; break;
                    }
                }

                newKnee.GearPart = "Knee";
                newKnee.GearSize = cbxKneeSize.Text;
                newKnee.GearCount = oldKnee.GearCount - 1;

            }
            GearInventory oldSkate = new GearInventory();
            GearInventory newSkate = new GearInventory();
            if (cbxElbowSize.Text != "NA")
            {

                oldSkate.GearPart = "Skate";
                oldSkate.GearSize = cbxSkateSize.Text;
                foreach (GearInventory gear in allGear)
                {
                    if (gear.GearPart == "Skate" && gear.GearSize == oldSkate.GearSize)
                    {
                        oldSkate.GearCount = gear.GearCount; break;
                    }
                }

                newSkate.GearPart = "Skate";
                newSkate.GearSize = cbxSkateSize.Text;
                newSkate.GearCount = oldSkate.GearCount - 1;

            }


            try
            {
                int result = 0;
                result = _gearApplicationManager.editGearApplication(_gearApplication, newGA);
                //this bit is just an experiment for now, needs a lot of work
                if (cbxElbowSize.Text != "NA") { GIM.editGearInventory(oldelbow, newelbow); }
                if (cbxHelmSize.Text != "NA") { GIM.editGearInventory(oldhelm, newhelm); }
                if (cbxWristSize.Text != "NA") { GIM.editGearInventory(oldWrist, newWrist); }
                if (cbxSkateSize.Text != "NA") { GIM.editGearInventory(oldSkate, newSkate); }
                if (cbxKneeSize.Text != "NA") { GIM.editGearInventory(oldKnee, newKnee); }








                if (result == 0)
                {
                    throw new ApplicationException("Update failed");


                }
                else
                {
                    MessageBox.Show("updated!");
                    //this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.DialogResult = false;
            }







        }
    }
}
