using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto.FilterDto
{
    public class TripFilterDto : BaseFilterDto
    {
        public string Title { get; set; }
        public DateTime? StartsAfter { get; set; }
        public DateTime? StartsBefore { get; set; }
    }

    



}
