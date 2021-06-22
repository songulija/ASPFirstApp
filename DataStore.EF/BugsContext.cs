using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataStore.EF
{
    /// <summary>
    /// have to inherit from DbContext. This instance represents session with database and
    /// can be used to query and save instances of your entities
    /// BugsContext represents database itself
    /// </summary>

    public class BugsContext : DbContext
    {
        /**
         * Create a constructor. With DbContextOptions 
         * these options can be used to configure BugsContext
         * And be use base class constructor. When using BugsContext class
         * we will have to provide those options to this constructor 
         * then it'll go to constructor of DbContext class
         */
        public BugsContext(DbContextOptions options) : base(options)
        {
        }
        /**
         * Each DbSet corresponds to TABLE. Project and Ticket objects
         * will have their own tables
         */

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        /**
         * After having properties that corresponds to tables
         * We need to configure database schema. The relationships
         * between TABLES. WHEN OnModelCreating IS called we are creating 
         * schema in memory that represents database schema(tables, relationships)
         * This is method where we configure that schema and table relationships
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configuring project entity(table) and adding one to many relationship
            //with Ticket table. Project can have many Tickets, and ticket can have one project
            //and ticket table has FOREIGN KEY which is Project id
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            //seeding. to add initial data to database
            //HasData is array of oobjects
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Name = "Project 1" },
                new Project { ProjectId = 2, Name = "Project 2" }
                );
            //initial data for tickets table. assigning to different Projects with ProjectId
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { TicketId = 1, Title = "Bug #1", ProjectId = 1},
                new Ticket { TicketId = 2, Title = "Bug #2", ProjectId = 1 },
                new Ticket { TicketId = 3, Title = "Bug #3", ProjectId = 2 }
                );
        }
    }
}
