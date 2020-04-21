using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public enum Status 
    {
        [Display(Description="Открыта")]
        Open,
        [Description("Решена")]
        Solved,
        Return,
        Closed,
        All
    }

    
}
