using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
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
    public sealed partial class Navigation : Page
    {
        public long CurrentTripId { get; set; }
        public Navigation()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var stackpanel = (StackPanel)e.Parameter;
            TripDto dto = (TripDto)stackpanel.DataContext;
            CurrentTripId = dto.Id;
            contentFrame.Navigate(typeof(TripDetails), e.Parameter);
        }

        private void NavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {         
            var label = args.InvokedItem as string;
            var pageType =
                label == "Packing List" ? typeof(PackingList) :
                label == "ToDo List" ? typeof(Login) :
                label == "Itinerary" ? typeof(Itinerary) : 
                label == "Categorieën" ? typeof(Login) : null; 
            
            if (pageType != null && pageType != contentFrame.CurrentSourcePageType)
            {
                contentFrame.Navigate(pageType, CurrentTripId);
            }
        }

        private void On_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
