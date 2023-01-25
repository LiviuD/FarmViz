using Microsoft.VisualStudio.TestTools.UnitTesting;
using FarmVizServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmVizServices.Tests
{
    [TestClass()]
    public class FileImportTests
    {
        [TestMethod()]
        public void ImportTest_AllGood()
        {
            FarmVizServices.FileImport fileImporter = new FileImport();
            var farms = fileImporter.Import("TestFiles\\farmdata.json");
            Assert.IsTrue(farms.Count == 3);
        }

        [TestMethod()]
        public void ImportTest_UnknownAnimalType()
        {
            FarmVizServices.FileImport fileImporter = new FileImport();
            bool isException = false;
            try
            {
                fileImporter.Import("TestFiles\\farmdata_unknownAnimalType.json");
            }
            catch(ArgumentException)
            {
                isException = true;
            }
            Assert.IsTrue(isException, "It should throw exception!");
        }

        [TestMethod()]
        public void ImportTest_UnknownGender()
        {
            FarmVizServices.FileImport fileImporter = new FileImport();
            bool isException = false;
            try
            {
                fileImporter.Import("TestFiles\\farmdata_unknownGender.json");
            }
            catch (ArgumentException)
            {
                isException = true;
            }
            Assert.IsTrue(isException, "It should throw exception!");
        }

        [TestMethod()]
        public void ImportTest_TooMuchMilk()
        {
            FarmVizServices.FileImport fileImporter = new FileImport();
            bool isException = false;
            try
            {
                fileImporter.Import("TestFiles\\farmData.json");
            }
            catch (ArgumentException)
            {
                isException = true;
            }
            Assert.IsTrue(isException, "It should throw exception!");
        }

        [TestMethod()]
        public void ImportTest_TooMuchFood()
        {
            FarmVizServices.FileImport fileImporter = new FileImport();
            bool isException = false;
            try
            {
                fileImporter.Import("TestFiles\\farmData.json");
            }
            catch (ArgumentException)
            {
                isException = true;
            }
            Assert.IsTrue(isException, "It should throw exception!");
        }
    }
}