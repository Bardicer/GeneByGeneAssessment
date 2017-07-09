using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneByGeneAssessment.Models
{
    public class GBGContext : DbContext
    {
        public GBGContext(DbContextOptions<GBGContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Status> Statuses { get; set; }


    }
}
