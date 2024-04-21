using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class SkaterteamApplicationManager : ISkaterteamApplicationManager
    {
        private ISkaterTeamApplicationAccessor _applicationAccessor = null;

        //defailt constructor will use database
        public SkaterteamApplicationManager()
        {
            _applicationAccessor = new SkaterteamApplicationAccessor();
        }

        //the optional contructor can accept any data providero
        public SkaterteamApplicationManager(ISkaterTeamApplicationAccessor applicationAccessor)
        {
            _applicationAccessor = applicationAccessor;

        }


        public int addSkaterteamApplication(SkaterTeamApplication _SkaterteamApplication)
        {
            int result = 0;
            int applicationID = _SkaterteamApplication.ApplicationID;
            string skaterId = _SkaterteamApplication.skaterID;
            string team = _SkaterteamApplication.TeamName;
            string status = _SkaterteamApplication.ApplicationStatus;
            try
            {
                result = _applicationAccessor.addSkaterTeamApplication(applicationID, skaterId, team, status);
                if (result == 0) { throw new ApplicationException("add failed"); }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("add failed", ex);
            }
            return result;
        }

        public int editSkaterteamApplication(SkaterTeamApplication _oldSkaterteamApplication, SkaterTeamApplication _newSkaterteamApplication)
        {
            int result = 0;
            int applicationID = _oldSkaterteamApplication.ApplicationID;
            string oldskaterId = _oldSkaterteamApplication.skaterID;
            string oldteam = _oldSkaterteamApplication.TeamName;
            string oldstatus = _oldSkaterteamApplication.ApplicationStatus;
            string newskaterId = _newSkaterteamApplication.skaterID;
            string newteam = _newSkaterteamApplication.TeamName;
            string newstatus = _newSkaterteamApplication.ApplicationStatus;

            try
            {

                result = _applicationAccessor.updateSkaterteamApplication(applicationID, oldskaterId, oldteam, oldstatus, newskaterId, newteam, newstatus);
                if (result == 0) { throw new ApplicationException("edit failed"); }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("edit failed", ex);
            }




            return result;
        }

        public List<SkaterTeamApplication> getAllSkaterteamApplication()
        {
            List<SkaterTeamApplication> values = new List<SkaterTeamApplication>();
            try
            {
                values = _applicationAccessor.selectAllSkaterteamApplication();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Applications not found", ex);
            }
            return values;
        }

        public SkaterTeamApplication getSkaterteamApplicationByPrimaryKey(int SkaterteamApplicationID)
        {
            SkaterTeamApplication _app = null;
            try
            {
                _app = _applicationAccessor.selectSkaterteamApplicationByPrimaryKey(SkaterteamApplicationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Application Not Found", ex);
            }
            return _app;
        }

        public int purgeSkaterteamApplication(int SkaterteamApplicationID)
        {
            int result = 0;
            try
            {
                result = _applicationAccessor.deleteSkaterteamApplication(SkaterteamApplicationID);
                if (result == 0) { throw new ApplicationException("delete failed"); }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("application not found", ex);
            }




            return result;
        }

        public List<SkaterTeamApplication> getSkaterTeamApplicationBySkaterID(Skater _skater)
        {
            List<SkaterTeamApplication> values = new List<SkaterTeamApplication>();
            try
            {
                values = _applicationAccessor.selectSkaterTeamApplicationBySkaterID(_skater);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Applications not found", ex);
            }
            return values;
        }
    }
}
