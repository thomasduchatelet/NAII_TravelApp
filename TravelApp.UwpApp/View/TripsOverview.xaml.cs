using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.UwpApp.ViewModels;
using TravelApp.View;
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

namespace TravelApp.UwpApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TripsOverview : Page
    {
        public TripsOverviewViewModel TripsOverviewViewModel = new TripsOverviewViewModel();
        public TripsOverview()
        {
            TripsOverviewViewModel.GetTrips();
            this.InitializeComponent();
        }
        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Navigation), sender);
            
        }
    }
}
