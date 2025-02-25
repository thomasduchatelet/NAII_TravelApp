﻿using System;
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
        private ObservableCollection<TripDto> _trips;
        public ObservableCollection<TripDto> Trips { get { return _trips; }  set { _trips = value; OnPropertyChanged(); } }
        public TripsOverviewViewModel()
        {
        }
        public async void GetTrips()
        {
            var trips = await ApiMethods.GetTrips();
            trips.ForEach(t => { if ("".Equals(t.Country)) t.Country = "Travel"; });
            Trips = new ObservableCollection<TripDto>(trips);
        }

        public async void DeleteTrip(TripDto trip)
        {
            Trips.Remove(trip);
            await ApiMethods.DeleteTrip(trip);

        }
    }
}
