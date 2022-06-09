using System;
using System.Collections.Generic;
using AV.Common.Entities;

namespace AV.Persistence.EntityFramework.DataSeeds
{

    internal sealed class MigrationSeedData
    {
        public static IList<District> GetDistricts()
        {
            return new List<District>
            {
                new District {Id = 1, Name = "Southern", Population = 197767, Area = 28470, Density = 6.9465},
                new District {Id = 2, Name = "South-East", Population = 85014, Area = 1780, Density = 47.76},
                new District {Id = 3, Name = "Kweneng", Population = 304549, Area = 31100, Density = 9.793},
                new District {Id = 4, Name = "Kgatleng", Population = 91660, Area = 7960, Density = 11.515},
                new District {Id = 5, Name = "Central", Population = 576064, Area = 142076, Density = 4.054619},
                new District {Id = 6, Name = "North-East", Population = 60264, Area = 550, Density = 11.770},
                new District {Id = 7, Name = "Ngamiland", Population = 152284, Area = 109130, Density = 1.39544},
                new District {Id = 8, Name = "Chobe", Population = 23347, Area = 20800, Density = 1.525},
                new District {Id = 9, Name = "Ghanzi", Population = 43095, Area = 117910, Density = 0.365491},
                new District {Id = 10, Name = "Kgalagadi", Population = 50752, Area = 105200, Density = 0.48243}
            };
        }

        public static IList<Location> GetLocations()
        {
            return  new List<Location>
            {
                //Central District
                new Location { Id = 1, Name = "Bobirwa",DistrictId = 5},
                new Location { Id = 2, Name = "Boteti",DistrictId = 5},
                new Location { Id = 3, Name = "Lerala",DistrictId = 5},
                new Location { Id = 4, Name = "Mahalapye", DistrictId = 5 },
                new Location { Id = 5, Name = "Mmadinare",DistrictId = 5},
                new Location { Id = 6, Name = "Mmaphashalala",DistrictId = 5},
                new Location { Id = 7, Name = "Mogorosi",DistrictId = 5},
                new Location { Id = 8, Name = "Nata",DistrictId = 5},
                new Location { Id = 9, Name = "Paje",DistrictId = 5},
                new Location { Id = 10, Name = "Rakops",DistrictId = 5},
                new Location { Id = 11, Name = "Sebina",DistrictId = 5},
                new Location { Id = 12, Name = "Sefhare",DistrictId = 5},
                new Location { Id = 13, Name = "Serowe",DistrictId = 5},
                new Location { Id = 14, Name = "Palapye",DistrictId = 5},
                new Location { Id = 15, Name = "Shoshong", DistrictId = 5},
                new Location { Id = 16, Name = "Taupye",DistrictId = 5},
                new Location { Id = 17, Name = "Tonota",DistrictId = 5},
                new Location { Id = 18, Name = "Tutume",DistrictId = 5},

                //Chobe District

                //Ghanzi District
                new Location { Id = 19, Name = "Charleshill",DistrictId = 9},
                new Location { Id = 20, Name = "Ghanzi",DistrictId = 9},

                //Kgalagadi District
                new Location { Id = 21, Name = "Hukuntsi",DistrictId = 10},
                new Location { Id = 22, Name = "Kang",DistrictId = 10},
                new Location { Id = 50, Name = "Tshabong",DistrictId = 10},

                //Kgatleng District
                new Location { Id = 23, Name = "Mmathubudukwane",DistrictId = 4},
                new Location { Id = 24, Name = "Mochudi",DistrictId = 4},

                //Kweneng District
                new Location { Id = 25, Name = "Letlhakeng",DistrictId = 3},
                new Location { Id = 26, Name = "Molepolole",DistrictId = 3},

                //North-East District
                new Location { Id = 27, Name = "Francistown",DistrictId = 6 },
                new Location { Id = 28, Name = "Masunga",DistrictId = 6 },
                
                //North-West District/Ngamiland District
                new Location { Id = 29, Name = "Ngamiland East",DistrictId = 7},
                new Location { Id = 30, Name = "Ngamiland West",DistrictId = 7},
                new Location { Id = 31, Name = "Okavango",DistrictId = 7},

                //South-East District
                new Location { Id = 33, Name = "Gaborone", DistrictId = 2 },
                new Location { Id = 32, Name = "Mogobane",DistrictId = 2},
                new Location { Id = 34, Name = "Otse",DistrictId = 2},
                new Location { Id = 35, Name = "Ramotswa",DistrictId = 2},
                new Location { Id = 36, Name = "Tlokweng", DistrictId = 2 },

                //Southern District
                new Location { Id = 37, Name = "Goodhope",DistrictId = 1},
                new Location { Id = 38, Name = "Jwaneng",DistrictId = 1},
                new Location { Id = 39, Name = "Kanye",DistrictId = 1},
                new Location { Id = 40, Name = "Mabutsane",DistrictId = 1},
                new Location { Id = 41, Name = "Moshupa",DistrictId = 1},                
                new Location { Id = 42, Name = "Bokaa",DistrictId = 4},                
                new Location { Id = 43, Name = "Metsimotlhabe",DistrictId = 3 },                
                new Location { Id = 44, Name = "Mmopane",DistrictId = 3},                
                new Location { Id = 45, Name = "Oodi",DistrictId = 4 },                
                new Location { Id = 46, Name = "Rasesa",DistrictId = 4 },                
                new Location { Id = 47, Name = "Thamaga",DistrictId = 3},                
                new Location { Id = 48, Name = "Sandveld",DistrictId = 5},                
            };
        }

