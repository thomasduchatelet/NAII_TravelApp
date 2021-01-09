using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto.FilterDto
{
    public class ItemTodoFilterDto : BaseFilterDto
    {
        public List<string> Categories { get; set; }
        public string Name { get; set; }
        public long? TripId { get; set; }
        public bool? Completed { get; set; }
    }
}
