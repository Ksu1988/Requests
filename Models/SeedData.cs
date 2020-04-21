using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Requests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RequestContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RequestContext>>()))
            {

                if (context.Requests.Any())
                {
                    return;   
                }

                var request1 = new Request
                {
                    Name = "Заявка1",
                    Content = "Нужно, чтобы все работало хорошо и быстро",
                    Status = Status.Open,
                    Date = new DateTime(2020, 04, 1)
                };
                var statusChange1 = new StatusChange
                {
                    Status = Status.Open,
                    Date = new DateTime(2020, 04, 1),
                    Comment = "Новая заявка",
                    Request = request1
                };
                request1.StatusChange.Add(statusChange1);
                context.StatusChange.Add(statusChange1);

                var request2 = new Request
                {
                    Name = "Заявка2",
                    Content = "Заявка номер два, очень важная",
                    Status = Status.Open,
                    Date = new DateTime(2020, 04, 10)
                };
                var statusChange2 = new StatusChange
                {
                    Status = Status.Open,
                    Date = new DateTime(2020, 04, 10),
                    Comment = "Новая заявка",
                    Request = request2
                };
                request2.StatusChange.Add(statusChange2);
                context.StatusChange.Add(statusChange2);

                var request3 = new Request
                {
                    Name = "Заявка3",
                    Content = "Нужно, чтобы все работало хорошо и быстро. Срочно",
                    Status = Status.Solved,
                    Date = new DateTime(2020, 04, 13)
                };
                var statusChange3 = new StatusChange
                {
                    Status = Status.Open,
                    Date = new DateTime(2020, 04, 11),
                    Comment = "Новая заявка",
                    Request = request3
                };
                var statusChange5 = new StatusChange
                {
                    Status = Status.Solved,
                    Date = new DateTime(2020, 04, 13),
                    Comment = "Все решили",
                    Request = request3
                };
                request3.StatusChange.Add(statusChange3);
                request3.StatusChange.Add(statusChange5);
                context.StatusChange.AddRange(statusChange3, statusChange5);

                var request4 = new Request
                {
                    Name = "Заявка4",
                    Content = "Нужно, чтобы все работало хорошо и правильно",
                    Status = Status.Open,
                    Date = new DateTime(2020, 04,18)
                };
                var statusChange4 = new StatusChange
                {
                    Status = Status.Open,
                    Date = new DateTime(2020, 04, 18),
                    Comment = "Новая заявка",
                    Request = request4
                };
                request4.StatusChange.Add(statusChange4);
                context.StatusChange.Add(statusChange4);


                context.Requests.AddRange(request1, request2, request3, request4);
                context.SaveChanges();
            }
        }
    }
}
