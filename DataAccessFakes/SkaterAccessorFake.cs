using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;


namespace DataAccessFakes
{
    public class SkaterAccessorFake : ISkaterAccessor
    {
        // create a few fake skaters for testing
        private List<SkaterVM> fakeSkaters = new List<SkaterVM>();
        private List<string> passwordHashes = new List<string>();
        private List<Skater> deletedSkaters = new List<Skater>();

        public SkaterAccessorFake()
        {
            fakeSkaters.Add(new SkaterVM()
            {
                SkaterID = "Hit",
                GivenName = "Tess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "tess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeSkaters.Add(new SkaterVM()
            {
                SkaterID = "Em",
                GivenName = "Bess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "bess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeSkaters.Add(new SkaterVM()
            {
                SkaterID = "High",
                GivenName = "Jess",
                FamilyName = "Data",
                Phone = "1234567890",
                Email = "jess@company.com",
                Active = true,
                Roles = new List<string>()
            });

            passwordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            passwordHashes.Add("badhash");
            passwordHashes.Add("badhash");

            fakeSkaters[0].Roles.Add("TestRole1");
            fakeSkaters[0].Roles.Add("TestRole2");
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;
            for (int i = 0; i < fakeSkaters.Count; i++)
            {

                if (passwordHashes[i] == passwordHash && fakeSkaters[i].Email == email)
                {
                    numAuthenticated++;

                }
            }

            return numAuthenticated;   //should be 1 or 0
        }

        public SkaterVM SelectSkaterVMByDerbyName(string derbyName)
        {
            SkaterVM skt = null;
            foreach (SkaterVM fakeSkater in fakeSkaters)
            {
                if (fakeSkater.SkaterID == derbyName)
                {
                    skt = fakeSkater;
                }

            }
            if (skt == null)
            {
                throw new ApplicationException("Skater not found(1)");
            }


            return skt;
        }

        public List<string> SelectRolesByDerbyName(string derbyName)
        {
            {
                List<string> roles = new List<string>();
                foreach (var _skt in fakeSkaters)
                {
                    if (_skt.SkaterID == derbyName)
                    {
                        roles = _skt.Roles;
                    }
                }


                return roles;
            }
        }



        public int UpdatePasswordHash(string derbyName, string oldPasswordHash, string newPasswordHash)
        {
            int rows = 0;

            for (int i = 0; i < fakeSkaters.Count; i++)
            {
                if (fakeSkaters[i].SkaterID == derbyName)
                {

                    if (passwordHashes[i] == oldPasswordHash)
                    {
                        passwordHashes[i] = newPasswordHash;
                        rows++;


                    }
                }

            }
            if (rows != 1)
            {
                throw new ApplicationException("Bad Email or password (6). Update Failed");
            }


            return rows;
        }

        public int addSkater(SkaterVM _Skater)
        {
            int starting = fakeSkaters.Count;
            fakeSkaters.Add(_Skater);
            int ending = fakeSkaters.Count;
            return ending - starting;
        }

        public List<SkaterVM> selectAllSkater()
        {
            return fakeSkaters;
        }

        public int updateSkater(Skater _oldSkater, Skater _newSkater)
        {
            int result = 0;
            foreach (Skater _skt in fakeSkaters)
            {
                if (_skt.SkaterID == _oldSkater.SkaterID)
                {
                    _skt.SkaterID = _newSkater.SkaterID;
                    _skt.TeamID = _newSkater.TeamID;
                    _skt.FamilyName = _newSkater.FamilyName;
                    _skt.GivenName = _newSkater.GivenName;
                    _skt.Phone = _newSkater.Phone;
                    _skt.Email = _newSkater.Email;
                    _skt.Active = _newSkater.Active;
                    result++;

                }

            }
            return result;
        }

        public int deleteSkater(Skater _Skater)
        {
            int result = 0;

            for (int i = 0; i < fakeSkaters.Count; i++)
            {
                if (fakeSkaters[i].SkaterID == _Skater.SkaterID)
                {
                    fakeSkaters.RemoveAt(i);
                    deletedSkaters.Add(_Skater);
                    result = 1;
                    break;

                }
            }
            if (result == 0) { throw new ArgumentException("skater not found"); }

            return result;
        }

        public int undeleteSkater(Skater _Skater)
        {
            int result = 0;
            for (int i = 0; i < fakeSkaters.Count; i++)
            {
                if (deletedSkaters[i].SkaterID == _Skater.SkaterID)
                {
                    deletedSkaters.RemoveAt(i);
                    //fakeSkaters.Add((SkaterVM)_Skater);
                    result = 1;
                    break;

                }
            }
            if (result == 0) { throw new ArgumentException("skater not found"); }

            return result;
        }

        public List<string> selectAllApplicationStatus()
        {
            throw new NotImplementedException();
        }
    }
}
