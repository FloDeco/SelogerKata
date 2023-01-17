using SeLoger.Domain;

namespace SeLoger.Application.Contracts;

public interface IClassifiedAdRepository
{
    ClassifiedAd  Insert(ClassifiedAd ad);
    void          ChangeAdStatus(Guid guid, ClassifiedAdStatus adStatus);
    ClassifiedAd? GetById(Guid        guid);
}