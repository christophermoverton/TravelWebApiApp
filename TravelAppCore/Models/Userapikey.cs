using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAppCore.Models
{
    public class Userapikey
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string ApiKeyId { get; set; }
        [Key]
        public string Email { get; set; }
        public bool Verified { get; set; }
        public DateTime Lastused { get; set; }
        public string GeneratekeyId { get; set; }
    }
}
