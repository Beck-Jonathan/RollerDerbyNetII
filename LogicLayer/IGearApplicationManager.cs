using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
