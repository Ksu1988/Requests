using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Requests.Models
{
    public class Request
    {
        /// <summary>
        /// Id заявки.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название заявки.
        /// </summary>
        [Display(Name = "Название заявки")]
        public string Name { get; set; }

        [Display(Name = "Содержание заявки")]
        public string Content { get; set; }

        //  [DataType(DataType.Custom)]
        [Display(Name = "Статус заявки")]
        public Status Status { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Дата заявки")]
        public DateTime Date { get; set; }

        /// <summary>
        /// История заявки
        /// </summary>
        [Display(Name = "История заявки")]
        public List<StatusChange> StatusChange { get; set; } = new List<StatusChange>();
    }
}
