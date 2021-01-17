using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared.Dto;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelApp.Models
{
    public class TripImageDto : GetTripDto
    {
        public BitmapImage Image = new BitmapImage();
    }
}
