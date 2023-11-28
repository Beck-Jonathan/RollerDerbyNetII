using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ITeamManager
    {
        bool addTeam(Team team);
        Team getTeamByPrimaryKey(string TeamID);
        List<Team> getAllTeam();
        int editTeam(Team _oldTeam, Team _newTeam);
        int purgeTeam(string TeamID);
    }

}
