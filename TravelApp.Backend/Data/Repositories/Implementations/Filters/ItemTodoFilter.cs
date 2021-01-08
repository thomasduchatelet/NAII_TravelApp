using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public abstract class ItemTodoFilter<T> : FilterBase<T> where T : TodoItemBase
    {
        public List<string> Categories { get; set; }
        public string Name { get; set; }
        public long? TripId { get; set; }
        public bool? Completed { get; set; }

        public ItemTodoFilter()
        {
            Categories = new List<string>();
        }

        public override IEnumerable<T> FilterAfterUsersFiltered(IEnumerable<T> input)
        {
            if (TripId.HasValue)
                input = input.Where(i => i.TripId == TripId.Value);
            if (Categories.Count > 0)
                input = input.Where(i => Categories.Contains(i.Category.Name));
            if (Name != null && Name != "")
                input = input.Where(i => i.Name.Contains(Name));
            if (Completed.HasValue)
                input = CheckCompleted(input, Completed.Value);
            return input;
        }

        protected abstract IEnumerable<T> CheckCompleted(IEnumerable<T> input, bool completed);
    }

    public class ItemFilter : ItemTodoFilter<Item>
    {
        protected override IEnumerable<Item> CheckCompleted(IEnumerable<Item> input, bool completed)
        {
            return input.Where(i => i.IsCompleted() == completed);
        }
    }

    public class TodoFilter : ItemTodoFilter<Todo>
    {
        protected override IEnumerable<Todo> CheckCompleted(IEnumerable<Todo> input, bool completed)
        {
            return input.Where(i => i.Completed == completed);
        }
    }
}
