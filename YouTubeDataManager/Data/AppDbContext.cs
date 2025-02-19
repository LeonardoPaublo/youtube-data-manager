using Microsoft.EntityFrameworkCore;
using YouTubeDataManager.Models;

public class AppDbContext : DbContext
{
    public DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=youtube-data-manager.db");
}