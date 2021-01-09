using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class ToDoItemBaseDto : BaseDto
    {
        private CategoryDto _category;
        private string _name;
        private bool _completed;
        private long _tripId;
        public CategoryDto Category { get { return _category; } set { _category = value; NotifyPropertyChanged(); } }
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }
        public bool Completed { get { return _completed; } set { _completed = value; NotifyPropertyChanged(); } }
        public long TripId { get { return _tripId; } set { _tripId = value; NotifyPropertyChanged(); } }

    }
}
