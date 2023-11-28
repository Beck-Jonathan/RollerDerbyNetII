using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessFakes
{
    public class SkaterTeamApplicationFakes : ISkaterTeamApplicationAccessor
    {
        private List<SkaterTeamApplication> fakeApplications = new List<SkaterTeamApplication>();

        public SkaterTeamApplicationFakes()
        {
            SkaterTeamApplication a = new SkaterTeamApplication();
            a.ApplicationID = 100;
            a.skaterID = "Alice";
            a.TeamName = "East Hawks";
            a.isActive = true;
            fakeApplications.Add(a);
            SkaterTeamApplication b = new SkaterTeamApplication();
            b.ApplicationID = 101;
            b.skaterID = "Balice";
            b.TeamName = "Central Hawks";
            b.isActive = true;
            fakeApplications.Add(b);
            SkaterTeamApplication c = new SkaterTeamApplication();
            c.ApplicationID = 102;
            c.skaterID = "Chalice";
            c.TeamName = "West Hawks";
            c.isActive = true;
            fakeApplications.Add(c);

        }

        public int addSkaterTeamApplication(int ApplicationID, string SkaterID, string TeamName, string ApplicationStatus)
        {
            SkaterTeamApplication application = new SkaterTeamApplication();
            application.ApplicationID = ApplicationID;
            application.skaterID = SkaterID;
            application.ApplicationStatus = ApplicationStatus;
            application.TeamName = TeamName;
            application.isActive = true;
            fakeApplications.Add(application);
            return 1;
        }

        public int deleteSkaterteamApplication(int SkaterteamApplicationID)
        {
            int result = 0;

            foreach (SkaterTeamApplication STA in fakeApplications)
            {


                if (STA.ApplicationID == SkaterteamApplicationID)
                {

                    STA.isActive = false;
                    result = 1;
                }
            }

            return result;
        }

        public List<SkaterTeamApplication> selectAllSkaterteamApplication()
        {
            return fakeApplications;
        }

        public SkaterTeamApplication selectSkaterteamApplicationByPrimaryKey(int SkaterteamApplicationID)
        {
            SkaterTeamApplication _app = null;
            foreach (SkaterTeamApplication STA in fakeApplications)
            {


                if (STA.ApplicationID == SkaterteamApplicationID)
                {

                    _app = STA;
                }
            }
            if (_app == null) { throw new ApplicationException(); }

            return _app;
        }

        public int updateSkaterteamApplication(int oldApplicationID, string oldSkaterID, string oldTeamName, string oldApplicationStatus, string newSkaterID, string newTeamName, string newApplicationStatus)
        {
            int result = 0;
            SkaterTeamApplication _app = null;
            foreach (SkaterTeamApplication STA in fakeApplications)
            {


                if (STA.ApplicationID == oldApplicationID && STA.skaterID == oldSkaterID && STA.TeamName == oldTeamName && STA.ApplicationStatus == oldApplicationStatus)
                {

                    _app = STA;
                    _app.ApplicationStatus = newApplicationStatus;
                    _app.TeamName = newTeamName;
                    _app.skaterID = newSkaterID;
                    result = 1; break;
                }
            }
            return result;
        }
    }

}
