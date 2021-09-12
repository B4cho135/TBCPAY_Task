using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance
{
    public class DefaultDbContext:DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
        {

        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<PhoneEntity> Phones { get; set; }
        public DbSet<RelatedPersonEntity> RelatedPersons { get; set; }
        public DbSet<RelatedPersonTypeEntity> RelatedPersonType { get; set; }
        public DbSet<PhoneTypeEntity> PhoneType { get; set; }
    }
}
