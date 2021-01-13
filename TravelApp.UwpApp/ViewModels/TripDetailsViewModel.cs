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
using Windows.UI.Xaml;

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

        private string _chartTitle;
        public string ChartTitle { get { return _chartTitle; } set { _chartTitle = value; OnPropertyChanged(); } }

        private string _dateLabel;
        public string DateLabel { get { return _dateLabel; } set { _dateLabel = value; OnPropertyChanged(); } }

        private List<double> confirmed = new List<double>();
          private List<double> deaths = new List<double>();
          private List<double> deathsTotal = new List<double>();
          private List<string>  dates = new List<string>();
        public void GetTrip(long id)
        {
            Trip = ApiMethods.GetTrips(new TripFilterDto() { Id = id }).Result.FirstOrDefault();
        }

        public async void GetCountries()
        {
            var countries = await ApiMethods.GetAllCountries();
            Countries = new ObservableCollection<CountryDto>(countries.OrderBy(c => c.Country));
            GetCountryCovidData(countries.FirstOrDefault(c => c.Country == Trip.Country));
        }

        public async void GetCountryCovidData(CountryDto countryDto)
        {
            ChartTitle = "Current Covid-19 situation in " + Trip.Country;
            DateLabel = Trip.StartDate.ToString("M") + " " + Trip.StartDate.ToString("yyyy") + " - " + Trip.EndDate.ToString("M") + " "  + Trip.EndDate.ToString("yyyy");
            var results = await ApiMethods.GetCountryCovidData(countryDto);
            CountryCovidResults = new ObservableCollection<CountryCovidResult>(results);
            InitializeChart();
        }

        public void InitializeChart()
        {
           if(CountryCovidResults != null)
            {

           confirmed = new List<double>();
           deaths = new List<double>();
           deathsTotal = new List<double>();
            dates = new List<string>();
            var previousresult = new CountryCovidResult() { Cases = 0 , Recovered = 0, Deaths = 0, Confirmed = 0};
            foreach (var result in CountryCovidResults)
            {
                dates.Add(result.Date.ToString("dd/MM/yy"));
                confirmed.Add(result.Confirmed - previousresult.Confirmed);
                deaths.Add(result.Deaths - previousresult.Deaths);
                previousresult = result;
            }

            deathsTotal = CountryCovidResults.Select(c => (double) c.Deaths).ToList();
                RefreshChart();
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

        private void RefreshChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>(confirmed),
                    Title = "Cases",
                    PointGeometry = null,
                    Visibility = CasesSeriesVisibility ? Visibility.Visible : Visibility.Collapsed

                },
                new LineSeries
                {
                    Values = new ChartValues<double>(deaths),
                    Title = "Deaths",
                    PointGeometry = null,
                    Visibility = DeathsSeriesVisibility ? Visibility.Visible : Visibility.Collapsed


                },
                //new LineSeries
                //{
                //    Values = new ChartValues<double>(recovered),
                //    Title = "Recovered"

                //}
                new LineSeries
                {
                    Values = new ChartValues<double>(deathsTotal),
                    Title = "Total Deaths",
                    PointGeometry = null,
                    Visibility = TotalDeathsSeriesVisibility ? Visibility.Visible : Visibility.Collapsed


                }
            };
        }

        private bool _casesSeriesVisibility = true;
        private bool _deathsSeriesVisibility = true;
        private bool _totalDeathsSeriesVisibility = true;
        public bool CasesSeriesVisibility
        {
            get { return _casesSeriesVisibility; }
            set
            {
                _casesSeriesVisibility = value;
                OnPropertyChanged();
                RefreshChart();

            }
        }

        public bool DeathsSeriesVisibility
        {
            get { return _deathsSeriesVisibility; }
            set
            {
                _deathsSeriesVisibility = value;
                OnPropertyChanged();
                RefreshChart();

            }
        }

        public bool TotalDeathsSeriesVisibility
        {
            get { return _totalDeathsSeriesVisibility; }
            set
            {
                _totalDeathsSeriesVisibility = value;
                OnPropertyChanged();
                RefreshChart();
            }
        }


    }
}
