using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ISkaterAccessor
    {

        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        SkaterVM SelectSkaterVMByDerbyName(string derbyName);
        List<string> SelectRolesByDerbyName(string derbyName);
        int addSkater(string SkaterID, string TeamID, string GivenName, string FamilyName, string Phone, string email);
        int UpdatePasswordHash(string derbyName, string oldPasswordHash, string newPasswordHash);
    }
}