        public static IList<Locality> GetLocalities()
        {
            return new List<Locality>()
            {
                new Locality {Id = 1, LocationId = 33, Name = "BBS Mall"},
                new Locality {Id = 2, LocationId = 33, Name = "block 3"},
                new Locality {Id = 3, LocationId = 33, Name = "Block 4"},
                new Locality {Id = 4, LocationId = 33, Name = "Block 5"},
                new Locality {Id = 5, LocationId = 33, Name = "Block 6"},
                new Locality {Id = 6, LocationId = 33, Name = "block 7"},
                new Locality {Id = 7, LocationId = 33, Name = "block 8"},
                new Locality {Id = 8, LocationId = 33, Name = "Block 9"},
                new Locality {Id = 9, LocationId = 33, Name = "Block 10"},
                new Locality {Id = 10, LocationId = 33, Name = "Broadhurst"},
                new Locality {Id = 11, LocationId = 33, Name = "Broadhurst (Ginger)"},
                new Locality {Id = 12, LocationId = 33, Name = "Commerce park"},
                new Locality {Id = 13, LocationId = 33, Name = "Ext 11"},
                new Locality {Id = 14, LocationId = 33, Name = "Ext 12"},
                new Locality {Id = 15, LocationId = 33, Name = "Ext 14"},
                new Locality {Id = 16, LocationId = 33, Name = "Ext 19"},
                new Locality {Id = 17, LocationId = 33, Name = "Ext 16"},
                new Locality {Id = 18, LocationId = 33, Name = "Ext 25"},
                new Locality {Id = 19, LocationId = 33, Name = "Ext 9"},
                new Locality {Id = 20, LocationId = 33, Name = "Gaborone west"},
                new Locality {Id = 21, LocationId = 33, Name = "Gaborone"},
                new Locality {Id = 22, LocationId = 33, Name = "Kgale view"},
                new Locality {Id = 23, LocationId = 33, Name = "Mogoditshane"},
                new Locality {Id = 24, LocationId = 33, Name = "Notwane"},
                new Locality {Id = 25, LocationId = 33, Name = "Old Naledi"},
                new Locality {Id = 26, LocationId = 33, Name = "Phakalane"},
                new Locality {Id = 27, LocationId = 33, Name = "Phase 1"},
                new Locality {Id = 28, LocationId = 33, Name = "Phase 2"},
                new Locality {Id = 29, LocationId = 33, Name = "Phase 3"},
                new Locality {Id = 30, LocationId = 33, Name = "Phase 4"},
                new Locality {Id = 31, LocationId = 33, Name = "Tlokweng"},
                new Locality {Id = 32, LocationId = 33, Name = "Tshweneng"},
                new Locality {Id = 33, LocationId = 33, Name = "Tsholofelo West"},
                new Locality {Id = 34, LocationId = 33, Name = "Tsholofelo East"},
                new Locality {Id = 35, LocationId = 33, Name = "Tsholofelo"},
                new Locality {Id = 36, LocationId = 33, Name = "Taung"},
                new Locality {Id = 37, LocationId = 33, Name = "Tawana"},
                new Locality {Id = 38, LocationId = 33, Name = "whitecity"},
                new Locality {Id = 39, LocationId = 4, Name = "Flowertown"},
                new Locality {Id = 40, LocationId = 43, Name = "Gaphatshwa"},
                new Locality {Id = 41, LocationId = 27, Name = "Lekgwapheng"},
                new Locality {Id = 42, LocationId = 2, Name = "Mafenyatlala"},
                new Locality {Id = 43, LocationId = 45, Name = "Matebeleng"},
                new Locality {Id = 44, LocationId = 45, Name = "Modipane"},
                new Locality {Id = 45, LocationId = 36, Name = "Boatle"},
                new Locality {Id = 46, LocationId = 36, Name = "Lesetlhana"},
                new Locality {Id = 47, LocationId = 46, Name = "West Extension"},
                new Locality {Id = 48, LocationId = 13, Name = "Mere"},
                new Locality {Id = 49, LocationId = 13, Name = "Morula"},
                new Locality {Id = 50, LocationId = 15, Name = "matlhabana"},
                new Locality {Id = 51, LocationId = 28, Name = "Mine stone"},
                new Locality {Id = 52, LocationId = 28, Name = "Tati River"},
                new Locality {Id = 53, LocationId = 33, Name = "Partial "},
                new Locality {Id = 54, LocationId = 33, Name = "Ext 10"},
                new Locality {Id = 55, LocationId = 33, Name = "Village"},
                new Locality {Id = 56, LocationId = 33, Name = "Ext 2"},
                new Locality {Id = 57, LocationId = 33, Name = "Ext 27"},
                new Locality {Id = 58, LocationId = 33, Name = "Tawana"},
                new Locality {Id = 59, LocationId = 33, Name = "Ledumang"},
                new Locality {Id = 60, LocationId = 33, Name = "Block 3 (Industrial)"},
                new Locality {Id = 61, LocationId = 33, Name = "Gaborone North"}
            };
        }

