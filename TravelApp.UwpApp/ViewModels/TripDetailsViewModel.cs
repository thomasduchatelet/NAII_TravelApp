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
        private List<ItemDto> items;
        private List<TodoDto> todos;

        private int _total = 0;
        private int _total_finished = 0;
        public int total { get { return _total; } set { _total = value; OnPropertyChanged(); } }
        public int total_finished { get { return _total_finished; } set { _total_finished = value; OnPropertyChanged(); } }

        private int _total_items = 0;
        private int _total_items_packed = 0;
        public int total_items { get { return _total_items; } set { _total_items = value; OnPropertyChanged(); } }
        public int total_items_packed { get { return _total_items_packed; } set { _total_items_packed = value; OnPropertyChanged(); } }

        private int _total_todos = 0;
        private int _total_todos_completed = 0;
        public int total_todos { get { return _total_todos; } set { _total_todos = value; OnPropertyChanged(); } }
        public int total_todos_completed { get { return _total_todos_completed; } set { _total_todos_completed = value; OnPropertyChanged(); } }

        private string _chartTitle;
        public string ChartTitle { get { return _chartTitle; } set { _chartTitle = value; OnPropertyChanged(); } }

        private string _dateLabel;
        public string DateLabel { get { return _dateLabel; } set { _dateLabel = value; OnPropertyChanged(); } }

        private List<double> confirmed = new List<double>();
          private List<double> deaths = new List<double>();
          private List<double> deathsTotal = new List<double>();
          private List<string>  dates = new List<string>();

        public async void GetCountries()
        {
            var countries = await ApiMethods.GetAllCountries();
            Countries = new ObservableCollection<CountryDto>(countries.OrderBy(c => c.Country));
            GetCountryCovidData(countries.FirstOrDefault(c => c.Country == Trip.Country));
        }

        public async void GetPackingData()
        {
            items = await ApiMethods.GetItemsEager(new ItemTodoFilterDto { TripId = Trip.Id });
            total_items = items.Sum(i => i.Count);
            total_items_packed = items.Sum(i => i.PackedCount);

            todos = await ApiMethods.GetToDosEager(new ItemTodoFilterDto { TripId = Trip.Id });
            total_todos = todos.Count();
            total_todos_completed = todos.Count(i => i.Completed);

            total = total_items + total_todos;

            total_finished = total_items_packed + total_todos_completed;

        }

        public async void GetCountryCovidData(CountryDto countryDto)
        {
            ChartTitle = "Current Covid-19 situation in " + countryDto.Country;
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
