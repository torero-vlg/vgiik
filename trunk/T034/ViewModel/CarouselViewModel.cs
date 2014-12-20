using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace T034.ViewModel
{
    public class CarouselViewModel
    {
        public CarouselViewModel(string folder, string path, string header)
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles().Select(f => f.Name);

            Header = header;
            Files = files;
            Folder = folder;
        }

        public string CarouselId 
        {
            get { return Folder.Replace("/", "_"); }
        }

        public string Folder { get; set; }
        
        public string Header { get; set; }
        
        public IEnumerable<string> Files { get; set; }
    }
}