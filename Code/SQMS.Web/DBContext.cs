using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SQMS.Web.Models;
using System.Data.Entity.Infrastructure;

namespace SQMS.Web
{
    public class SQMSDBContext : DbContext
    {
        public SQMSDBContext()
            : base("name=SQMSDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(c => c.Roles).WithMany(i => i.Users)
               .Map(t => t.MapLeftKey("UserId")
                   .MapRightKey("RoleId")
                   .ToTable("UserRoles"));
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Nisaab> Nisaabs { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionType> RegionTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<SabaqAttendance> SabaqAttendances { get; set; }
        public DbSet<SabaqBook> SabaqBooks { get; set; }
        public DbSet<SabaqGroup> SabaqGroups { get; set; }
        public DbSet<SabaqLog> SabaqLogs { get; set; }
        public DbSet<SabaqRegistration> SabaqRegistrations { get; set; }
        public DbSet<SabaqRequest> SabaqRequests { get; set; }
        public DbSet<SabaqStatus> SabaqStatus { get; set; }
        public DbSet<User> Users { get; set; }
    }
}