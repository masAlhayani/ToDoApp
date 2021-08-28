using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDolist.Models
{
    public class ToDoListContext:DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext> options)
             : base(options)
        { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category {
                    CategoryId="work",
                    CategoryName="Work"
                },
                 new Category
                 {
                     CategoryId = "home",
                     CategoryName = "Home"
                 },
                  new Category
                  {
                      CategoryId = "ex",
                      CategoryName = "Excersices"
                  },
                   new Category
                   {
                       CategoryId = "shopping",
                       CategoryName = "Shoping"
                   }
                );

            modelBuilder.Entity<Status>().HasData(

                new Status { 
                    StatusID="open",
                    StatusName="Open"
                },
                 new Status
                 {
                     StatusID = "closed",
                     StatusName = "Completed"
                 }

                );
        }

    }
        
}
