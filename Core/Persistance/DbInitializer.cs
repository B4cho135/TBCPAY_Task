using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance
{
    public static class DbInitializer
    {
        public static async Task Initialize(DefaultDbContext context)
        {

            if (context.Cities.Count() == 0)
            {
                await context.Cities.AddAsync(new CityEntity()
                {
                    Name = "Tbilisi"
                });

                await context.Cities.AddAsync(new CityEntity()
                {
                    Name = "Batumi"
                });
            }

            if (context.RelatedPersonType.Count() == 0)
            {
                await context.RelatedPersonType.AddRangeAsync(new List<RelatedPersonTypeEntity>() {
                    new RelatedPersonTypeEntity() {
                        Type = "Colleague"
                    },
                    new RelatedPersonTypeEntity() {
                        Type = "Familiar"
                    },
                    new RelatedPersonTypeEntity() {
                        Type = "Relative"
                    },
                     new RelatedPersonTypeEntity() {
                        Type = "Other"
                    }
                });
            }

            if (context.PhoneType.Count() == 0)
            {
                await context.PhoneType.AddRangeAsync(new List<PhoneTypeEntity>() {
                    new PhoneTypeEntity() {
                        Type = "Mobile"
                    },
                    new PhoneTypeEntity() {
                        Type = "Home"
                    },
                    new PhoneTypeEntity() {
                        Type = "Office"
                    },
                });
            }


            context.SaveChanges();

        }
    }
}
