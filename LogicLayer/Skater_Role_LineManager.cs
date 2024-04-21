using DataAccessLayer;
using System;

namespace LogicLayer
{
    public class Skater_Role_LineManager : ISkater_Role_LineManager
    {
        private Skater_Role_LineAccessor _roleAccessor;
        public Skater_Role_LineManager()
        {
            _roleAccessor = new Skater_Role_LineAccessor();
        }
        public Skater_Role_LineManager(Skater_Role_LineAccessor roleAccessor)
        {
            _roleAccessor = roleAccessor;
        }

        public int addSkater_Role_Line(string RoleID, string SkaterID)
        {
            int result = 0;

            try
            {
                result = _roleAccessor.insertSkater_Role_Line(RoleID, SkaterID);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public int deleteSkater_Role_Line(string RoleId, string Skaterid)
        {
            int result = 0;

            try
            {
                result = _roleAccessor.removeSkater_Role_Line(RoleId, Skaterid);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
