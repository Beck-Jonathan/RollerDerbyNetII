using System;

namespace DataObjects
{
    public class GearApplication
    {
        public int ApplicationID { set; get; }
        public string SkaterID { set; get; }
        public int GearReuestID { set; get; }
        public DateTime ApplicationTime { set; get; }
        public string ApplicationStatus { set; get; }

    }
    public class GearApplicationVM : GearApplication
    {
        public Skater _skater { get; set; }
        public GearRequest _gearrequest { get; set; }
       
    }
}
