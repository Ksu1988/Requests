using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{

    public enum SortState
    {
        StatusAsc,    // по статусу по возрастанию
        StatusDesc,   // по статусу по убыванию
        DateAsc, // по дате по возрастанию
        DateDesc   // по дате по убыванию        
    }
}
