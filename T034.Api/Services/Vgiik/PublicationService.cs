using System.IO;
using T034.Api.Dto.Vgiik;
using T034.Api.Entity.Vgiik;
using T034.Api.Services.Common;

namespace T034.Api.Services.Vgiik
{
    public interface IPublicationService : IService
    {
        PublicationDto Get(object id);
        PublicationDto Create(PublicationDto dto);
        PublicationDto Update(PublicationDto dto);
    }

    public class PublicationService : AbstractRepository<Publication, PublicationDto, int>, IPublicationService
    {
        public new PublicationDto Create(PublicationDto dto)
        {
            var result = base.Create(dto);

            var path = $"/Content/images/publication/{result.Id}/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return result;
        }
    }
}
