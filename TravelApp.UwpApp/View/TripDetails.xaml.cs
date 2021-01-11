﻿using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Uwp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.Shared.Dto;
using TravelApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TripDetails : Page
    {
        
        public TripDetailsViewModel ViewModel = new TripDetailsViewModel();
        private CountryDto _selectedCountry;

        public TripDetails()
        {
            InitializeComponent();
            ViewModel.GetCountries();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var stackpanel = (StackPanel)e.Parameter;
            TripDto dto = (TripDto)stackpanel.DataContext;
            ViewModel.Trip = dto;
            base.OnNavigatedTo(e); 
        }

        private void CboCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCountry = (CountryDto)e.AddedItems[0];
            ViewModel.GetCountryCovidData(_selectedCountry);
        }
    }
    
}
