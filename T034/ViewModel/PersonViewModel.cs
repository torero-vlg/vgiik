using System.Collections.Generic;
using T034.ViewModel.Common;

namespace T034.ViewModel
{
    public class PersonViewModel
    {
        public PersonViewModel()
        {
            Videos = new List<VideoViewModel>();
            Docs = new List<CarouselViewModel>();
            Albums = new List<AlbumViewModel>();
        }

        public string FullName { get; set; }
        public string Title { get; set; }
        public int PersonId { get; set; }
        public string Text { get; set; }

        public List<CarouselViewModel> Docs { get; set; }
        public List<AlbumViewModel> Albums { get; set; }

        public IEnumerable<string> Files { get; set; }
        public IEnumerable<VideoViewModel> Videos { get; set; }
    

        public string FilesFolder
        {
            get { return string.Format("/Content/images/people/{0}/files/", PersonId); }
        }
    }
}