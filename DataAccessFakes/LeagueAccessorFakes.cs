using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessFakes
{
    public class LeagueAccessorFakes : ILeagueAccessor
    {
        private List<League> fakeLeagues = new List<League>();

        public LeagueAccessorFakes()
        {
            League League1 = new League();
            League1.LeagueID = "Fake1";
            League1.Region = "Midwest";
            League1.Gender = "Female";
            fakeLeagues.Add(League1);
            League League2 = new League();
            League2.LeagueID = "Fake2";
            League2.Region = "Chicago";
            League2.Gender = "Female";
            fakeLeagues.Add(League2);
            League League3 = new League();
            League3.LeagueID = "Fake3";
            League3.Region = "Chicago";
            League3.Gender = "Male";
            fakeLeagues.Add(League3);


        }

        public League selectLeagueByPrimaryKey(string LeagueID)
        {
            League result = null;
            foreach (League league in fakeLeagues)
            {
                if (league.LeagueID == LeagueID)
                {
                    result = league;
                }

            }
            if (result == null)
            {
                throw new ApplicationException("league not found");
            }
            return result;
        }

        public int addLeague(string LeagueID, string Region, String Gender)
        {
            int starting = fakeLeagues.Count;
            League newLeague = new League();
            newLeague.LeagueID = LeagueID;
            newLeague.Region = Region;
            newLeague.Gender = Gender;
            fakeLeagues.Add(newLeague);
            int ending = fakeLeagues.Count;
            int result = ending = starting;
            return result;
        }

        public List<League> selectAllLeague()
        {
            return fakeLeagues;


        }

        public int updateLeague(string oldLeagueID, string oldRegion, string oldGender, string newRregion, string newGender)
        {
            int rows = 0;

            foreach (League league in fakeLeagues)
            {
                if (league.LeagueID == oldLeagueID && league.Region == oldRegion && league.Gender == oldGender)
                {

                    league.Region = newRregion;
                    league.Gender = newGender;

                    rows++;
                }

            }
            if (rows != 1)
            {
                throw new ApplicationException("Bad leagueId, Region, or Gender (6). Update Failed");
            }
            return rows;

        }

        public int deleteLeague(string LeagueID)
        {
            int result = 0;
            int IndexToDelete = fakeLeagues.Count + 1;
            for (int i = 0; i < fakeLeagues.Count; i++)
            {
                if (fakeLeagues[i].LeagueID == LeagueID)
                {
                    IndexToDelete = i;
                    result = 1;
                    break;
                }
            }
            if (result == 0) { return result; }
            if (result == 1)
            {
                fakeLeagues.RemoveAt(IndexToDelete);




                return result;
            }
            else { return result; }
        }



    }
}
