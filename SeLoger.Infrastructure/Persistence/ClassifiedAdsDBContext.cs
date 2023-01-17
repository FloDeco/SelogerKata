using Microsoft.EntityFrameworkCore;

namespace SeLoger.Infrastructure.Persistence;

public class ClassifiedAdsDbContext : DbContext
{
    public ClassifiedAdsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ClassifiedAdEntity> ClassifiedAds { get; set; }
}