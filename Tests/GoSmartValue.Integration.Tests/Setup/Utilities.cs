using AV.Common.Entities;
using AV.Persistence.EntityFramework;
using System;
using System.Collections.Generic;

namespace GoSmartValue.Integration.Tests.Setup
{
    public static class Utilities
    {
        private static Guid user1_Id = Guid.Parse("70a9260f-df17-476b-b89d-34ac10a498a5");

        public static void InitializeDbForTests(ValuationsContext gosmartValueDb)
        {
            gosmartValueDb.Baskets.AddRange(GetBasketsTestData());
            gosmartValueDb.SaveChanges();
        }

        public static void ReinitializeDbForTests(ValuationsContext gosmartValueDb)
        {
            gosmartValueDb.Baskets.RemoveRange(gosmartValueDb.Baskets);
            InitializeDbForTests(gosmartValueDb);
        }

        private static IEnumerable<Basket> GetBasketsTestData()
        {
            return new List<Basket>()
            {
                new Basket(user1_Id),
            };
        }
    }
}
