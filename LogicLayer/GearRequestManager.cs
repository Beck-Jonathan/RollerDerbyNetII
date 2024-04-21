using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;

namespace LogicLayer
{
    public class GearRequestManager : IGearRequestManager

    {
        private IGearRequestAccessor _gearRequestAccessor = null;

        //defailt constructor will use database
        public GearRequestManager()
        {
            _gearRequestAccessor = new GearRequestAccessor();
        }

        //the optional contructor can accept any data providero
        public GearRequestManager(IGearRequestAccessor gearRequestAccessor)
        {
            _gearRequestAccessor = gearRequestAccessor;

        }
        //this function will add a gear request
        public int addGearRequest(GearRequest _GearRequest)
        {
            int result = 0;
            try
            {
                result = _gearRequestAccessor.addGearRequest(_GearRequest);
                if (result == 0) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        //this function will get a gear request by it's PK
        public GearRequest getGearRequestByPrimaryKey(int GearRequestID)
        {
            GearRequest gr = null;
            try
            {
                gr = _gearRequestAccessor.selectGearRequestByPrimaryKey(GearRequestID);
                if (gr == null) { throw new ApplicationException(); }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return gr;
        }
    }
}
