namespace Bookish.Data.Models.Internal
{
    public class Author
    {
        public int Id;
        public string Name;

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}