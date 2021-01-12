using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class LocationDto : BaseDto
    {
        private string _name;
        private double _latitude;
        private double _longitude;
        private long _itineraryId;
        public double Order { get; set; }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }
        public double Latitude { get { return _latitude; } set { _latitude = value; NotifyPropertyChanged(); } }
        public double Longitude { get { return _longitude; } set { _longitude = value; NotifyPropertyChanged(); } }
        public long ItineraryId { get { return _itineraryId; } set { _itineraryId = value; NotifyPropertyChanged(); } }

        public string Country { get; set; }
    }
}
