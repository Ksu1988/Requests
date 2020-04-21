using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Requests.Models;

namespace Requests.Data
{
    public class RequestContext : DbContext
    {
        public RequestContext(DbContextOptions<RequestContext> options)
          : base(options)
        {
        }

        public DbSet<StatusChange> StatusChange { get; set; }

        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new EnumToStringConverter<Status>();
            modelBuilder
            .Entity<Request>()
            .Property(e => e.Status)
            .HasConversion(converter);

        }
    }
}
