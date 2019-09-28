using FindMyHouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindMyHouseUnitTest
{
    [TestClass]
    public class SortedHousesTests
    {
        [TestMethod]
        public void DeSerializeJson_IsCorrectResponse_ReturnstObject()
        {
            List<House> jsonHouses = new List<House>();
            var result = Program.DeSerializeJson();
            Assert.IsInstanceOfType(result, jsonHouses.GetType());
            Assert.IsNotNull(result, "Value must be given");
        }

        private List<House> GetHouses()
        {
            return Program.DeSerializeJson();
        }

        [TestMethod]
        public void SortHousesBasedOnRooms_RoomCount_ReturnsObject()
        {
            //Arrange
            var sortedHouses = new SortedHouses(GetHouses());
            var result = sortedHouses.SortHousesBasedonRooms();
            var expectedList = result.OrderBy(x => x.@params.rooms);
            Assert.IsTrue(expectedList.SequenceEqual(result));
        }

        [TestMethod]
        public void SortHousesBasedOnStreet_StreetName_ReturnsObject()
        {
            //Arrange
            var sortedHouses = new SortedHouses(GetHouses());
            var result = sortedHouses.SortHousesBasedOnStreet();
            var expectedList = result.OrderBy(x => x.street);
            Assert.IsTrue(expectedList.SequenceEqual(result));
        }
    }
}
