using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IRoleManager
    {
        int addRole(string RoleID);
        Role getRoleByPrimaryKey(string RoleID);
        List<Role> getAllRole();
        int editRole(string oldRoleID, string newRoleID);
        int purgeRole(string RoleID);
    }
}
