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

        public int addSkater(string SkaterID, string TeamID, string GivenName, string FamilyName, string Phone, string email)
        {
            int x = 0;

            return x;

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
    }
}
