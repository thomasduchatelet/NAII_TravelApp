using System;
using System.Collections.Generic;
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
    public sealed partial class Itinerary : Page
    {
        private long currentTripId;
        public ItineraryViewModel ViewModel = new ItineraryViewModel();
        public Itinerary()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var dto = (TripDto)e.Parameter;
            currentTripId = dto.Id;
            ViewModel.GetItinerary(currentTripId);
            base.OnNavigatedTo(e);
        }


        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var location = (LocationDto) btn.DataContext;
            ViewModel.LocationUp(location);
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var location = (LocationDto)btn.DataContext;
            ViewModel.LocationDown(location);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItinerary), ViewModel.Itinerary);
        }
    }
}
