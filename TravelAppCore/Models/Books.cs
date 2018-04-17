using System;
using System.Collections.Generic;

namespace TravelAppCore.Models
{
    public partial class Books
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
