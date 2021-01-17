using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.UwpApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PackingList : Page
    {
        private long currentTripId;
        public PackingListViewModel ViewModel = new PackingListViewModel();

        public PackingList()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TripDto trip = (TripDto)e.Parameter;
            currentTripId = trip.Id;
            ViewModel.GetItems(currentTripId);
            base.OnNavigatedTo(e);
        }

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.FilterCategory(e.AddedItems);
        }

    private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPackingItem), currentTripId);
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var item = (ItemDto)btn.DataContext;
            ViewModel.DeleteItem(item);
        }

        private void IncreaseCount_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var item = (ItemDto)btn.DataContext;
            ViewModel.UpdateItem(item, true);
        }

        private void DecreaseCount_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var item = (ItemDto)btn.DataContext;
            ViewModel.UpdateItem(item, false);
            

        }
    }
}
