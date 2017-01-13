using System.Collections.Generic;
using T034.Api.Dto;
using T034.Api.Entity.Administration;
using T034.Api.Entity.Vgiik;
using T034.Api.Services.Common;

namespace T034.Api.Services
{
    public interface IPersonService : IService
    {
        IEnumerable<PersonDto> Select();
    }

    public class PersonService : AbstractRepository<Person, PersonDto, int>, IPersonService
    {
    }
}
