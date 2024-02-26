using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
   

public interface IGearInventoryAccessor
    {
        
        
        List<GearInventory> selectAllGearInventory();
        int updateGearInventory(GearInventory _oldGearInventory , GearInventory _newGearInventory);
       
    }

}
