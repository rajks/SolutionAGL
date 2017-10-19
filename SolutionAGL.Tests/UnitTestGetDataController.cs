using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolutionAGL;
using SolutionAGL.Controllers;
using SolutionAGL.Models;

namespace SolutionAGL.Tests
{
    [TestClass]
    public class UnitTestGetDataController
    {
        [TestMethod]
        public async Task TestGetDataMaleCats()
        {
            var dataController = new GetDataController();
            var petDataDisplay = await dataController.GetFormattedData();

            // Check if returned object is null
            Assert.IsNotNull(petDataDisplay);

            // Check array contains cat names is null
            Assert.IsNotNull(petDataDisplay.Male);

            // At least one male owner with pet as Cat
            Assert.AreNotEqual(0, petDataDisplay.Male.Length);
        }

        [TestMethod]
        public async Task TestGetDataFemaleCats()
        {
            var dataController = new GetDataController();
            var petDataDisplay = await dataController.GetFormattedData();

            // Check if returned object is null
            Assert.IsNotNull(petDataDisplay);

            // Check array contains cat names is null
            Assert.IsNotNull(petDataDisplay.Female);

            // At least one male owner with pet as Cat
            Assert.AreNotEqual(0, petDataDisplay.Female.Length);
        }
    }
}
