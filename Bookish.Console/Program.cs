using System;
using System.Collections.Generic;
using System.Linq;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookishClient = new BookishClient();

            string command = args[0];

            switch (command)
            {
                case "books":
                    if (args.Length < 2)
                    {
                        System.Console.Out.WriteLine(
                            "Second argument required for books command. books <query> | books --all");
                        System.Console.Out.WriteLine(
                            "Either enter --all to list all books, or enter a string to search by title or author name.");
                    }
                    else if (args.Length > 2)
                    {
                        System.Console.Out.WriteLine("Too many arguments. books <query> | books --all");
                    }
                    else
                    {
                        List<Book> books;
                        if (args[1] == "--all")
                        {
                            books = bookishClient.FindAllBooks();
                            if (books.Count == 0) System.Console.Out.WriteLine("No books in database.");
                        }
                        else
                        {
                            string searchQuery = args[1];
                            books = bookishClient.SearchBooks(searchQuery);
                            if (books.Count == 0)
                                System.Console.Out.WriteLine($"No books matching query '{searchQuery}' found.");
                        }
                        Book.DisplayListToConsole(books);
                    }
                    
                    break;
                case "book":
                    if (args.Length < 2)
                    {
                        System.Console.Out.WriteLine("Second argument required for book command. book <id>");
                    }
                    else if (args.Length > 2)
                    {
                        System.Console.Out.WriteLine("Too many arguments. book <id>");
                    }
                    else
                    {
                        int bookId = int.Parse(args[1]);
                        Book book = bookishClient.FindBook(bookId);
                        
                        if (book == null) System.Console.Out.WriteLine($"No book matching ID '{bookId}' found.");
                        
                        book.DisplayToConsole();
                    }  
                    
                    break;
                /*case "authors":
                    if (args.Length < 2)
                    {
                        System.Console.Out.WriteLine("Second argument required for authors command. authors <query> | authors --all");
                        System.Console.Out.WriteLine("Either enter --all to list all authors, or enter a string to search by name.");
                        return;
                    }
                    else if (args.Length > 2)
                    {
                        System.Console.Out.WriteLine("Too many arguments. authors <query> | authors --all");
                        return;
                    }
                    else {
                        List<Author> authors;
                        if (args[1] == "--all")
                        {
                            authors = bookishClient.FindAllAuthors();
                            if (authors.Count == 0) System.Console.Out.WriteLine("No authors in database.");
                        }
                        else
                        {
                            string searchQuery = args[1];
                            authors = bookishClient.SearchAuthors(searchQuery);
                            if (authors.Count == 0) System.Console.Out.WriteLine($"No authors matching query '{searchQuery}' found.");
                        }
                        Author.DisplayListToConsole(authors);
                    }
                    
                    break;*/
                case "author":
                    if (args.Length < 2)
                    {
                        System.Console.Out.WriteLine("Second argument required for author command. author <id>");
                    }
                    else if (args.Length > 2)
                    {
                        System.Console.Out.WriteLine("Too many arguments. author <id>");
                    }
                    else
                    {
                        int authorId = int.Parse(args[1]);
                        (Author author, List<Book> books) = bookishClient.FindAuthor(authorId);
                        
                        if (author == null) System.Console.Out.WriteLine($"No author matching ID '{authorId}' found.");
                        
                        author.DisplayToConsole();
                        Book.DisplayListToConsole(books);
                    }  
                    
                    break;
            }
        }
    }
}
