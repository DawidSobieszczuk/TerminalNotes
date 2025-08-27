using Microsoft.EntityFrameworkCore;

namespace DawidSobieszczuk.TerminalNotes.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Note>()
                .Property(n => n.Tags)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
