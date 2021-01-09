using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.Models;
using TravelApp.ViewModels;

namespace TravelApp.UwpApp.ViewModels
{
    public class TripsOverviewViewModel : BindableBase
    {
        public ObservableCollection<TripDto> _trips;
        public ObservableCollection<TripDto> Trips
        { get { return this._trips; } set { Set(ref _trips, value); } }
        public TripsOverviewViewModel()
        {
        }
        public async void getTrips()
        {
            Trips = new ObservableCollection<TripDto>(await ApiMethods.GetTrips());
        }
    }
}
