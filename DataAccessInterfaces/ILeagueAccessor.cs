using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ILeagueAccessor
    {
        int addLeague(string LeagueID, string Region, string Gender);
        League selectLeagueByPrimaryKey(string LeagueID);
        List<League> selectAllLeague();
        int updateLeague(string oldLeagueID, string oldRegion, string oldGender, string newRegion, string newGender);
        int deleteLeague(string LeagueID);
    }
}
