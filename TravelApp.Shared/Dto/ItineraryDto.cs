using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class ItineraryDto : BaseDto
    {
        private string _name;
        private List<LocationDto> _locations;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }
        public List<LocationDto> Locations { get { return _locations; } set { _locations = value; NotifyPropertyChanged(); } }
    }

    public class CreateItineraryDto : ItineraryDto
    {
        public long TripId { get; set; }
    }
}
