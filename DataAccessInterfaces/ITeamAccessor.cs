using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ITeamAccessor
    {
        int insertTeam(Team team);
        Team selectTeamByPrimaryKey(string TeamID);
        List<Team> selectAllTeam();
        int updateTeam(Team oldTeam, Team newTeam);
        int deleteTeam(string TeamID);
    }
}
