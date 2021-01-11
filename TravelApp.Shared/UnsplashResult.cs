using System;
using System.Collections.Generic;
using System.Text;

namespace TravelApp.Shared
{
        public class Urls
        {
            public string raw { get; set; }
            public string full { get; set; }
            public string regular { get; set; }
            public string small { get; set; }
            public string thumb { get; set; }
        }


        public class Source
        {
            public string title { get; set; }
            public string subtitle { get; set; }
            public string description { get; set; }
            public string meta_title { get; set; }
            public string meta_description { get; set; }
        }

        public class Tag
        {
            public string type { get; set; }
            public string title { get; set; }
            public Source source { get; set; }
        }

        public class Result
        {
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime? promoted_at { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string color { get; set; }
            public string blur_hash { get; set; }
            public string description { get; set; }
            public string alt_description { get; set; }
            public Urls urls { get; set; }
        }

        public class UnsplashResult
        {
            public int total { get; set; }
            public int total_pages { get; set; }
            public List<Result> results { get; set; }
        }
}
