using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessFakes
{
    public class GearRequestFakes : IGearRequestAccessor
    {
        List<GearRequest> _gearReqs = new List<GearRequest>();
        public GearRequestFakes()
        {
            GearRequest fakeReq1 = new GearRequest();
            GearRequest fakeReq2 = new GearRequest();
            GearRequest fakeReq3 = new GearRequest();
            fakeReq1.GearRequestID = 100;
            fakeReq1.WristGuardSize = "Large";
            fakeReq1.ElbowPadSize = "Large";
            fakeReq1.KneePadSize = "Large";
            fakeReq1.SkateSize = "7";
            fakeReq1.HelmSize = "Large";
            fakeReq2.GearRequestID = 101;
            fakeReq2.WristGuardSize = "Medium";
            fakeReq2.ElbowPadSize = "Medium";
            fakeReq2.KneePadSize = "Medium";
            fakeReq2.SkateSize = "8";
            fakeReq2.HelmSize = "Medium";
            fakeReq3.GearRequestID = 100;
            fakeReq3.WristGuardSize = "Small";
            fakeReq3.ElbowPadSize = "Small";
            fakeReq3.KneePadSize = "Small";
            fakeReq3.SkateSize = "10";
            fakeReq3.HelmSize = "Small";
            _gearReqs.Add(fakeReq1);
            _gearReqs.Add(fakeReq2);
            _gearReqs.Add(fakeReq3);

        }

        public int addGearRequest(GearRequest _GearRequest)
        {
            int startsize = _gearReqs.Count();
            _gearReqs.Add(_GearRequest);
            int endsize = _gearReqs.Count;
            return endsize - startsize;
        }

        public GearRequest selectGearRequestByPrimaryKey(int GearRequestID)
        {
            GearRequest result = null;
            foreach (GearRequest gearRequest in _gearReqs)
            {
                if (gearRequest.GearRequestID == GearRequestID) { result = gearRequest; break; }
                if (result == null) { throw new ApplicationException(); }

            }


            return result;
        }
    }
}
