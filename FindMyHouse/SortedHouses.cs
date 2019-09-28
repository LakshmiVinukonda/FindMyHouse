using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindMyHouse
{
    public class SortedHouses
    {
        private House _sisterHouse;
        private List<House> _allhousesList;
        public SortedHouses(List<House> houses)
        {
            _allhousesList = houses;
            // Finding Sister house from list of houses
            _sisterHouse = _allhousesList.Where(sh => sh?.street == "Eberswalder Strasse 55").FirstOrDefault();
            _allhousesList.Remove(_sisterHouse);
        }

        #region List of houses based on distance
        public List<House> SortHousesBasedOnDistance()
        {
            Dictionary<House, double> disthouses = new Dictionary<House, double>();
            // Calculating Distance from Sister house to other houses and storing in Dictionary
            _allhousesList.ForEach(h => disthouses.Add(h, h.DistanceTo(_sisterHouse?.coords?.lat ?? 0, _sisterHouse?.coords?.lon ?? 0)));
            List<House> dhouses = new List<House>();
            // Sorting houses
            foreach (var item in disthouses.Where(x => x.Value != 0).OrderBy(x => x.Value))
                dhouses.Add(item.Key);

            return dhouses;
        }
        #endregion

        #region List of houses based on number of rooms
        public List<House> SortHousesBasedonRooms()
        {
            return _allhousesList.Where(house => house?.@params?.rooms > 5).OrderBy(house => house.@params.rooms).ToList();
        }
        #endregion

        #region List of houses based on street name
        public List<House> SortHousesBasedOnStreet()
        {
            return _allhousesList.Where(house => house.@params == null || (house?.@params?.rooms == 0
            || house?.@params?.value == 0) || house.coords == null || ((house?.coords.lat == 0
            || house?.coords?.lon == 0))).OrderBy(x => x.street).ToList();
        }
        #endregion

    }
}
