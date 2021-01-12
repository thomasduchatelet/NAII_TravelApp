using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
using TravelApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddItinerary : Page
    {
        public ItineraryViewModel ViewModel = new ItineraryViewModel();
        public CovidOverviewViewModel CountryViewModel = new CovidOverviewViewModel();
       
        public AddItinerary()
        {
            this.InitializeComponent();
            CountryViewModel.GetCountries();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var dto = (ItineraryDto)e.Parameter;
            ViewModel.Itinerary = dto;
            if (dto.Locations != null)
                ViewModel.Locations = new ObservableCollection<LocationDto>(dto.Locations);
            else
                ViewModel.Locations = new ObservableCollection<LocationDto>(new List<LocationDto>());
            base.OnNavigatedTo(e);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateOrUpdateItinerary();
            Frame.Navigate(typeof(Itinerary), new TripDto() { Id = ViewModel.Itinerary.TripId });
        }

        private void AddLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            if(cboCountry.SelectedItem != null && txtLocationName.Text != null)
            {
                var country = (CountryDto)cboCountry.SelectedItem;
                var location = new LocationDto() { Country = country.Country, Name = txtLocationName.Text, Order = (ViewModel.Locations.Count + 1) * 10  };
                ViewModel.AddLocation(location);

            }
        }
    }
}
