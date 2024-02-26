using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Team
    {
        public string TeamId { set; get; }
        public string LeagueID { set; get; }

        public decimal MonthlyDue { set; get; }
        public string City { set; get; }
        public string State { set; get; }
        public string Zip { set; get; }

        public bool isActive { set; get; }

    }
    public class TeamVM : Team
    {
        public League league { set; get; }

    }

}
