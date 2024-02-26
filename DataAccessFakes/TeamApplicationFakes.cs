using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccessFakes
{
    public class TeamApplicationFakes : ITeamApplicationAccessor
    {
        private List<TeamApplication> fakeApplications = new List<TeamApplication>();

        public TeamApplicationFakes()
        {
            TeamApplication fakeApplication1 = new TeamApplication();

            TeamApplication fakeApplication2 = new TeamApplication();
            TeamApplication fakeApplication3 = new TeamApplication();
            fakeApplication1.ApplicationTime = DateTime.Now;
            fakeApplication1.ApplicationStatus = "pending";
            fakeApplication1.TeamApplicationID = 100;
            fakeApplication1.SkaterID = "malFoy";
            fakeApplication1.TeamName = "West Hawks";
            fakeApplications.Add(fakeApplication1);
            fakeApplication2.ApplicationTime = DateTime.Now;
            fakeApplication2.ApplicationStatus = "pending";
            fakeApplication2.TeamApplicationID = 101;
            fakeApplication2.SkaterID = "Draco";
            fakeApplication2.TeamName = "East Hawks";
            fakeApplications.Add(fakeApplication2);
            fakeApplication3.ApplicationTime = DateTime.Now;
            fakeApplication3.ApplicationStatus = "pending";
            fakeApplication3.TeamApplicationID = 102;
            fakeApplication3.SkaterID = "Harry";
            fakeApplication3.TeamName = "Wizards";
            fakeApplications.Add(fakeApplication3);

        }
        public int addTeamApplication(TeamApplication _TeamApplication)
        {
            int start = fakeApplications.Count;
            fakeApplications.Add(_TeamApplication);
            int end = fakeApplications.Count;
            return end - start;
        }

        public List<TeamApplication> selectAllTeamApplication()
        {
            return fakeApplications;
        }

        public TeamApplication selectTeamApplicationByPrimaryKey(int TeamApplicationID)
        {
            TeamApplication returnApplication = null;
            int primaryKey = TeamApplicationID;
            int result = 0;
            foreach (TeamApplication teamApplication in fakeApplications)
            {
                if (teamApplication.TeamApplicationID == primaryKey) {
                    returnApplication = teamApplication;
                    result = 1;
                return returnApplication;
                }


            }
            if (result == 0) { throw new ApplicationException(); }
            return returnApplication;
        }

        public int updateTeamApplication(TeamApplication _oldTeamApplication, TeamApplication _newTeamApplication)
        {
            int result = 0;
            foreach (TeamApplication teamApplication in fakeApplications) {
                if (teamApplication.TeamApplicationID == _oldTeamApplication.TeamApplicationID) { 
                teamApplication.ApplicationStatus=_newTeamApplication.ApplicationStatus;
                    result = 1;
                
                }
            
            } 
            if(result == 0) { throw new ApplicationException(); }



            return result;
        }
    }
}
