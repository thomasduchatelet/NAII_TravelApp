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
        public class CategoriesViewModel : BindableBase
        {
            private ObservableCollection<CategoryDto> _categories;
            public ObservableCollection<CategoryDto> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }
            public async void GetAllCategories()
            {
                Categories = new ObservableCollection<CategoryDto>(await ApiMethods.GetCategories());
            }

        public async void DeleteItem(CategoryDto category)
        {
            Categories.Remove(category);
            await ApiMethods.DeleteCategory(category);

        }
    }
}
