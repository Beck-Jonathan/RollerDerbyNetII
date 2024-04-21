using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessFakes
{
    public class GearApplicationFakes : IGearApplicationAccessor
    {
        private List<GearApplication> FakeApplications = new List<GearApplication>();

        public GearApplicationFakes()
        {
            GearApplication fakeApplication1 = new GearApplication();
            GearApplication fakeApplication2 = new GearApplication();
            GearApplication fakeApplication3 = new GearApplication();
            fakeApplication1.ApplicationID = 100;
            fakeApplication1.SkaterID = "malfoy";
            fakeApplication1.GearReuestID = 100;
            fakeApplication1.ApplicationTime = DateTime.Now;
            fakeApplication1.ApplicationStatus = "Pending";
            fakeApplication2.ApplicationID = 101;
            fakeApplication2.SkaterID = "Balfoy";
            fakeApplication2.GearReuestID = 101;
            fakeApplication2.ApplicationTime = DateTime.Now;
            fakeApplication2.ApplicationStatus = "Pending";
            fakeApplication3.ApplicationID = 102;
            fakeApplication3.SkaterID = "Harry";
            fakeApplication3.GearReuestID = 102;
            fakeApplication3.ApplicationTime = DateTime.Now;
            fakeApplication3.ApplicationStatus = "Pending";


            FakeApplications.Add(fakeApplication1);
            FakeApplications.Add(fakeApplication2);
            FakeApplications.Add(fakeApplication3);



        }
        public int addGearApplication(GearApplication _GearApplication)
        {
            int starting = FakeApplications.Count();
            FakeApplications.Add(_GearApplication);
            int ending = FakeApplications.Count();
            return ending - starting;
        }

        public List<GearApplication> selectAllGearApplication()
        {
            return FakeApplications;
        }

        public GearApplication selectGearApplicationByPrimaryKey(int GearApplicationID)
        {
            GearApplication result = null;
            foreach (GearApplication gearApplication in FakeApplications)
            {
                if (GearApplicationID == gearApplication.ApplicationID)
                {
                    result = gearApplication;
                    break;
                }
            }
            if (result == null) { throw new ApplicationException(); }


            return result;
        }

        public int updateGearApplication(GearApplication _oldGearApplication, GearApplication _newGearApplication)
        {
            int result = 0;
            foreach (GearApplication gearApplication in FakeApplications)
            {
                if (gearApplication.ApplicationID == _oldGearApplication.ApplicationID)
                {
                    gearApplication.ApplicationStatus = _newGearApplication.ApplicationStatus;
                    result = 1;
                    break;

                }
                if (result == 0) { throw new ApplicationException(); }
            }

            return result;
        }
    }
}
