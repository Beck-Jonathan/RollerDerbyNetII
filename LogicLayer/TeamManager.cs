using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LogicLayer
{
    public class TeamManager : ITeamManager
    {

        private ITeamAccessor _teamAccessor = null;

        //defailt constructor will use database
        public TeamManager()
        {
            _teamAccessor = new TeamAccessor();
        }

        //the optional contructor can accept any data provider
        public TeamManager(ITeamAccessor teamAccessor)
        {
            _teamAccessor = teamAccessor;

        }
        //this function will add a team
        public bool addTeam(Team _team)
        {
            bool result = false;
            try
            {
                result = (1 == _teamAccessor.insertTeam(_team));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return result;
        }
        //this function will edit an existing team
        public int editTeam(Team _old, Team _new)
        {
            int result = 0;

            try
            {
                result = _teamAccessor.updateTeam(_old, _new);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Team not found, update failed", ex);
            }
            return result;
        }
        //this function will grab all teams
        public List<Team> getAllTeam()
        {
            List<Team> results = null;
            try
            {
                results = _teamAccessor.selectAllTeam();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Teams not found", ex);
            }
            return results;
        }
        //this function will great teams by team id
        public Team getTeamByPrimaryKey(string TeamID)
        {
            Team team = null;
            try
            {
                team = _teamAccessor.selectTeamByPrimaryKey(TeamID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("team not found", ex);
            }
            return team;
        }
        //this function will mark a team as inactive
        public int purgeTeam(string TeamID)
        {
            int result = 0;
            try
            {
                result = _teamAccessor.deleteTeam(TeamID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("delete failed", ex);
            }


            return result;
        }
    }
}
