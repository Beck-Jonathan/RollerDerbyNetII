using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IGearApplicationManager
    {
        int addGearApplication(GearApplication _GearApplication);
        GearApplication getGearApplicationByPrimaryKey(int GearApplicationID);
        List<GearApplication> getAllGearApplication();
        int editGearApplication(GearApplication _oldGearApplication, GearApplication _newGearApplication);

    }
}
