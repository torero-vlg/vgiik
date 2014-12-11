using System.Collections.Generic;

namespace T034.ViewModel
{
    public class CarouselViewModel
    {
        public string CarouselId 
        {
            get { return Folder.Replace("/", "_"); }
        }

        public string Folder { get; set; }
        
        public string Header { get; set; }
        
        public IEnumerable<string> Files { get; set; }
    }
}