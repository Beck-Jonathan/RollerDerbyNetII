using DataObjects;

namespace DataAccessInterfaces
{
    public interface IGearRequestAccessor
    {
        int addGearRequest(GearRequest _GearRequest);
        GearRequest selectGearRequestByPrimaryKey(int GearRequestID);

    }
}
