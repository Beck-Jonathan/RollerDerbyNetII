using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ISkaterAccessor
    {

        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        SkaterVM SelectSkaterVMByDerbyName(string derbyName);
        List<string> SelectRolesByDerbyName(string derbyName);

        int UpdatePasswordHash(string derbyName, string oldPasswordHash, string newPasswordHash);
        int addSkater(SkaterVM _Skater);
        List<SkaterVM> selectAllSkater();
        int updateSkater(Skater _oldSkater, Skater _newSkater);
        int deleteSkater(Skater _Skater);
        int undeleteSkater(Skater _Skater);
        List<String> selectAllApplicationStatus();
        List<String> RetreiveSkaterRoles();
        Skater selectSkaterByEmail(String email);

        List<String> selectDistinctTeamForDropDown();
    }
}
