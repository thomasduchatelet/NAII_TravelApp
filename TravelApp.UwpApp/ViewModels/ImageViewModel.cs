using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TravelApp.UwpApp.Models;
using Windows.UI.Xaml.Media;

namespace TravelApp.ViewModels
{
    public class ImageViewModel : BindableBase
    {
        private string _backgroundImg = "https://images.unsplash.com/photo-1479232284091-c8829ec114ad?ixlib=rb-1.2.1&ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&auto=format&fit=crop&w=1351&q=80";
        public string BackgroundImg
        {
            get { return _backgroundImg; }
            set { _backgroundImg = value; OnPropertyChanged(); }
        }
        public async void GetBackground()
        {
            var result = await ApiMethods.GetImageUrl("travel");
            BackgroundImg = result.results[0].urls.full;
        }

        public async Task<String> ImageFrom(string tag)
        {
            var result = await ApiMethods.GetImageUrl(tag);
            return result.results[0].urls.full;
        }
    }
}
