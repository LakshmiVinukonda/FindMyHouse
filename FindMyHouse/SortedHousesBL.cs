using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindMyHouse
{
    public class SortedHousesBL
    {
        public ISortedHouses sortedHouses;
        public SortedHousesBL(ISortedHouses sortedHouses)
        {
            this.sortedHouses = sortedHouses;
        }

        #region Group all sorted list to find the house
        public List<House> GetSortedAllLists()
        {
            var dhouses = sortedHouses.SortHousesBasedOnDistance();
            var rhouses = sortedHouses.SortHousesBasedonRooms();
            var shouses = sortedHouses.SortHousesBasedOnStreet();

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

        #region Print sorted houses 
        // Printing outputs
        public void PrintSortedHousesList(object item, string message)
        {
            TextWriter textWriter = Console.Out;
            // serialize to json
            var jsonResponse = JsonConvert.SerializeObject(item);
            textWriter.WriteLine(message + jsonResponse + "\n");
        }
        #endregion
    }
}
