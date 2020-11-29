using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Context
{
    public class CanastaContext: DbContext
    {
        public CanastaContext(DbContextOptions<CanastaContext> options) : base(options) { }

        public DbSet<Canasta> canasta { get; set; }
        public DbSet<Orden> orden { get; set; }
    }
}
