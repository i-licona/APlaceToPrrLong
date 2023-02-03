using APlaceToPrrLong.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APlaceToPrrLong
{
    public class AplicationDbContext:IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<PetPost> PetPosts { get; set; }
    }
}
