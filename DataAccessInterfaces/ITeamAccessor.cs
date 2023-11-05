using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ITeamAccessor
    {
        int insertTeam(string TeamId, string LeagueID, decimal MonthlyDue, string City, string State, string Zip);
        Team selectTeamByPrimaryKey(string TeamID);
        List<Team> selectAllTeam();
        int updateTeam(string oldTeamId, string oldLeagueID, decimal oldMonthlyDue, string oldCity, string oldState, string oldZip, string newLeagueID, decimal newMonthlyDue, string newCity, string newState, string newZip);
        int deleteTeam(string TeamID);
    }
}
