using SeLoger.Application.Contracts;
using SeLoger.Domain;

namespace SeLoger.Application.Tests.TestDoubles;

internal class ClassifiedAdRepositoryDouble : IClassifiedAdRepository
{
    public Guid               ClassifiedAdStatusChangedGuid;
    public ClassifiedAdStatus ClassifiedAdStatusChangedUsedStatus;
    public ClassifiedAd       GetByIdReturnedValue;

    public ClassifiedAdRepositoryDouble()
    {
    }

    public ClassifiedAdRepositoryDouble(ClassifiedAdStatus status)
    {
        StubStatus = status;
    }

    public ClassifiedAd AdInserted { get; set; }

    public ClassifiedAdStatus StubStatus { get; set; }

    public ClassifiedAd Insert(ClassifiedAd ad)
    {
        ad.Id      = Guid.NewGuid();
        AdInserted = ad;

        return ad;
    }

    public void ChangeAdStatus(Guid guid, ClassifiedAdStatus adStatus)
    {
        ClassifiedAdStatusChangedGuid       = guid;
        ClassifiedAdStatusChangedUsedStatus = adStatus;
    }

    public ClassifiedAd GetById(Guid guid)
    {
        GetByIdReturnedValue = new ClassifiedAd
        {
            Id          = guid,
            Latitude    = 48.85,
            Longitude   = 2.35,
            Description = "new flat",
            Status      = StubStatus,
            Type        = ClassifiedAdType.Flat
        };

        return GetByIdReturnedValue;
    }
}