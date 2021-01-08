using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class Item : TodoItemBase
    {
        public int Count { get; set; }
        public int PackedCount { get; private set; }

        public Item(string name, int count, Category category)
        {
            Name = name;
            Count = count;
            PackedCount = 0;
            Category = category;
        }

        protected Item()
        {
            Completed = IsCompleted();
        }

        public void IncreasePackedCount(int amount)
        {
            PackedCount += amount;
            Completed = IsCompleted();
        }
        private bool IsCompleted()
        {
            return PackedCount >= Count;
        }
    }
}
