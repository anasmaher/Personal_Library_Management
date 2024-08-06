using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace LibraryManagement.Data
{
    public class LibraryManagementContext : IdentityDbContext<IdentityUserEx>
    {
        public LibraryManagementContext(DbContextOptions<LibraryManagementContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserEx>().ToTable("users", "Security");
            builder.Entity<IdentityRole>().ToTable("Role", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");

            builder.Entity<Book>()
                .HasOne(x => x.User)
                .WithMany(y => y.Books)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Author>()
                .HasOne(x => x.User)
                .WithMany(y => y.Authors)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ViewModels.BookAuthorVM> BookAuthorVM { get; set; } = default!;
    }
}
