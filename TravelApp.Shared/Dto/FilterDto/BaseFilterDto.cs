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
            var properties = GetType().GetProperties();
            var query = "";

            foreach (var property in properties)
                if (property.GetValue(this) != null)
                    if(!(property.Name.Equals("Id") && (long) property.GetValue(this) ==0))
                        query += property.Name +"=" + property.GetValue(this).ToString() + "&";
            return query.EndsWith("&") ? query.Substring(0, query.Length - 1) : query;
        }
    }
}
