using DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class Skater_Role_LineFakes : ISkater_Role_LineAccessor
    {
       Dictionary<string,string>_fakeRoles = new Dictionary<string,string>();
        public Skater_Role_LineFakes()
        {
            _fakeRoles.Add("Alice", "Admin");

        }
        public int insertSkater_Role_Line(string RoleID, string SkaterID)
        {
            int start = _fakeRoles.Keys.Count();
            _fakeRoles.Add(SkaterID, RoleID);
            return _fakeRoles.Keys.Count - start;
        }

        public int removeSkater_Role_Line(string RoleID, string SkaterID)
        {
            int start = _fakeRoles.Keys.Count();
            _fakeRoles.Remove(SkaterID);
            return start - _fakeRoles.Keys.Count;
        }
    }
}
