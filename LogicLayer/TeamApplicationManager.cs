using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class TeamApplicationManager : ITeamApplicationManager
    {
        private ITeamApplicationAccessor _applicationAccessor = null;

        //defailt constructor will use database
        public TeamApplicationManager()
        {
            _applicationAccessor = new TeamApplicationAccessor();
        }

        //the optional contructor can accept any data providero
        public TeamApplicationManager(ITeamApplicationAccessor applicationAccessor)
        {
            _applicationAccessor = applicationAccessor;

        }
        //this function will add a team application
        public int addTeamApplication(TeamApplication _TeamApplication)
        {
            int result = 0;
            try
            {
                result = _applicationAccessor.addTeamApplication(_TeamApplication);
                if (result == 0) { throw new ApplicationException("add failed"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        //this function will edit a team application
        public int editTeamApplication(TeamApplication _oldTeamApplication, TeamApplication _newTeamApplication)
        {
            int result = 0;
            try
            {
                result = _applicationAccessor.updateTeamApplication(_oldTeamApplication, _newTeamApplication);
                if (result == 0) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return result;
        }
        //this function will grab all team applications
        public List<TeamApplication> getAllTeamApplication()
        {
            List<TeamApplication> teamApplications = new List<TeamApplication>();
            try
            {
                teamApplications = _applicationAccessor.selectAllTeamApplication();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return teamApplications;
        }
        //this function will get the team application by PK
        public TeamApplication getTeamApplicationByPrimaryKey(int TeamApplicationID)
        {
            TeamApplication result= null;
            try
            {
                result = _applicationAccessor.selectTeamApplicationByPrimaryKey(TeamApplicationID);
                if (result == null) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
