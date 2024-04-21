using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IGearInventoryManager
    {

        List<GearInventory> getAllGearInventory();
        int editGearInventory(GearInventory _oldGearInventory, GearInventory _newGearInventory);
    }
}
