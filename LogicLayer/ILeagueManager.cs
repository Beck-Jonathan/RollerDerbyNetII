using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ILeagueManager
    {
        bool createLeague(League league);
        League getLeagueByPrimaryKey(string leagueID);
        List<League> getAllLeague();
        int updateLeague(League oldLeague, League newLeague);
        int deleteLeague(League league);
    }
}
