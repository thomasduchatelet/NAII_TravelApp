using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared
{
    public class CountryCovidResult
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int Cases { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public int Confirmed { get; set; }

        public int Recovered { get; set; }
        public int Deaths { get; set; }
    }
}
