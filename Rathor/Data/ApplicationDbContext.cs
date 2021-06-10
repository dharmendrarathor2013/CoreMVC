using Microsoft.EntityFrameworkCore; 
using Rathor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rathor.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<TaskDetail> TaskDetail { get; set; }
        public DbSet<TaskDocument> TaskDocuments { get; set; }
        public DbSet<TaskType> TaskType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Sprint> Sprint { get; set; }
        public DbSet<Project> Project { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to Many between TaskType and TaskDetail
            modelBuilder.Entity<TaskType>().HasMany(a => a.TaskDetail)
                .WithOne(b => b.TaskType)
                .HasForeignKey(b => b.TypeID);

            //One to Many between User and TaskDetail
            modelBuilder.Entity<User>().HasMany(c => c.TaskDetail)
                .WithOne(d => d.User)
                .HasForeignKey(b => b.AssignyId);

            //One to Many between Status and TaskDetail
            modelBuilder.Entity<Status>().HasMany(c => c.TaskDetail)
                .WithOne(d => d.Status)
                .HasForeignKey(b => b.StatusId);

            //One to Many between Sprint and TaskDetail
            modelBuilder.Entity<Sprint>().HasMany(c => c.TaskDetail)
                .WithOne(d => d.Sprint)
                .HasForeignKey(b => b.SprintId);

            //Many to One between TaskDocument and TaskDetail
            modelBuilder.Entity<TaskDocument>().HasOne(c => c.TaskDetail)
                .WithMany(d => d.TaskDocuments)
                .HasForeignKey(b => b.TaskID);

            //One to Many between User and Project
            modelBuilder.Entity<User>().HasMany(c => c.Projects)
                .WithOne(d => d.User)
                .HasForeignKey(b => b.AssignyId);

            //One to Many between Project and Sprint
            modelBuilder.Entity<Project>().HasMany(c => c.Sprints)
                .WithOne(d => d.Project)
                .HasForeignKey(b => b.projectId);
        }
    }
}
