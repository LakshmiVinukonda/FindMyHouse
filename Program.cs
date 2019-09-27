using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace FindMyHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"houses.json"));
                AllHouses jsonHouses = JsonConvert.DeserializeObject<AllHouses>(json);

                Myhouse myhouse = new Myhouse(jsonHouses);

                var result = myhouse.GetSortedAllLists();
                result = result.Distinct().ToList();

                // House based on my requirments
                var myHouse = result.Where(house => house.@params?.rooms >= 10 && house.@params?.value <= 5000000).FirstOrDefault();
                var response = new { houses = myHouse };
                myhouse.PrintSortedHousesList(response, "This is my House \n \n");
            }
            catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
            catch (Exception ex) { throw ex; }
            
        }

       
    }
}
