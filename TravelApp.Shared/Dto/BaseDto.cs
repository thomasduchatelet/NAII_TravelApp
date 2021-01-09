using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TravelApp.Shared.Dto
{
    public class BaseDto : INotifyPropertyChanged
    {
        private long _id;
        public long Id { get { return _id; } set { _id = value; NotifyPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
