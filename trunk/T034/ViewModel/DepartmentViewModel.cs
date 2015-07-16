using System.Collections.Generic;

namespace T034.ViewModel
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
            Albums = new List<CarouselViewModel>();
            Files = new List<string>();
            Videos = new List<string>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Staff { get; set; }

        public string Text { get; set; }

        public string MainPhoto { get; set; }
        public string MainPhotoDescription { get; set; }

        public List<CarouselViewModel> Albums { get; set; }
		
		public IEnumerable<string> Files { get; set; }
		public string FilesFolder
        {
            get { return string.Format("/Content/images/department/{0}/files/", Id); }
        }

        public string Nodes { get; set; }

        public IEnumerable<string> Videos { get; set; }
    }
}