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
    public class GearApplicationManager : IGearApplicationManager { 

         private IGearApplicationAccessor _gearAccessor = null;

        public GearApplicationManager()
        {
            _gearAccessor = new GearApplicationAccessor();
    

                }

    public GearApplicationManager(IGearApplicationAccessor gearAccessor)
    {
        _gearAccessor = gearAccessor;
    }
        //this function will add a gear application
        public int addGearApplication(GearApplication _GearApplication)
        {
            int result = 0;
            try
            {
                result = _gearAccessor.addGearApplication(_GearApplication);
                if (result == 0) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
        //this function will edit a gear application
        public int editGearApplication(GearApplication _oldGearApplication, GearApplication _newGearApplication)
        {
            int result = 0;
            try
            {
                result = _gearAccessor.updateGearApplication(_oldGearApplication, _newGearApplication);
                if (result==0) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return result;
        }
        //this function will get all gear applicaitons
        public List<GearApplication> getAllGearApplication()
        {
            List<GearApplication> results = new List<GearApplication>();
            try
            {
                results = _gearAccessor.selectAllGearApplication();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return results;
        }
        //this function will get a gear application by pk
        public GearApplication getGearApplicationByPrimaryKey(int GearApplicationID)
        {
            GearApplication ga = null;
            try
            {
                ga = _gearAccessor.selectGearApplicationByPrimaryKey(GearApplicationID);
                if ( ga==null)
                {
                    throw new ApplicationException();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return ga;
        }
    }
}
