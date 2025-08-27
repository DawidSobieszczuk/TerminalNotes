using DawidSobieszczuk.TerminalNotes.Data.Interfaces;
using DawidSobieszczuk.TerminalNotes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DawidSobieszczuk.TerminalNotes.Data.DataProviders
{
    public class DatabaseAppDataProvider(IDbContextFactory<AppDbContext> dbContextFactory) : IAppDataProvider
    {
        public async Task DeleteNoteAsync(Note note)
        {
            AppDbContext dbContext = await dbContextFactory.CreateDbContextAsync();
            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<Note>> GetNotesAsync()
        {
            AppDbContext dbContext = await dbContextFactory.CreateDbContextAsync();
            return dbContext.Notes.AsNoTracking();
        }

        public async Task InsertNoteAsync(Note note)
        {
            AppDbContext dbContext = await dbContextFactory.CreateDbContextAsync();
            await dbContext.Notes.AddAsync(note);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            AppDbContext dbContext = await dbContextFactory.CreateDbContextAsync();
            dbContext.Notes.Update(note);
            await dbContext.SaveChangesAsync();
        }
    }
}
