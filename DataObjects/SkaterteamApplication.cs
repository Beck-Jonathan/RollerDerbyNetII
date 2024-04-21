using System;

namespace DataObjects
{
    public class SkaterTeamApplication
    {
        public int ApplicationID { set; get; }
        public string skaterID { set; get; }
        public string TeamName { set; get; }
        public DateTime ApplicationTime { set; get; }
        public string ApplicationStatus { set; get; }
        public bool isActive { set; get; }
    }
}
