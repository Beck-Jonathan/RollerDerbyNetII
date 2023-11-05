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

        public int insertTeam(string TeamId, string LeagueID,decimal Monthlydue, string City, string State, string Zip)
        {
            int startingSize = faketeams.Count;
            TeamVM _newTeam = new TeamVM();
            _newTeam.TeamId = TeamId;
            _newTeam.LeagueID = LeagueID;
            _newTeam.MonthlyDue = 55;
            _newTeam.City = City;
            _newTeam.State = State;
            _newTeam.Zip = Zip;
            faketeams.Add(_newTeam);

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

        public int updateTeam(string oldTeamId, string oldLeagueID,decimal oldMonthlyDue ,string oldCity, string oldState, string oldZip, string newLeagueID,decimal newMonthlyDue ,string newCity, string newState, string newZip)
        {
            int result = 0;
            try
            {


                foreach (TeamVM _team in faketeams)
                {
                    if (_team.TeamId == oldTeamId && _team.LeagueID == oldLeagueID &&_team.MonthlyDue==oldMonthlyDue &&_team.City == oldCity && _team.State == oldState && _team.TeamId == oldZip)
                    {

                        _team.LeagueID = newLeagueID;
                        _team.City = newCity;
                        _team.State = newState;
                        _team.MonthlyDue = newMonthlyDue;
                        _team.Zip = newZip;
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
