using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ITeamManager
    {
        bool addTeam(string TeamId, string LeagueID,decimal MonthlyDue, string City, string State, string Zip);
        Team getTeamByPrimaryKey(string TeamID);
        List<Team> getAllTeam();
        int editTeam(string oldTeamId, string oldLeagueID, decimal oldMonthlyDue, string oldCity, string oldState, string oldZip, string newTeamId, string newLeagueID, decimal newMonthlyDue, string newCity, string newState, string newZip);
        int purgeTeam(string TeamID);
    }

}
