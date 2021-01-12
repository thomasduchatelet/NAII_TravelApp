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
        private bool _completedFilter = false;
        public bool CompletedFilter { get { return _completedFilter;} set { _completedFilter = value; FilterCategory(new List<object>() { _currentCategory }); } }
        private List<ItemDto> _allItems;
        private ObservableCollection<ItemDto> _items;
        private CategoryDto _currentCategory;
        public ObservableCollection<ItemDto> Items { get { return _items; } 
            set { _items = value; OnPropertyChanged(); } }

        private ObservableCollection<CategoryDto> _categories;
        public ObservableCollection<CategoryDto> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }



        public async void GetItems(long tripId)
        {
            _allItems = await ApiMethods.GetItemsEager(new ItemTodoFilterDto { TripId = tripId });
            _allItems.ForEach(i => { if (i.PackedCount >= i.Count) i.Completed = true; });
            Items = new ObservableCollection<ItemDto>(_allItems.OrderBy(i => i.CategoryId));
            Categories = new ObservableCollection<CategoryDto>(Items.Select(i => i.Category).GroupBy(i => i.Name).Select(g => g.First()).ToList());
            _currentCategory = new CategoryDto { Id = -1, Name = "---" };
            Categories.Add(_currentCategory);
            Categories = new ObservableCollection<CategoryDto>(Categories.OrderBy(c => c.Name));
            CompletedFilter = _completedFilter;
        }

        public async void DeleteItem(ItemDto item)
        {
            Items.Remove(item);
            await ApiMethods.DeleteItem(item);

        }


        public void FilterCategory(IList<object> addedItems)
        {
            
            List<CategoryDto> categories = addedItems.Cast<CategoryDto>().ToList();
            _currentCategory = categories[0];
            var items = new List<ItemDto>();
            if (categories[0].Id == -1)
                 items = _allItems;
            else
                items = _allItems.Where(i => categories[0].Id == i.CategoryId).ToList();
            Items = new ObservableCollection<ItemDto>(items.Where(i => i.Completed != _completedFilter || i.Completed == false).OrderBy(i => i.CategoryId));
        }

        public void UpdateItem(ItemDto item)
        {
            var index = Items.IndexOf(item);
            Items[index] = item;
            ApiMethods.UpdateItem(item);

        }
    }
}
