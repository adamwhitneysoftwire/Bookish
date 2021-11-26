using Bookish.Data.Models.Internal;

namespace Bookish.Data.Models.Database
{
    public class BookWrapper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Isbn { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public Book ToBook()
        {
            return new Book(Id, Title, Isbn);
        }
        
        public Author ToAuthor()
        {
            return new Author(AuthorId, AuthorName);
        }
    }
}