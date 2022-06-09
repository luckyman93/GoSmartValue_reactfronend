using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Persistence.EntityFramework.UnitOfWorks;
using NUnit.Framework;
using System;
using System.Linq;
using District = AV.Common.Entities.District;
using Street = AV.Common.Entities.Street;

namespace AV.Persistence.EntityFramework.Tests
{
    [TestFixture]
    public class LocationUnitofWorkTests
    {

        [Test]
        public void GetAllLocations_Returns_ListOfTowns_With_Gaborone()
        {
            var dbOptions = TestUtilities.GetDbOptions(Guid.NewGuid().ToString());

            using (var db = new ValuationsContext(dbOptions))
            {
                db.Districts.Add(new District { Id = 1, Name = "Gaborone" });
                db.SaveChanges();
                Assert.That(db.Districts.Count(), Is.EqualTo(1));
            }
        }

        [TestCase("Street Name 1")]
        [TestCase("Street Name 2")]
        public void AddStreetName_AddStreet_if_not_exists(string streetName)
        {
            //Arrange
            ILocationUnitOfWork locationUnitOfWork = TestUtilities.LocationUnitOfWorkInstance();
            //Act
            locationUnitOfWork.AddStreetName(1, 1, streetName);
            var result = locationUnitOfWork.GetStreets(0).SingleOrDefault(s => s.StreetName == streetName);
            Assert.That(result.StreetName, Is.EqualTo(streetName));
        }

        [TestCase(null, Metric.Acres, 0)]
        [TestCase(1, Metric.Acres, Constants.OneAcreToMetersSquared)]
        [TestCase(1.3, Metric.Acres, 5260.9133486)]
        [TestCase(1, Metric.Hectres, Constants.OneHectreToMetersSquared)]
        [TestCase(1, Metric.SquareMetres, 1)]
        public void ConvertToMetresSquared_Returns_correct_values_by_metric(decimal? size, Metric metric, decimal? expected)
        {
            var result = LocationUnitOfWork.ConvertToMetreSquared(size, metric);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
