using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SeLoger.Domain;
using SeLoger.Infrastructure.Persistence;

namespace SeLoger.Infrastructure.Tests;

public class ClassifiedAdRepositoryTest
{
    private readonly ClassifiedAdEntity                       classifiedAdEntitySampleFlat;
    private readonly ClassifiedAdEntity                       classifiedAdEntitySampleHouse;
    private readonly DbContextOptions<ClassifiedAdsDbContext> contextOptions;

    public ClassifiedAdRepositoryTest()
    {
        contextOptions = new DbContextOptionsBuilder<ClassifiedAdsDbContext>()
                         .UseInMemoryDatabase("ClassifiedAdsTest")
                         .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                         .Options;

        using var context = new ClassifiedAdsDbContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        classifiedAdEntitySampleHouse = new ClassifiedAdEntity
        {
            Title       = "house A",
            Description = "nice house",
            Id          = Guid.NewGuid(),
            Latitude    = 48.85,
            Longitude   = 2.35,
            Status      = (int)ClassifiedAdStatus.WaitingForValidation,
            Type        = (int)ClassifiedAdType.House
        };
        classifiedAdEntitySampleFlat = new ClassifiedAdEntity
        {
            Title       = "flat C",
            Description = "nice flat",
            Id          = Guid.NewGuid(),
            Latitude    = 48.85,
            Longitude   = 2.35,
            Status      = (int)ClassifiedAdStatus.WaitingForValidation,
            Type        = (int)ClassifiedAdType.Flat
        };
        context.AddRange(classifiedAdEntitySampleHouse, classifiedAdEntitySampleFlat);

        context.SaveChanges();
    }

    [Fact]
    public void Repository_should_insert_into_database_new_classified_ad()
    {
        //Arrange
        var classifiedAdRepository = new ClassifiedAdRepository(new ClassifiedAdsDbContext(contextOptions));
        var ad = new ClassifiedAd
        {
            Title       = "fake house",
            Description = "house with swimming pool",
            Latitude    = 13.4,
            Longitude   = 13.2,
            Type        = 0
        };

        //Act
        var classifiedAd = classifiedAdRepository.Insert(ad);

        //Assert
        classifiedAd.Id.Should().NotBeEmpty();
        classifiedAd.Title.Should().Be(ad.Title);
        classifiedAd.Description.Should().Be(ad.Description);
        classifiedAd.Latitude.Should().Be(ad.Latitude);
        classifiedAd.Longitude.Should().Be(ad.Longitude);
    }

    [Fact]
    public void Repository_should_update_status_when_change_ad_status_is_called()
    {
        //Arrange
        var dbContext    = new ClassifiedAdsDbContext(contextOptions);
        var adRepository = new ClassifiedAdRepository(dbContext);


        //Act
        adRepository.ChangeAdStatus(classifiedAdEntitySampleFlat.Id, ClassifiedAdStatus.Published);

        //Assert
        dbContext.ClassifiedAds.Find(classifiedAdEntitySampleFlat.Id).Status.Should()
                 .Be((int)ClassifiedAdStatus.Published);
    }
}