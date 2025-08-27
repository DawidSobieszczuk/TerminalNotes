namespace DawidSobieszczuk.TerminalNotes.Data.Models
{
    public class Note
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public required List<string> Tags { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
