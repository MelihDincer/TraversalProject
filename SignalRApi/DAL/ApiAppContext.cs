using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SignalRApi.DAL
{
    public class ApiAppContext : DbContext
    {
        public ApiAppContext(DbContextOptions<ApiAppContext> options): base(options) { }
        public DbSet<Visitor> Visitors { get; set; }
    }
}
