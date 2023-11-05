using System.Collections.Generic;

namespace DataObjects
{
    public class Skater
    {
        /*
         * storage model for the Skater table 
        */
        public string SkaterID { get; set; }
        public string TeamID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    public class SkaterVM : Skater
    {
        public List<string> Roles { get; set; }
    }
}
