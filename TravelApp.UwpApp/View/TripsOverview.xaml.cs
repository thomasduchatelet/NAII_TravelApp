using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.ViewModels;
using TravelApp.View;
using TravelApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.UwpApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TripsOverview : Page
    {
        public TripsOverviewViewModel ViewModel = new TripsOverviewViewModel();
        public ImageViewModel imageViewModel = new ImageViewModel();
        public TripsOverview()
        {
            ViewModel.GetTrips();
            this.InitializeComponent();
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Navigation), sender);
            
        }

        private void NewTrip_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTrip));
        }
        public async void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            var query = img.Tag.ToString();
            if (img != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                img.Width = bitmapImage.DecodePixelWidth = 280;
                bitmapImage.UriSource = new Uri(await imageViewModel.ImageFrom(query));
                img.Source = bitmapImage;
            }
        }

        private void deleteTrip_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var trip = (TripDto)btn.DataContext;
            ViewModel.DeleteTrip(trip);
        }
    }
}
