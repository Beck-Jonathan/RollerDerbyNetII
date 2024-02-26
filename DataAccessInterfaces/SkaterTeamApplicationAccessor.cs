using System.Collections.Generic;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ISkaterTeamApplicationAccessor
    {
        int addSkaterTeamApplication(int ApplicationID, string SkaterID, string TeamName, string ApplicationStatus);
        SkaterTeamApplication selectSkaterteamApplicationByPrimaryKey(int SkaterteamApplicationID);
        List<SkaterTeamApplication> selectAllSkaterteamApplication();
        int updateSkaterteamApplication(int oldApplicationID, string oldSkaterID, string oldTeamName, string oldApplicationStatus, string newSkaterID, string newTeamName, string newApplicationStatus);
        int deleteSkaterteamApplication(int SkaterteamApplicationID);
        List<SkaterTeamApplication> selectSkaterTeamApplicationBySkaterID(Skater skater);
    }
}
