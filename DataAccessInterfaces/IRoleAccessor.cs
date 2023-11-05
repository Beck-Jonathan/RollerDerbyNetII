using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IRoleAccessor
    {
        int insertRole(string RoleID);
        Role selectRoleByPrimaryKey(string RoleID);
        List<Role> selectAllRole();
        int updateRole(string oldRoleID, string newRoleID);
        int deleteRole(string RoleID);
    }
}
