using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ITeamApplicationAccessor
    {
        int addTeamApplication(TeamApplication _TeamApplication);
        TeamApplication selectTeamApplicationByPrimaryKey(int TeamApplicationID);
        List<TeamApplication> selectAllTeamApplication();
        int updateTeamApplication(TeamApplication _oldTeamApplication, TeamApplication _newTeamApplication);

    }
}
