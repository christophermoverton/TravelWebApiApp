using System;
using System.Collections.Generic;

namespace TravelAppCore.Models
{
    public partial class Trips
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int? TripID { get; set; }
        public string TripName { get; set; }
        public int? WaypointID { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
    }
}
