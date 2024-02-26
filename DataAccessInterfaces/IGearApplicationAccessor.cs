using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IGearApplicationAccessor
    {
        int addGearApplication(GearApplication _GearApplication);
        GearApplication selectGearApplicationByPrimaryKey(int GearApplicationID);
        List<GearApplication> selectAllGearApplication();
        int updateGearApplication(GearApplication _oldGearApplication, GearApplication _newGearApplication);

    }
}
