using PayPalDemo.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayPalDemo.Models;
using System.ComponentModel.DataAnnotations;

namespace PayPalDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }
        public DbSet<RoleVM> RoleVM { get; set; } = default!;
        public DbSet<UserVM> UserVM { get; set; } = default!;
        public DbSet<UserRoleVM> UserRoleVM { get; set; } = default!;
        public DbSet<IPN> IPNs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
               base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}