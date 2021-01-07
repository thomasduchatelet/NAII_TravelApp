using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAII.TravelApp.Backend.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public List<Trip> Trips { get; set; }

    }
}
