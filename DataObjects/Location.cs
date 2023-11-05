namespace DataObjects
{
    public class Location
    {
        public string LocationId { set; get; }
        public string LeagueID { set; get; }
        public string ContactPhone { set; get; }
        public string City { set; get; }
        public string State { set; get; }

        public string ZipCode { set; get; }
    }
    public class LocationVM : Location
    {
        public League league { set; get; }
    }
}
