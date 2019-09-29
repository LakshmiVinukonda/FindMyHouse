using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindMyHouse
{
    public class Program
    {
        public ISortedHouses sortedHouses;
        static void Main(string[] args)
        {
            try
            {
                SortedHousesBL sortedHouses = new SortedHousesBL(new SortedHouses(DeSerializeJson()));
                var result = sortedHouses.GetSortedAllLists();
                result = result.Distinct().ToList();

                // House based on my requirments
                var myHouse = result.Where(house => house.@params?.rooms >= 10 && house.@params?.value <= 5000000).FirstOrDefault();
                var response = new { houses = myHouse };
                sortedHouses.PrintSortedHousesList(response, "This is my House \n \n");
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
    }
}
