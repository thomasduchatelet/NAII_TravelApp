using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using TravelApp.UwpApp.Models;

namespace TravelApp.ViewModels
{
    public class TripDetailsViewModel : BindableBase
    {
        public TripDto Trip { get; set; }
        
        public void GetTrip(long id)
        {
            Trip = ApiMethods.GetTrips(id: id).Result.FirstOrDefault();
        }
    }
}
