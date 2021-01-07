using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class Todo
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public bool Completed { get; set; }
        public Category Category { get; set; }
        public long TripId { get; set; }
        public long CategoryId { get; set; }

        protected Todo()
        {

        }
        public Todo(string name, Category category)
        {
            Name = name;
            Completed = false;
            Category = category;
        }
    }
}
