using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class LeagueManager : ILeagueManager
    {
        private ILeagueAccessor _leagueAccessor = null;

        public LeagueManager()
        {
            _leagueAccessor = new LeagueAccessor(); //use the database
        }

        public LeagueManager(ILeagueAccessor leagueAccessor)
        {
            _leagueAccessor = leagueAccessor;
        }

        //this function will create a league and add it to db
        public bool createLeague(League league)
        {
            bool result = false;
            try
            {
                result = (1 == _leagueAccessor.addLeague(league.LeagueID, league.Region, league.Gender));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return result;
        }
        //this function will mark a league as inactive
        public int deleteLeague(League league)
        {
            int result = 0;
            try
            {
                result = _leagueAccessor.deleteLeague(league);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("delete failed", ex);
            }


            return result;
        }


        //this function will grab all leagues
        public List<League> getAllLeague()
        {
            List<League> results = null;
            try
            {
                results = _leagueAccessor.selectAllLeague();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("leagues not found", ex);
            }
            return results;
        }


        //this function will get a league by league id
        public League getLeagueByPrimaryKey(string LeagueID)
        {
            League league = null;
            try
            {
                league = _leagueAccessor.selectLeagueByPrimaryKey(LeagueID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("league not found", ex);
            }
            return league;
        }
        //this function will update a league
        public int updateLeague(League oldLeague, League newLeague)
        {
            int result = 0;

            try
            {
                result = _leagueAccessor.updateLeague(oldLeague.LeagueID, oldLeague.Region, oldLeague.Gender, newLeague.Region, newLeague.Gender);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("league not found, update failed", ex);
            }
            return result;
        }
    }
}
