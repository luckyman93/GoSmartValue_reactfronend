using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Persistence.EntityFramework.Repositories;
using AV.Persistence.EntityFramework.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AV.Common.Entities;

namespace AV.Persistence.EntityFramework.Tests
{
    public static class TestUtilities
    {
        public static DbContextOptions<ValuationsContext> GetDbOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ValuationsContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName).Options;
        }

        public static ILocationUnitOfWork LocationUnitOfWorkInstance()
        {
            var dbName = Guid.NewGuid().ToString();
            var db = new ValuationsContext(TestUtilities.GetDbOptions(dbName));
            db.Streets.AddRange(GetSampleStreets());

            ILocationsRepository locationsRepository = new LocationsRepository(db);
            IComparableRepository comparablesRepository = new ComparableRepository(db);
            IStreetsRepository streetsRepository = new StreetsRepository(db);
            ILocalAreaRepository localAreaRepository = new LocalAreaRepository(db);

            return new LocationUnitOfWork(db, locationsRepository, streetsRepository, comparablesRepository, localAreaRepository);
        }

        private static IEnumerable<Street> GetSampleStreets()
        {
            return new List<Street>()
            {
                new()
                {
                    LocalityId =0,
                    StreetName = "Street Name 1",
                    Verified = true
                },
                new()
                {
                    LocalityId = 0,
                    StreetName = "Street Name 2",
                    Verified = true
                }
            };
        }
    }
}