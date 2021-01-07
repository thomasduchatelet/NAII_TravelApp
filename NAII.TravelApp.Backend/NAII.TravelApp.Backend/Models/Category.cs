using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class Category : TravelAppClass
    {
        public string Name { get; set; }


        public Category(string name)
        {
            Name = name;
        }

        protected Category()
        {

        }
    }
}
