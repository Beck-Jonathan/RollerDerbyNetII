using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ITeamApplicationManager
    {
        int addTeamApplication(TeamApplication _TeamApplication);
        TeamApplication getTeamApplicationByPrimaryKey(int TeamApplicationID);
        List<TeamApplication> getAllTeamApplication();
        int editTeamApplication(TeamApplication _oldTeamApplication, TeamApplication _newTeamApplication);

    }
}
