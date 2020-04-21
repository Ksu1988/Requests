using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requests.Data;
using Requests.Models;

namespace Requests.Controllers
{
    public class RequestsController : Controller
    {
        private readonly RequestContext _context;

        public RequestsController(RequestContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index(DateTime date1, DateTime date2,
            SortState sortOrder = SortState.StatusAsc, Status status = Status.All)
        {
            //фильтрация
            IQueryable<Request> requests = _context.Requests.Include(x => x.StatusChange);

            if (status != Status.All)
            {
                requests = requests.Where(p => p.Status == status);
            }
            if (date1.Ticks != 0)
            {
                requests = requests.Where(p => p.Date >= date1);
            }
            if (date2.Ticks != 0)
            {
                requests = requests.Where(p => p.Date <= date2);
            }
            // сортировка
            switch (sortOrder)
            {
                case SortState.StatusDesc:
                    requests = requests.OrderByDescending(s => s.Status);
                    break;
                case SortState.DateAsc:
                    requests = requests.OrderBy(s => s.Date);
                    break;
                case SortState.DateDesc:
                    requests = requests.OrderByDescending(s => s.Date);
                    break;
                default:
                    requests = requests.OrderBy(s => s.Status);
                    break;
            }
            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(status, date1, date2),
                Requests = requests

            };
            return View(viewModel);
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.Include(t => t.StatusChange)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Content,Status,Date")] Request request)
        {
            if (ModelState.IsValid)
            {
                var status = new StatusChange()
                {
                    Date = request.Date,
                    RequestId = request.Id,
                    Request = request,
                    Status = request.Status,
                    Comment = "Создана новая заявка"
                };
                request.StatusChange.Add(status);
                _context.Add(status);
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.Include(t => t.StatusChange)
            .FirstOrDefaultAsync(m => m.Id == id);
            var statuses = new List<Status>();
            statuses.Add(request.Status);
            switch (request.Status)
            {
                case Status.Open:
                    statuses.Add(Status.Solved);
                    break;
                case Status.Solved:
                    statuses.Add(Status.Closed);
                    statuses.Add(Status.Return);
                    break;
                //case Status.Closed:
                //    break;
                case Status.Return:
                    statuses.Add(Status.Solved);
                    break;
            }
            ViewData["Status"] = new SelectList(statuses);
          
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,Status,Date,StatusChange")] Request request, string comment)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            var s = request.StatusChange;
            if (String.IsNullOrEmpty(comment))
            {
                ModelState.AddModelError("Comment", "Введите комментарий");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var status = new StatusChange()
                    {
                        Date = request.Date,
                        RequestId = request.Id,
                        Request = request,
                        Status = request.Status,
                        Comment = comment
                    };
                    request.StatusChange.Add(status);
                    _context.Add(status);
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
