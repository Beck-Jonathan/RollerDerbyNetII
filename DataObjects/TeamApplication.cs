using System;

namespace DataObjects
{
    public class TeamApplication
    {
        public int TeamApplicationID { set; get; }
        public string SkaterID { set; get; }
        public string TeamName { set; get; }
        public DateTime ApplicationTime { set; get; }
        public string ApplicationStatus { set; get; }

    }
    public class TeamApplicationVM : TeamApplication
    {
        public Skater _skater { get; set; }
        public Team _team { get; set; }
    }
}
