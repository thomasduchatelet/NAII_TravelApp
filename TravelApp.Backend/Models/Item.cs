using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Models
{
    public class Item : TodoItemBase
    {
        public int Count { get; set; }
        public int PackedCount { get; set; }

        public Item(string name, int count, Category category)
        {
            Name = name;
            Count = count;
            PackedCount = 0;
            Category = category;
        }

        protected Item() {}
        public bool IsCompleted()
        {
            return PackedCount >= Count;
        }
    }
}
