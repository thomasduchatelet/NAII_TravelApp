using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class ItemDto : ToDoItemBaseDto
    {
        public int Count { get; set; }
        public int PackedCount { get; set; }
    }
}
