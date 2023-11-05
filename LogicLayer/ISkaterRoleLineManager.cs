using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ISkater_Role_LineManager
    {
        int addSkater_Role_Line(string RoleID, string SkaterID);
        Skater_Role_Line getSkater_Role_LineByPrimaryKey(string Skater_Role_LineID);
        List<Skater_Role_Line> getAllSkater_Role_Line();
        int editSkater_Role_Line(string oldRoleID, string oldSkaterID, string newRoleID, string newSkaterID);
        int purgeSkater_Role_Line(string Skater_Role_LineID);
    }
}