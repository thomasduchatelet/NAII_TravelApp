using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Models
{
    public class Todo : TodoItemBase
    {

        public bool Completed { get; set; }
        protected Todo()
        {

        }
        public Todo(string name, Category category)
        {
            Name = name;
            Completed = false;
            Category = category;
        }
    }
}
