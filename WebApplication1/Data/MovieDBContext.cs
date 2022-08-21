using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data.Entities;

namespace MoviesAPI.Data
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }

    }
}
