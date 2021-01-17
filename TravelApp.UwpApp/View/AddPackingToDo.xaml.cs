using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.Models;
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
    public sealed partial class AddPackingToDo : Page
    {
        private long currentTripId;
        private CategoryDto selectedCategory;
        public AddPackingItemViewModel ViewModel = new AddPackingItemViewModel();
        public AddPackingToDo()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentTripId = (long)e.Parameter;
            ViewModel.GetAllCategories();
            base.OnNavigatedTo(e);
        }

        private async void AddToDo_Click(object sender, RoutedEventArgs e)
        {
            await ApiMethods.AddToDo(currentTripId, txtToDoName.Text, selectedCategory.Id);
            Frame.GoBack();
        }

        private void TextBox_OnBeforeTextChanging(TextBox sender,
                                          TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory = (CategoryDto)e.AddedItems.First();
        }
    }
}
