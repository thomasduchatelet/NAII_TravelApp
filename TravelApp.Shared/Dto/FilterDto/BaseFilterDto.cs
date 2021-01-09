using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace TravelApp.Shared.Dto.FilterDto
{
    public class BaseFilterDto : BaseDto
    {

        public string ParseQuery()
        {
            NameValueCollection query = new NameValueCollection();
            var properties = GetType().GetProperties();

            foreach (var property in properties)
                query[property.Name] = property.GetValue(this).ToString();

            return query.ToString();
        }
    }
}
