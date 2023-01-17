using SeLoger.Application.Contracts;
using SeLoger.Domain;

namespace SeLoger.Infrastructure.Persistence;

public class ClassifiedAdRepository : IClassifiedAdRepository
{
    private readonly ClassifiedAdsDbContext adsDbContext;

    public ClassifiedAdRepository(ClassifiedAdsDbContext adsDbContext)
    {
        this.adsDbContext = adsDbContext;
    }

    public ClassifiedAd Insert(ClassifiedAd ad)
    {
        var classifiedAdEntity = new ClassifiedAdEntity
        {
            Id          = Guid.NewGuid(),
            Title       = ad.Title,
            Description = ad.Description,
            Type        = (int)ad.Type,
            Latitude    = ad.Latitude,
            Longitude   = ad.Longitude,
            Status      = (int)ad.Status
        };

        adsDbContext.Add(classifiedAdEntity);
        adsDbContext.SaveChanges();

        return new ClassifiedAd
        {
            Id          = classifiedAdEntity.Id,
            Title       = ad.Title,
            Description = ad.Description,
            Type        = ad.Type,
            Latitude    = ad.Latitude,
            Longitude   = ad.Longitude,
            Status      = ad.Status
        };
    }

    public void ChangeAdStatus(Guid guid, ClassifiedAdStatus adStatus)
    {
        var classifiedAdEntity = adsDbContext.ClassifiedAds.Find(guid);
        classifiedAdEntity.Status = (int)adStatus;
        adsDbContext.SaveChanges();
    }

    public ClassifiedAd GetById(Guid guid)
    {
        var classifiedAdEntity = adsDbContext.ClassifiedAds.Find(guid);
        return new ClassifiedAd
        {
            Id          = classifiedAdEntity.Id,
            Title       = classifiedAdEntity.Title,
            Description = classifiedAdEntity.Description,
            Latitude    = classifiedAdEntity.Latitude,
            Longitude   = classifiedAdEntity.Longitude,
            Status      = (ClassifiedAdStatus)classifiedAdEntity.Status,
            Type        = (ClassifiedAdType)classifiedAdEntity.Type
        };
    }
}