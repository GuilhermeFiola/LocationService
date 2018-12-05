using System;
using System.Collections.Generic;

namespace MicroserviceCore.LocationService.Models 
{
    public class LocationRecord
    {
        public Guid ID {get; set;}
        public float Latitude {get; set;}
        public float Longitude {get; set;}
        public float Altitude {get; set;}
        public long Timestamp {get; set;}
        public Guid MemberID {get; set;}
    }
}