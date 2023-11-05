using DataAccessInterfaces;
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

        //defailt constructor will use database
        public SkaterManager()
        {
            _skaterAccessor = new SkaterAccessor();
        }

        //the optional contructor can accept any data providero
        public SkaterManager(ISkaterAccessor skaterAccessor)
        {
            _skaterAccessor = skaterAccessor;

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

        public bool createSkater(Skater skt)
        {
            bool result = false;
            try
            {
                result = (1 == _skaterAccessor.addSkater(skt.SkaterID, skt.TeamID, skt.GivenName, skt.FamilyName, skt.Phone, skt.Email));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return result;
        }


    }
}
