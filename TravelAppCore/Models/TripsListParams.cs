using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAppCore.Models
{
    public class TripsListParams
    {
        public string UserId { get; set; }
        public string UserApiKeyId { get; set; }
        public List<Trips> Triplist { get; set; }
    }
}
