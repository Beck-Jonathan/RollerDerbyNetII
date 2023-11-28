using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessFakes
{
    public class TeamAccessorFakes : ITeamAccessor
    {
        private List<Team> faketeams = new List<Team>();

        public TeamAccessorFakes()
        {
            TeamVM team1 = new TeamVM();
            team1.TeamId = "Blackhawks";
            team1.LeagueID = "WFTDA";
            team1.MonthlyDue = 40;
            team1.City = "Chicago";
            team1.State = "IL";
            team1.Zip = "55214";
            faketeams.Add(team1);
            TeamVM team2 = new TeamVM();
            team2.TeamId = "Blues";
            team2.LeagueID = "WFTDA";
            team2.MonthlyDue = 45;
            team2.City = "St Louis";
            team2.State = "Mo";
            team2.Zip = "52452";
            faketeams.Add(team2);
            TeamVM team3 = new TeamVM();
            team3.TeamId = "Wild";
            team3.LeagueID = "MFTDA";
            team3.MonthlyDue = 50;
            team3.City = "St. Paul";
            team3.State = "MN";
            team3.Zip = "44521";
            faketeams.Add(team3);
        }

        public int deleteTeam(string TeamID)
        {
            throw new NotImplementedException();
        }

        public int insertTeam(Team team)
        {
            int startingSize = faketeams.Count;

            faketeams.Add(team);

            int endingSize = faketeams.Count;
            //return endingSize - startingSize;
            return 1;
        }

        public List<Team> selectAllTeam()
        {



            return faketeams;

        }

        public Team selectTeamByPrimaryKey(string TeamID)
        {
            Team result = new Team();
            try
            {
                foreach (TeamVM _team in faketeams)
                {
                    if (_team.TeamId == TeamID)
                    {
                        result.TeamId = _team.TeamId;
                        result.LeagueID = _team.LeagueID;
                        result.City = _team.City;
                        result.State = _team.State;

                        result.Zip = _team.Zip;


                    }


                }
                if (result.TeamId == null) { throw new ApplicationException(); }
            }
            catch (ApplicationException ex) { throw ex; }



            return result;
        }

        public int updateTeam(Team oldTeam, Team newTeam)
        {
            int result = 0;
            try
            {


                foreach (TeamVM _team in faketeams)
                {
                    if (_team.TeamId == oldTeam.TeamId && _team.LeagueID == oldTeam.LeagueID && _team.MonthlyDue == oldTeam.MonthlyDue && _team.City == oldTeam.City && _team.State == oldTeam.State && _team.Zip == oldTeam.Zip)
                    {

                        _team.LeagueID = newTeam.LeagueID;
                        _team.City = newTeam.City;
                        _team.State = newTeam.State;
                        _team.MonthlyDue = newTeam.MonthlyDue;
                        _team.Zip = newTeam.Zip;
                        result = 1;


                    }
                    if (result == 0) { throw new ApplicationException(); }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
