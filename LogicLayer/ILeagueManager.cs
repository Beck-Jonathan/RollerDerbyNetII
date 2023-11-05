using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ILeagueManager
    {
        bool createLeague(string LeagueID, string Region, string Gender);
        League getLeagueByPrimaryKey(string LeagueID);
        List<League> getAllLeague();
        int updateLeague(string oldLeagueID, string oldRegion, string oldGender, string newLeagueID, string newregion, string newGender);
        int deleteLeague(string LeagueID);
    }
}
