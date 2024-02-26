using DataObjects;

namespace LogicLayer
{
    public interface IGearRequestManager
    {
        int addGearRequest(GearRequest _GearRequest);
        GearRequest getGearRequestByPrimaryKey(int GearRequestID);

    }
}
