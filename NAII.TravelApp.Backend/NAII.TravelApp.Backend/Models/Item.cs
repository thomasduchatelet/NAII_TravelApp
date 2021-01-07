using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class Item
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public int Count { get; set; }
        public int PackedCount { get; set; }
        public long TripId { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
