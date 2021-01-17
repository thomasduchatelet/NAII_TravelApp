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
    public sealed partial class ToDoList : Page
    {
        private long currentTripId;
        public ToDoListViewModel ViewModel = new ToDoListViewModel();

        public ToDoList()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TripDto trip = (TripDto)e.Parameter;
            currentTripId = trip.Id;
            ViewModel.GetToDos(currentTripId);
            base.OnNavigatedTo(e);
        }

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.FilterCategory(e.AddedItems);
        }

        private void cbCompleted_Changed(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            var todo = (TodoDto)cb.DataContext;
            bool completed = cb.IsChecked != null && (bool)cb.IsChecked;
            ViewModel.UpdateToDo(todo, completed);
        }

        private void NewToDo_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPackingToDo), currentTripId);
        }

        private void DeleteToDo_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var todo = (TodoDto)btn.DataContext;
            ViewModel.DeleteToDo(todo);


        }
    }
}
