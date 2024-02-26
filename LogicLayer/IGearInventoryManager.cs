using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IGearInventoryManager
    {

        List<GearInventory> getAllGearInventory();
        int editGearInventory(GearInventory _oldGearInventory, GearInventory _newGearInventory);
    }
}
