using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace T034.ViewModel
{
    public class PersonViewModel
    {
        public string FullName { get; set; }
        public int PersonId { get; set; }
        public List<CarouselViewModel> Docs { get; set; }

        public IEnumerable<string> Files 
        {
            get
            {
                IEnumerable<string> files = new List<string>();

                try
                {
                    var directory = new DirectoryInfo(FilesFolder);
                    files = directory.GetFiles().Select(f => f.Name);
                }
                catch (Exception ex)
                {
                }
                return files;
            } 
        }

        public string FilesFolder
        {
            get { return string.Format("/Content/images/people/{0}/files/", PersonId); }
        }
    }
}