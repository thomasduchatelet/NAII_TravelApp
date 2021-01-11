using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class ItemDto : ToDoItemBaseDto
    {
        private string _name;
        private int _count;
        private int _packedCount;
        public int Count { get { return _count; } set { _count = value; NotifyPropertyChanged(); } }
        public int PackedCount { get { return _packedCount; } set { _packedCount = value; NotifyPropertyChanged(); } }
     
    }
}
