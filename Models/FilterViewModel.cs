using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Requests.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(Status status, DateTime date1, DateTime date2)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            var statuses = new List<Status>();
            statuses.Add(Status.All);
            statuses.Add(Status.Open);
            statuses.Add(Status.Closed);
            statuses.Add(Status.Return);
            statuses.Add(Status.Solved);

            Statuses = new SelectList(statuses, status);
            SelectedStatus = status;
            SelectedDate1 = date1;
            SelectedDate2 = date2;
        }
        public SelectList Statuses { get; private set; } // список заявок
        public Status SelectedStatus { get; private set; }   // выбранная заявка по статусу
        public DateTime SelectedDate1 { get; private set; }
        public DateTime SelectedDate2 { get; private set; }
    }
}
