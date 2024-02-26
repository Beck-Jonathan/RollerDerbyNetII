﻿using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;  // provides hash libraries
using System.Text;

namespace LogicLayer
{
    public class SkaterManager : ISkaterManager
    {
        //dependency inversion
        private ISkaterAccessor _skaterAccessor = null;
        private ISkater_Role_LineAccessor _roleAccessor = null;

        //defailt constructor will use database
        public SkaterManager()
        {
            _skaterAccessor = new SkaterAccessor();
            _roleAccessor = new Skater_Role_LineAccessor();
        }

        //the optional contructor can accept any data providero
        public SkaterManager(ISkaterAccessor skaterAccessor)
        {
            _skaterAccessor = skaterAccessor;
            _roleAccessor = new Skater_Role_LineAccessor();

        }
        public bool AuthenticateSkater(string email, string password)
        {
            bool result = false;
            password = hashSHA256(password);
            result = (1 == _skaterAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password));
            return result;
        }

        public SkaterVM GetSkaterVMByDerbyName(string derbyName)
        {
            SkaterVM skater = null;
            try
            {
                skater = _skaterAccessor.SelectSkaterVMByDerbyName(derbyName);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Skater Not Found", ex);
            }


            return skater;
        }

        //this function will get the roles for the skater
        public List<string> getRolesBySkaterDerbyName(string derbyName)
        {
            List<string> roles = new List<string>();
            //roles.Add("");
            //roles.Add("");
            try
            {
                roles = _skaterAccessor.SelectRolesByDerbyName(derbyName);
            }
            catch (Exception)
            {

                throw;
            }
            return roles;
        }
        //this function will hash the password
        public string hashSHA256(string source)
        {
            string hashValue = "";
            //hash functions run at the bit and byte level
            byte[] data;

            //create a .net hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));

            }
            //create an output string builder object
            //loop through byte array to build a return string
            //using a for loop
            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            hashValue = s.ToString();
            return hashValue;
        }

        //this function will log in the skater with valid password and id
        public SkaterVM LoginSkater(string SkaterID, string password)
        {
            SkaterVM skaterVM = null;
            try
            {
                if (AuthenticateSkater(SkaterID, password))
                {
                    skaterVM = GetSkaterVMByDerbyName(SkaterID);
                    skaterVM.Roles = getRolesBySkaterDerbyName(skaterVM.SkaterID);
                }
                else { throw new ArgumentException("Auth Failed"); }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Auth Failed", ex);
            }





            return skaterVM;
        }
        //this function will reset the users password
        public bool resetPassword(string derbyName, string oldPassword, string newPassword)
        {
            bool result = false;

            oldPassword = hashSHA256(oldPassword);
            newPassword = hashSHA256(newPassword);
            try
            {
                result = (1 == _skaterAccessor.UpdatePasswordHash(derbyName, oldPassword, newPassword));
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Invalid Credentials (1)", ex);
            }

            return result;
        }

        //this function will add a skater to the database, and assign them the default role
        public int addSkater(SkaterVM skt)
        {
            int addresult = 0;
            int roleresult = 0;
            try
            {
                addresult = _skaterAccessor.addSkater(skt);
                if (addresult == 0) { throw new ApplicationException("add failed"); }
                roleresult = _roleAccessor.insertSkater_Role_Line("Skater", skt.SkaterID);
                if (roleresult == 0) { throw new ApplicationException("Role Assignment failed"); }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return addresult;
        }


        //this function will get all skaters
        public List<SkaterVM> getAllSkater()
        {
            List<SkaterVM> allSkaters = null;
            try
            {
                allSkaters = _skaterAccessor.selectAllSkater();
                if (allSkaters == null)
                {
                    throw new ApplicationException("skaters not found");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return allSkaters;
        }
        //this function will edit skater. It's called in a few locaitons, such as assign them to
        //a team, or just full update by an admin
        public int editSkater(Skater _oldSkater, Skater _newSkater)
        {
            {
                int result = 0;
                try
                {
                    result = _skaterAccessor.updateSkater(_oldSkater, _newSkater);
                    if (result == 0) { throw new ApplicationException("update failed"); }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("update failed", ex);
                }
                return result;
            }
        }
        //this function will mark a skater as inactive

        public int purgeSkater(Skater _skater)
        {
            {
                int result = 0;
                try
                {
                    result = _skaterAccessor.deleteSkater(_skater);
                    if (result == 0) { throw new ApplicationException("delete failed"); }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("delete failed", ex);
                }
                return result;
            }
        }

        //this function will will mark an inactive skakter as active again
        public int unpurgeSkater(Skater _skater)
        {
            {
                int result = 0;
                try
                {
                    result = _skaterAccessor.undeleteSkater(_skater);
                    if (result == 0) { throw new ApplicationException("restore failed"); }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("restore failed", ex);
                }
                return result;
            }
        }

        public List<string> getAllApplicationStatus()
        {
            List<string> result = new List<string>();
            try
            {
                result = _skaterAccessor.selectAllApplicationStatus();
                if (result.Count == 0) { throw new ApplicationException("unable to grab statuses"); }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return result;
        }
    }
}
