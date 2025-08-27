using DawidSobieszczuk.TerminalNotes.Data.Models;

namespace DawidSobieszczuk.TerminalNotes.Data.Interfaces
{
    public interface IAppDataProvider
    {
        public Task<IQueryable<Note>> GetNotesAsync();
        public Task UpdateNoteAsync(Note note);
        public Task InsertNoteAsync(Note note);
        public Task DeleteNoteAsync(Note note);
    }
}
