using System;
using System.Collections.Generic;
using Db.Entity;

namespace T034.ViewModel
{
    public class CarouselViewModel
    {
        public CarouselViewModel(IEnumerable<NodeViewModel> nodes, string header, string interval = "5000")
        {

            Header = header;

            Files = nodes;

            Interval = interval;
        }

        public string CarouselId 
        {
            get { return Guid.NewGuid().ToString(); }
        }

        public string Folder { get; set; }
        
        public string Header { get; set; }
        
        public string Interval { get; set; }

        public IEnumerable<NodeViewModel> Files { get; set; }
    }
}