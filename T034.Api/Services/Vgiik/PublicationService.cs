using T034.Api.Dto.Vgiik;
using T034.Api.Entity.Vgiik;
using T034.Api.Services.Common;

namespace T034.Api.Services.Vgiik
{
    public interface IPublicationService : IService
    {
        PublicationDto Get(object id);
    }

    public class PublicationService : AbstractRepository<Publication, PublicationDto, int>, IPublicationService
    {
    }
}
