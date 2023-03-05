using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }


        public DbSet<MetricsModel> Metrics { get; set; }
    }
}
