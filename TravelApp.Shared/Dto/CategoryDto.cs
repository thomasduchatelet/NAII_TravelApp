using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class CategoryDto : BaseDto
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }
    }
}
