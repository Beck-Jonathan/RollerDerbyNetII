using DataObjects;
using System.Collections.Generic;

namespace DataAccessInterfaces
{


    public interface IGearInventoryAccessor
    {


        List<GearInventory> selectAllGearInventory();
        int updateGearInventory(GearInventory _oldGearInventory, GearInventory _newGearInventory);

    }

}
