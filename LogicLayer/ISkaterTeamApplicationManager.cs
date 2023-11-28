using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ISkaterteamApplicationManager
    {
        int addSkaterteamApplication(SkaterTeamApplication _SkaterteamApplication);
        SkaterTeamApplication getSkaterteamApplicationByPrimaryKey(int SkaterteamApplicationID);
        List<SkaterTeamApplication> getAllSkaterteamApplication();
        int editSkaterteamApplication(SkaterTeamApplication _oldSkaterteamApplication, SkaterTeamApplication _newSkaterteamApplication);
        int purgeSkaterteamApplication(int SkaterteamApplicationID);
    }
}
