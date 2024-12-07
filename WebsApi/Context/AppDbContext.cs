using Microsoft.EntityFrameworkCore;
using WebsApi.Models;

namespace WebsApi.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<WebItem> Items { get; set; } 

    }
}
