using LiveCharts;
using LiveCharts.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Shared;
using TravelApp.Shared.Dto;
using TravelApp.Shared.Dto.FilterDto;
using TravelApp.UwpApp.Models;

namespace TravelApp.ViewModels
{
    public class TripDetailsViewModel : BindableBase
    {
        public AxesCollection XCollection { get { return _xCollection; } set { _xCollection = value; OnPropertyChanged(); } }
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection { get { return _seriesCollection; } set { _seriesCollection = value; OnPropertyChanged(); }}
        private AxesCollection _xCollection = new AxesCollection() { new Axis() { ShowLabels = false }};
        public TripDto Trip { get; set; }
        private ObservableCollection<CountryDto> _countries;
        public ObservableCollection<CountryDto> Countries { get { return _countries; } set { _countries = value; OnPropertyChanged(); } }
        public ObservableCollection<CountryCovidResult> CountryCovidResults { get; set; }

        public void GetTrip(long id)
        {
            Trip = ApiMethods.GetTrips(new TripFilterDto() { Id = id }).Result.FirstOrDefault();
        }

        public async void GetCountries()
        {
            var countries = await ApiMethods.GetAllCountries();
            Countries = new ObservableCollection<CountryDto>(countries.OrderBy(c => c.Country));
        }

        public async void GetCountryCovidData(CountryDto countryDto)
        {
            var results = await ApiMethods.GetCountryCovidData(countryDto);
            CountryCovidResults = new ObservableCollection<CountryCovidResult>(results);
            InitializeChart();
        }

        public void InitializeChart()
        {
           if(CountryCovidResults != null)
            {

           var confirmed = new List<double>();
           var deaths = new List<double>();
           var deathsTotal = new List<double>();
            var dates = new List<string>();
            var previousresult = new CountryCovidResult() { Cases = 0 , Recovered = 0, Deaths = 0, Confirmed = 0};
            foreach (var result in CountryCovidResults)
            {
                dates.Add(result.Date.ToString("dd/MM/yy"));
                confirmed.Add(result.Confirmed - previousresult.Confirmed);
                deaths.Add(result.Deaths - previousresult.Deaths);
                previousresult = result;
            }

            deathsTotal = CountryCovidResults.Select(c => (double) c.Deaths).ToList();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(confirmed),
                    Title = "Cases"
                },
                new LineSeries
                {
                    Values = new ChartValues<double>(deaths),
                    Title = "Deaths"

                },
                //new LineSeries
                //{
                //    Values = new ChartValues<double>(recovered),
                //    Title = "Recovered"

                //}
                new LineSeries
                {
                    Values = new ChartValues<double>(deathsTotal),
                    Title = "Total Deaths"

                }
               // new LineSeries
               // {
               //     Values = new ChartValues<double>(recoveredTotal),
               //     Title = "Total Recovered"

               // }
            };
            XCollection[0].ShowLabels = true;
            XCollection[0].Title = "Dates";
            XCollection[0].Labels = dates;
            XCollection[0].LabelsRotation = 40;
            XCollection[0].Separator = new Separator // force the separator step to 1, so it always display all labels
            {
                Step = dates.Count / 27,
                IsEnabled = false //disable it to make it invisible.
            };

            }

        }



    }
}