        public static IList<ComparableBandSize> GetBandSizes()
        {
            return new List<ComparableBandSize>
            {
                new ComparableBandSize
                {
                    BandName = "LowIncome",
                    LowerBandLimit = 460,
                    UpperBandLimit = 600
                },
                new ComparableBandSize
                {
                    BandName = "MiddleIncome",
                    LowerBandLimit = 601,
                    UpperBandLimit = 800
                },
                new ComparableBandSize
                {
                    BandName = "HighIncome",
                    LowerBandLimit = 800,
                    UpperBandLimit = int.MaxValue
                },
            };
        }

        public static IList<Role> GetUserRoles()
        {
            return new List<Role>
            {
                new Role{Id = new Guid("af4da8fd-6a63-4249-8b6a-d8068224f051"), NormalizedName="ADMIN", Name = "admin"},
                new Role{Id = new Guid("a6c02a94-9984-423b-aaf8-7394f26fe6d0"), NormalizedName="CLIENT", Name = "client"},
                new Role{Id = new Guid("4eeebad8-3100-4103-8757-5e60780fb160"), NormalizedName="ANALYST", Name = "analyst"},
                new Role{Id = new Guid("15733086-823e-452e-be44-e49a975f3964"), NormalizedName="MANAGER", Name = "manager"},
                new Role{Id = new Guid("08d7809a-443e-6a79-090d-0de147013b55"), NormalizedName="USER", Name = "user"},
            };
        }
    }
}
