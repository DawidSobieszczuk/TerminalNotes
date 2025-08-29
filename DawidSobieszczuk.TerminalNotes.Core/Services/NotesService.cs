using DawidSobieszczuk.TerminalNotes.Data.Interfaces;
using DawidSobieszczuk.TerminalNotes.Data.Models;
using System.Diagnostics;

namespace DawidSobieszczuk.TerminalNotes.Core.Services
{
    public class NotesService(IAppDataProvider appDataProvider)
    {
        public async Task AddNote(string content, List<string>? tags = null)
        {
            var note = new Note
            {
                Id = 0,
                Content = content,
                Tags = tags,
                CreatedAt = DateTime.UtcNow,
            };
            await appDataProvider.InsertNoteAsync(note);
        }
    }
}
