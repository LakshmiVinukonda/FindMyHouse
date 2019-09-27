using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindMyHouse
{
    public class Myhouse
    {
        private House _sisterHouse;
        private AllHouses _allhousesList;
        public Myhouse(AllHouses houses)
        {
            _allhousesList = houses;
            // Finding Sister house from list of houses
            _sisterHouse = _allhousesList.houses.Where(sh => sh?.street == "Eberswalder Strasse 55").FirstOrDefault();
            _allhousesList.houses.Remove(_sisterHouse);
        }

        #region List of houses based on distance
        private List<House> SortHousesBasedOnDistance()
        {
            List<House> dhouses = new List<House>();

            Dictionary<House, double> disthouses = new Dictionary<House, double>();
            // Calculating Distance from Sister house to other houses and storing in Dictionary
            _allhousesList.houses.ForEach(h => disthouses.Add(h, h.DistanceTo(_sisterHouse?.coords?.lat ?? 0, _sisterHouse?.coords?.lon ?? 0)));

            // Sorting houses
            foreach (var item in disthouses.Where(x => x.Value != 0).OrderBy(x => x.Value))
                dhouses.Add(item.Key);

            return dhouses;
        }
        #endregion

        #region List of houses based on number of rooms
        private List<House> SortHousesBasedonRooms()
        {
            return _allhousesList.houses.Where(house => house?.@params?.rooms > 5).OrderBy(house => house.@params.rooms).ToList();
        }
        #endregion

        #region List of houses based on street name
        private List<House> SortHousesBasedOnStreet()
        {
            return _allhousesList.houses.Where(house => house.@params == null || (house?.@params?.rooms == 0
            || house?.@params?.value == 0) || house.coords == null || ((house?.coords.lat == 0
            || house?.coords?.lon == 0))).OrderBy(x => x.street).ToList();
        }
        #endregion

        #region Group all sorted list to find the house
        public List<House> GetSortedAllLists()
        {
            var dhouses = SortHousesBasedOnDistance();
            var rhouses = SortHousesBasedonRooms();
            var shouses = SortHousesBasedOnStreet();

            var dcollection = new { houses = dhouses };
            PrintSortedHousesList(dcollection, "Sorted houses based on Distance \n \n");

            var rcollection = new { houses = rhouses };
            PrintSortedHousesList(rcollection, "Sorted houses based on rooms \n \n");

            var scollection = new { houses = shouses };
            PrintSortedHousesList(scollection, "Sorted houses based on street name \n \n");

            var allSortedLists = new List<House>(dhouses.Count + rhouses.Count + shouses.Count);
            allSortedLists.AddRange(dhouses);
            allSortedLists.AddRange(rhouses);
            allSortedLists.AddRange(shouses);

            return allSortedLists;
        }
        #endregion

        // Print sorted houses 
        public void PrintSortedHousesList(object item, string message)
        {
            TextWriter textWriter = Console.Out;
            var jsonResponse = JsonConvert.SerializeObject(item);
            textWriter.WriteLine(message + jsonResponse + "\n");
        }
    }
}
