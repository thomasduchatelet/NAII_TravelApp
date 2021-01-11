using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using TravelApp.Shared.Dto.FilterDto;
using TravelApp.UwpApp.Models;

namespace TravelApp.ViewModels
{
    public class PackingListViewModel : BindableBase
    {
        private List<ItemDto> _allItems;
        private ObservableCollection<ItemDto> _items;
        public ObservableCollection<ItemDto> Items { get { return _items; } 
            set { _items = value; OnPropertyChanged(); } }

        private ObservableCollection<CategoryDto> _categories;
        public ObservableCollection<CategoryDto> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }



        public async void GetItems(long tripId)
        {
            _allItems = await ApiMethods.GetItemsEager(new ItemTodoFilterDto { TripId = tripId });
            Items = new ObservableCollection<ItemDto>(_allItems);
            Categories = new ObservableCollection<CategoryDto>(Items.Select(i => i.Category).GroupBy(i => i.Name).Select(g => g.First()).ToList());
        }


        public void FilterCategory(IList<object> addedItems)
        {
            List<CategoryDto> categories = addedItems.Cast<CategoryDto>().ToList();
            Items = new ObservableCollection<ItemDto>(_allItems.Where(i => categories.Contains(i.Category)));
        }

        public void UpdateItem(ItemDto item)
        {
            var index = Items.IndexOf(item);
            Items[index] = item;
            ApiMethods.UpdateItem(item);

        }
    }
}
