using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.Models;
using TravelApp.UwpApp.View;
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
    public sealed partial class AddTrip : Page
    {
        public AddTripViewModel ViewModel = new AddTripViewModel();
        public AddTrip()
        {          
           this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.GetCountries();
        }

        private async void AddTrip_Click(object sender, RoutedEventArgs e)
        {
            var country = (CountryDto) cboCountry.SelectedItem;
            await ApiMethods.AddTrip(txtTripTitel.Text,dpStartDate.SelectedDate.Value.DateTime, dpEndDate.SelectedDate.Value.DateTime, new ItineraryDto() { Name = txtTripTitel.Text + " Route", Locations = new List<LocationDto>() { new LocationDto() { Name = txtCity.Text, Country = country.Country} } });
            Frame.Navigate(typeof(TripsOverview));

        }
    }
}
