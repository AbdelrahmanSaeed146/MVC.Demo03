using Microsoft.EntityFrameworkCore;
using MVC.Demo03.DAL.Data.configurations;
using MVC.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.DAL.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

       /// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       ///=> optionsBuilder.UseSqlServer("Server = . ; Datebase = MvcApp;Trusted_Connection=True");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
    }
}
