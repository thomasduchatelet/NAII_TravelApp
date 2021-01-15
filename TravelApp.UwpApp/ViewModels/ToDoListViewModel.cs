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
        public class ToDoListViewModel : BindableBase
        {
            private int _total_todos = 0;
            private int _total_finished = 0;
            public int total_todos { get { return _total_todos; } set { _total_todos = value; OnPropertyChanged(); } }
            public int total_finished { get { return _total_finished; } set { _total_finished = value; OnPropertyChanged(); } }
            private bool _completedFilter = false;
            public bool CompletedFilter { get { return _completedFilter; } set { _completedFilter = value; FilterCategory(new List<object>() { _currentCategory }); } }
            private List<TodoDto> _allTodos;
            private ObservableCollection<TodoDto> _todos;
            private CategoryDto _currentCategory;
            public ObservableCollection<TodoDto> Todos
            {
                get { return _todos; }
                set { _todos = value; OnPropertyChanged(); }
            }

            private ObservableCollection<CategoryDto> _categories;
            public ObservableCollection<CategoryDto> Categories { get { return _categories; } set { _categories = value; OnPropertyChanged(); } }



            public async void GetToDos(long tripId)
            {
                _allTodos = await ApiMethods.GetToDosEager(new ItemTodoFilterDto { TripId = tripId });
                Todos = new ObservableCollection<TodoDto>(_allTodos.OrderBy(i => i.CategoryId));
                Categories = new ObservableCollection<CategoryDto>(Todos.Select(i => i.Category).GroupBy(i => i.Name).Select(g => g.First()).ToList());
                _currentCategory = new CategoryDto { Id = -1, Name = "---" };
                Categories.Add(_currentCategory);
                Categories = new ObservableCollection<CategoryDto>(Categories.OrderBy(c => c.Name));
                CompletedFilter = _completedFilter;
                this.total_todos = Todos.Count(i => !i.Completed);
                this.total_finished = Todos.Count(i => i.Completed);
            }

            public async void DeleteToDo(TodoDto todo)
            {
            if (todo.Completed)
            {
                total_finished--;
            }
            total_todos--;
            Todos.Remove(todo);
            
            await ApiMethods.DeleteToDo(todo);
            }

            public void FilterCategory(IList<object> addedItems)
            {

                List<CategoryDto> categories = addedItems.Cast<CategoryDto>().ToList();
                _currentCategory = categories[0];
                var items = new List<TodoDto>();
                if (categories[0].Id == -1)
                    items = _allTodos;
                else
                    items = _allTodos.Where(i => categories[0].Id == i.CategoryId).ToList();
                Todos = new ObservableCollection<TodoDto>(items.Where(i => i.Completed != _completedFilter || i.Completed == false).OrderBy(i => i.CategoryId));
                this.total_todos = Todos.Count(i => !i.Completed);
                this.total_finished = Todos.Count(i => i.Completed);

            }

            public void UpdateToDo(TodoDto todo, bool completed)
            {
            if (completed)
            {
                total_finished++;
            }
            else
            {
                total_finished--;
            }
            var index = Todos.IndexOf(todo);
                var category = Categories.Where(i => i.Id == todo.CategoryId).FirstOrDefault();
                Todos[index] = todo;
                Todos[index].Category = category;
                todo.Completed = completed;
               ApiMethods.UpdateTodo(todo);
                    

                }



            
        }
    
}
