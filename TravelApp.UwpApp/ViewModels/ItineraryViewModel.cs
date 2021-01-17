using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.Models;

namespace TravelApp.ViewModels
{
    public class ItineraryViewModel : BindableBase
    {
        private ItineraryDto _itinerary;
        public ItineraryDto Itinerary { get { return _itinerary; } set { _itinerary = value; OnPropertyChanged(); }  }

        private ObservableCollection<LocationDto> _locations;
        public ObservableCollection<LocationDto> Locations { get { return _locations; } set { _locations = value; OnPropertyChanged(); } }


        public async void GetItinerary(long tripid)
        {

            Itinerary = await ApiMethods.GetItinerary(tripid);
            Locations = new ObservableCollection<LocationDto>(Itinerary.Locations);
        }

        public async void LocationUp(LocationDto location)
        {
            var locations = await ApiMethods.ChangeLocationPosition(Locations.IndexOf(location) -1, location);
            Locations = new ObservableCollection<LocationDto>(locations.OrderBy(l => l.Order));
        }

        public void CreateOrUpdateItinerary()
        {
            Itinerary.Locations = Locations.ToList();
            if (Itinerary.Id == 0)
                CreateItinerary();
            else
                UpdateItinerary();
        }
        private async void CreateItinerary()
        {
            Itinerary = await ApiMethods.CreateItinerary(Itinerary);
        }

        private async void UpdateItinerary()
        {
            Itinerary = await ApiMethods.UpdateItinerary(Itinerary);
        }

        public async void LocationDown(LocationDto location)
        {
            var locations = await ApiMethods.ChangeLocationPosition(Locations.IndexOf(location) + 2, location);
            Locations = new ObservableCollection<LocationDto>(locations.OrderBy(l => l.Order));
        }

        public void AddLocation(LocationDto location)
        {
            Locations.Add(location);
            OnPropertyChanged("Locations");
        }
    }
}
