using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public class StatusChange
    {
        /// <summary>
        /// Id заявки.
        /// </summary>
        public int StatusChangeId { get; set; }


        [Display(Name = "Статус заявки")]
        public Status Status { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Дата изменения")]
        public DateTime Date { get; set; }


        [Display(Name = "Дата изменения")]
        public String Comment { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

    }
}
