using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public class IndexViewModel
    {        
            public IEnumerable<Request> Requests { get; set; }                   
            public FilterViewModel FilterViewModel { get; set; }
            public SortViewModel SortViewModel { get; set; }        
    }
}
