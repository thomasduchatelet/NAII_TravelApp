using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Models
{
    public abstract class TodoItemBase : TravelAppClass
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public long TripId { get; set; }
        public long CategoryId { get; set; }
    }
}
