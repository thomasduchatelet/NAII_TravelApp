using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class ToDoItemBaseDto : BaseDto
    {
        public CategoryDto Category { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public long TripId { get; set; }

    }
}
