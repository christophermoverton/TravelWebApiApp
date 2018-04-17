using System;
using System.Collections.Generic;

namespace TravelAppCore.Models
{
    public partial class Trips
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? TripId { get; set; }
        public int? WaypointId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
    }
}
