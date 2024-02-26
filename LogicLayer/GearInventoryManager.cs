using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class GearInventoryManager : IGearInventoryManager
    {

        private IGearInventoryAccessor _gearAccessor = null;

        public GearInventoryManager()
        {
            _gearAccessor = new GearInventoryAccessor(); //use the database
        }

        public GearInventoryManager(IGearInventoryAccessor gearAccessor)
        {
            _gearAccessor = gearAccessor;
        }
        public int editGearInventory(GearInventory _oldGearInventory, GearInventory _newGearInventory)
        {
            int result = 0;
            try
            {
                result=_gearAccessor.updateGearInventory(_oldGearInventory, _newGearInventory);
                if (result == 0) { throw new Exception(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public List<GearInventory> getAllGearInventory()
        {
            List<GearInventory> _gearInventoryList = null;
            try
            {
                _gearInventoryList = _gearAccessor.selectAllGearInventory();
                if (_gearInventoryList == null) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return _gearInventoryList;

        }
    }
}
