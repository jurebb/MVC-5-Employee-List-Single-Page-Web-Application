using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.DataAccessLayer
{
    //DbSet simply represent the collection of all the entities that can be queried from the database. When we write a Linq query again DbSet object it internally converted to query and fired against database.

    public class SalesERPDAL : DbContext
    {
        public DbSet<Employee> Employees { get; set; } //DbSet will represent all the employees that can be queried from the database.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }
    }
}