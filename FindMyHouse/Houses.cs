using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyHouse
{
    public class House
    {
        public Coordinates coords { get; set; }
        public Params @params { get; set; }
        public string street { get; set; }
    }

    public class Coordinates
    {
        public Coordinates(double latitude, double longitude)
        {
            lat = latitude;
            lon = longitude;
        }
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Params
    {
        public int rooms { get; set; }
        public long value { get; set; }
    }

    public class AllHouses
    {
        public AllHouses() { houses = new List<House>(); }
        public List<House> houses { get; set; }
    }
}
