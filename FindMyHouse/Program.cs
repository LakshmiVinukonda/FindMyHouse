using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindMyHouse
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var result = GetSortedAllLists();
                result = result.Distinct().ToList();

                // House based on my requirments
                var myHouse = result.Where(house => house.@params?.rooms >= 10 && house.@params?.value <= 5000000).FirstOrDefault();
                var response = new { houses = myHouse };
                PrintSortedHousesList(response, "This is my House \n \n");
            }
            catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { throw ex; }
            
        }

        #region DeSerialize Json
        // Deserialize Json to AllHouses
        public static List<House> DeSerializeJson()
        {
            string json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"houses.json"));
            AllHouses jsonHouses = JsonConvert.DeserializeObject<AllHouses>(json);
            return jsonHouses.houses;
        }
        #endregion

        #region Group all sorted list to find the house
        static List<House> GetSortedAllLists()
        {
            SortedHouses sortedHouses = new SortedHouses(DeSerializeJson());

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
        static void PrintSortedHousesList(object item, string message)
        {
            TextWriter textWriter = Console.Out;
            // serialize to json
            var jsonResponse = JsonConvert.SerializeObject(item);
            textWriter.WriteLine(message + jsonResponse + "\n");
        }
        #endregion
    }
}
