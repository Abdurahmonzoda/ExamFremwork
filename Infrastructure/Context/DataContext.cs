using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
    }
}
