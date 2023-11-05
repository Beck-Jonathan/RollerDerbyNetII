using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ISkater_Role_LineAccessor
    {
        int insertSkater_Role_Line(string RoleID, string SkaterID);
        Skater_Role_Line selectSkater_Role_LineByPrimaryKey(string RoleID, string SkaterID);
        List<Skater_Role_Line> selectAllSkater_Role_Line();
        int updateSkater_Role_Line(string oldRoleID, string oldSkaterID, string newRoleID, string newSkaterID);
        int deleteSkater_Role_Line(string Skater_Role_LineID);
    }
}
