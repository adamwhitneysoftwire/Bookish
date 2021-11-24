using System.Collections.Generic;

namespace Bookish.Data.Models.Internal
{
    public class Book
    {
        public int Id;
        public string Title;
        public int Isbn;
        public List<Author> Authors;
        public List<BookInstance> Instances;

        public Book(int id, string title, int isbn)
        {
            Id = id;
            Title = title;
            Isbn = isbn;
            Authors = new List<Author>();
            Instances = new List<BookInstance>();
        }

        public void AddAuthor(Author author)
        {
            if (Authors.Exists(a => a == author)) return;
            Authors.Add(author);
        }

        public void AddInstance(BookInstance instance)
        {
            if (Instances.Exists(i => i == instance)) return;
            Instances.Add(instance);
        }
    }
}