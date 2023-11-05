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

        public bool createLeague(string LeagueID, string Region, String Gender)
        {
            bool result = false;
            try
            {
                result = (1 == _leagueAccessor.addLeague(LeagueID, Region, Gender));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return result;
        }

        public int deleteLeague(string LeagueId)
        {
            int result = 0;
            try
            {
                result = _leagueAccessor.deleteLeague(LeagueId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("delete failed", ex);
            }


            return result;
        }

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

        public int updateLeague(string oldLeagueID, string oldRegion, string oldGender, string newLeagueID, string newregion, string newGender)
        {
            int result = 0;

            try
            {
                result = _leagueAccessor.updateLeague(oldLeagueID, oldRegion, oldGender, newregion, newGender);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("league not found, update failed", ex);
            }
            return result;
        }
    }
}
