using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SkaterAccessor : ISkaterAccessor
    {
        public int AuthenticateUserWithEmailAndPasswordHash(string skaterID, string passwordHash)
        {
            int rows = 0;
            //start with connection object

            var conn = SqlConnectionProvider.GetConnection();



            //set the command text

            var commandText = "sp_auth_skater";

            // create the command object

            var cmd = new SqlCommand(commandText, conn);

            //set command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters

            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            //we need to set the parameter values

            cmd.Parameters["@SkaterID"].Value = skaterID;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            //now that everything is setup , we can open connection and execute command in a try/catch/finally

            try
            {
                // open connection
                conn.Open();
                //execute command and capture results
                rows = Convert.ToInt32(cmd.ExecuteScalar());


            }

            catch (Exception ex) { throw ex; }

            finally
            {
                conn.Close();
            }



            return rows;
        }

        public SkaterVM SelectSkaterVMByDerbyName(string derbyName)
        {
            SkaterVM skaterVM = new SkaterVM();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_get_skater_name_by_SkaterID";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            // parameter values
            cmd.Parameters["@SkaterID"].Value = derbyName;
            try
            {
                // open the connection
                conn.Open();
                // execute

                var reader = cmd.ExecuteReader();
                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        skaterVM.SkaterID = reader.GetString(0);
                        skaterVM.GivenName = reader.GetString(1);
                        skaterVM.FamilyName = reader.GetString(2);
                        skaterVM.Phone = reader.GetString(3);
                        skaterVM.Email = reader.GetString(4);
                        skaterVM.Active = reader.GetBoolean(6);

                        //skaterVM.Active = reader.GetBoolean(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Skater not found (2)");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return skaterVM;
        }

        public List<string> SelectRolesByDerbyName(string derbyName)
        {
            List<string> roles = new List<string>();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_select_roles_by_SkaterID";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            // parameter values
            cmd.Parameters["@SkaterID"].Value = derbyName;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }


        public int UpdatePasswordHash(string derbyName, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_passwordHash";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            // we need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = derbyName;
            cmd.Parameters["@oldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;
            // now that everything is set up, we can open the connection and
            // execute the command in a try-catch-finally
            try
            {
                //update is execute non quory
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    //treat failed update as an exception
                    throw new ArgumentException("invalid email password (8)");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }


            return rows;
        }

        public int addSkater(string SkaterID, string TeamID, string GivenName, string FamilyName, string Phone, string email)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Skater";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@TeamID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 250);


            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = SkaterID;
            cmd.Parameters["@TeamID"].Value = TeamID;
            cmd.Parameters["@GivenName"].Value = GivenName;
            cmd.Parameters["@FamilyName"].Value = FamilyName;
            cmd.Parameters["@Phone"].Value = Phone;
            cmd.Parameters["@email"].Value = email;

            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<Skater> selectAllSkater()
        {
            List<Skater> output = new List<Skater>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Skater";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // There are no parameters to set or add
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var _Skater = new Skater();
                        _Skater.SkaterID = reader.GetString(0);
                        _Skater.TeamID = reader.GetString(1);
                        _Skater.GivenName = reader.GetString(2);
                        _Skater.FamilyName = reader.GetString(3);
                        _Skater.Phone = reader.GetString(4);
                        _Skater.Email = reader.GetString(5);
                        //_Skater.passwordhash = reader.GetString(6);
                        _Skater.Active = reader.GetBoolean(7);
                        output.Add(_Skater);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return output;
        }
        public int updateSkater(string oldSkaterID, string oldTeamID, string oldGivenName, string oldFamilyName, string oldPhone, string oldemail, string oldpasswordhash, bool oldactive, string newTeamID, string newGivenName, string newFamilyName, string newPhone, string newemail, string newpasswordhash, bool newactive)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Skater";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@oldTeamID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newTeamID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldGivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newGivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldFamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newFamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldPhone", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@newPhone", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@oldemail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@newemail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@oldpasswordhash", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@newpasswordhash", SqlDbType.NVarChar, 256);
            cmd.Parameters.Add("@oldactive", SqlDbType.Bit);
            cmd.Parameters.Add("@newactive", SqlDbType.Bit);

            //We need to set the parameter values
            cmd.Parameters["@oldSkaterID"].Value = oldSkaterID;

            cmd.Parameters["@oldTeamID"].Value = oldTeamID;
            cmd.Parameters["@newTeamID"].Value = newTeamID;
            cmd.Parameters["@oldGivenName"].Value = oldGivenName;
            cmd.Parameters["@newGivenName"].Value = newGivenName;
            cmd.Parameters["@oldFamilyName"].Value = oldFamilyName;
            cmd.Parameters["@newFamilyName"].Value = newFamilyName;
            cmd.Parameters["@oldPhone"].Value = oldPhone;
            cmd.Parameters["@newPhone"].Value = newPhone;
            cmd.Parameters["@oldemail"].Value = oldemail;
            cmd.Parameters["@newemail"].Value = newemail;
            cmd.Parameters["@oldpasswordhash"].Value = oldpasswordhash;
            cmd.Parameters["@newpasswordhash"].Value = newpasswordhash;
            cmd.Parameters["@oldactive"].Value = oldactive;
            cmd.Parameters["@newactive"].Value = newactive;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    //treat failed update as exception 
                    throw new ArgumentException("invalid values, update failed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        public int deleteSkater(string SkaterID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Skater";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            //set parameter!!!
            cmd.Parameters["@SkaterID"].Value = SkaterID;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery(); if (rows == 0)
                {
                    //treat failed delete as exepction
                    throw new ArgumentException("Invalid Primary Key");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}

