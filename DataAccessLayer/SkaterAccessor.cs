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
        public Skater selectSkaterByEmail(String email)
        {
            Skater skater = new Skater();
            // connection
            var conn = SqlConnectionProvider.GetConnection();
            // command text
            var cmdText = "sp_get_skater_name_by_Email";
            // command
            var cmd = new SqlCommand(cmdText, conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50);
            // parameter values
            cmd.Parameters["@Email"].Value = email;
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
                        skater.SkaterID = reader.GetString(0);
                        skater.GivenName = reader.GetString(1);
                        skater.FamilyName = reader.GetString(2);
                        skater.Phone = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        skater.Email = reader.GetString(4);
                        skater.TeamID = reader.GetString(5);
                        skater.Active = reader.GetBoolean(7);

                        //skaterVM.Active = reader.GetBoolean(5);
                    }
                }
                else
                {
                    throw new ArgumentException("Skater not found (3)");
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
            return skater;
        }


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
                        skaterVM.TeamID = reader.GetString(5);
                        skaterVM.Active = reader.GetBoolean(7);

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

        public int addSkater(SkaterVM _Skater)
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

            cmd.Parameters.Add("@GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@passwordHash", SqlDbType.NVarChar, 100);


            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = _Skater.SkaterID;

            cmd.Parameters["@GivenName"].Value = _Skater.GivenName;
            cmd.Parameters["@FamilyName"].Value = _Skater.FamilyName;
            cmd.Parameters["@email"].Value = _Skater.Email;
            cmd.Parameters["@passwordHash"].Value = _Skater.passwordhash;

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

        public List<SkaterVM> selectAllSkater()
        {
            List<SkaterVM> output = new List<SkaterVM>();
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
                        var _Skater = new SkaterVM();
                        _Skater.SkaterID = reader.GetString(0);
                        _Skater.TeamID = reader.GetString(1);
                        _Skater.GivenName = reader.GetString(2);
                        _Skater.FamilyName = reader.GetString(3);
                        _Skater.Phone = reader.IsDBNull(4) ? "Not Provided" : reader.GetString(4);
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
        public int updateSkater(Skater _oldSkater, Skater _newSkater)
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
            cmd.Parameters.Add("@oldPhone", SqlDbType.NVarChar, 13);
            cmd.Parameters.Add("@newPhone", SqlDbType.NVarChar, 13);
            cmd.Parameters.Add("@oldemail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@newemail", SqlDbType.NVarChar, 250);





            //We need to set the parameter values
            cmd.Parameters["@oldSkaterID"].Value = _oldSkater.SkaterID;

            cmd.Parameters["@oldTeamID"].Value = _oldSkater.TeamID;
            cmd.Parameters["@newTeamID"].Value = _newSkater.TeamID;
            cmd.Parameters["@oldGivenName"].Value = _oldSkater.GivenName;
            cmd.Parameters["@newGivenName"].Value = _newSkater.GivenName;
            cmd.Parameters["@oldFamilyName"].Value = _oldSkater.FamilyName;
            cmd.Parameters["@newFamilyName"].Value = _newSkater.FamilyName;
            cmd.Parameters["@oldPhone"].Value = _oldSkater.Phone;
            cmd.Parameters["@newPhone"].Value = _newSkater.Phone;
            cmd.Parameters["@oldemail"].Value = _oldSkater.Email;
            cmd.Parameters["@newemail"].Value = _newSkater.Email;


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
        public int deleteSkater(Skater _skater)
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
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            //set parameter!!!
            cmd.Parameters["@SkaterID"].Value = _skater.SkaterID;
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


        public int undeleteSkater(Skater _Skater)
        {

            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_undelete_Skater";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            //set parameter!!!
            cmd.Parameters["@SkaterID"].Value = _Skater.SkaterID;
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
        public List<String> selectAllApplicationStatus()
        {
            List<String> output = new List<String>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_ApplicationStatus";
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
                    while (reader.Read())
                    {
                        var _ApplicationStatus = "";
                        _ApplicationStatus = reader.GetString(0);
                        output.Add(_ApplicationStatus);
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

        public List<string> RetreiveSkaterRoles()
        {
            List<String> output = new List<String>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Role";
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
                    while (reader.Read())
                    {
                        var role = "";
                        role = reader.GetString(0);
                        output.Add(role);
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
    }
}

