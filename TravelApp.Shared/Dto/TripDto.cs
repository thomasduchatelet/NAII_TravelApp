using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class TripDto : BaseDto
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private string _title;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; NotifyPropertyChanged(); } }
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; NotifyPropertyChanged(); } }
        public string Title { get { return _title; } set { _title = value; NotifyPropertyChanged(); } }
        public string Country { get; set; }
    }

    public class GetTripDto : TripDto
    {
        public ItineraryDto Itinerary { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<TodoDto> Todos { get; set; }
    }
}
