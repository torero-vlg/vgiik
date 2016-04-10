using System.Collections.Generic;
using System.Web.Mvc;
using T034.ViewModel.Common;

namespace T034.ViewModel
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? PersonId { get; set; }
        public string Person { get; set; }
        public int? DepartmentId { get; set; }
        public string Department { get; set; }
        public ICollection<SelectListItem> Persons { get; set; }
        public ICollection<SelectListItem> Departments { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}