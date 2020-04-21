using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public class SortViewModel
    {
        public SortState StatusSort { get; private set; } // значение для сортировки по статусу
        public SortState DateSort { get; private set; }    // значение для сортировки по дате
       
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            StatusSort = sortOrder == SortState.StatusAsc ? SortState.StatusAsc : SortState.StatusAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;          
            Current = sortOrder;
        }
    }
}
