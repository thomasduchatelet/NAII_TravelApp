using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.Models;

namespace TravelApp.ViewModels
{
    public class AddTripViewModel : BindableBase
    {
        private ObservableCollection<CategoryDto> _categories;
        public ObservableCollection<CategoryDto> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }
        private ObservableCollection<CountryDto> _countries;
        public ObservableCollection<CountryDto> Countries { get { return _countries; } set { _countries = value; OnPropertyChanged(); } }

        public async void GetCountries()
        {
            var countries = await ApiMethods.GetAllCountries();
            Countries = new ObservableCollection<CountryDto>(countries.OrderBy(c => c.Country));
        }

        public async void GetAllCategories()
        {
            Categories = new ObservableCollection<CategoryDto>(await ApiMethods.GetCategories());
        }
    }
}
