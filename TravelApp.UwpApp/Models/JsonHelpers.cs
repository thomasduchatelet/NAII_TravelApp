using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace TravelApp.Models
{
    class JsonHelpers
    {
        public static HttpStringContent ObjectToHttpContent(Object Body)
        {
            var jsonBody = JsonConvert.SerializeObject(Body);
            HttpStringContent content = new HttpStringContent(jsonBody, encoding: Windows.Storage.Streams.UnicodeEncoding.Utf8, mediaType: "application/json");
            return content;
        }      
    }
}
