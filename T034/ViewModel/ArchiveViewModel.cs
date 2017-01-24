using System.Collections.Generic;
using T034.Api.Entity.Vgiik;

namespace T034.ViewModel
{

    //TODO удалить если не используется
    public class ArchiveViewModel
    {
        public List<Person> Persons { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
    }
}