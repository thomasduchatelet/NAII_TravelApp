﻿using TravelApp.Backend.Data.Repositories.Interfaces;
using TravelApp.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApp.Backend.Data.Repositories.Implementations.Filters
{
    public class CategoryFilter : IFilter<Category>
    {
        public string Name { get; set; }

        public IEnumerable<Category> Filter(IEnumerable<Category> input, string userId)
        {
            input = input.Where(i => i.UserId == userId);
            if (Name != "" && Name != null)
                input = input.Where(i => i.Name.ToLower().Contains(Name.ToLower()));

            return input;
        }
    }
}
