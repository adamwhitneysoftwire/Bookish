using Bookish.Data.Models.Internal;

namespace Bookish.Data.Models.Database
{
    public class AuthorWrapper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public long BookIsbn { get; set; }
        
        public Author ToAuthor()
        {
            return new Author(Id, Name);
        }
        
        public Book ToBook()
        {
            return new Book(BookId, BookTitle, BookIsbn);
        }
    }
}