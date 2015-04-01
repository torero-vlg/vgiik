using System.Collections.Generic;

namespace T034.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Staff { get; set; }

        public string Text { get; set; }

        public string MainPhoto { get; set; }
        public string MainPhotoDescription { get; set; }

        public List<CarouselViewModel> Albums { get; set; }
    }
}