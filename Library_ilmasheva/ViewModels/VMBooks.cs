using Microsoft.EntityFrameworkCore.ChangeTracking;
using Library_ilmasheva.Classes;
using Library_ilmasheva.Models;
using Library_ilmasheva.Context;

namespace Library_ilmasheva.ViewModels
{
    public class VMBooks : Notification
    {
        public LibraryContext libraryContext = new LibraryContext();
        public ObservableCollectionListSource<Book> Books { get; set; }

        public VMBooks() =>
            Books = new ObservableCollectionListSource<Book>(libraryContext.Books.OrderBy(x => x.Title));

        public RealyCommand OnAddBook
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Book NewBook = new Book()
                    {
                        YearPublish = DateTime.Now.Year,
                        Title = "Новая книга",
                        Author = "Автор",
                        Genre = "Жанр"
                    };
                    Books.Add(NewBook);
                    libraryContext.Books.Add(NewBook);
                    libraryContext.SaveChanges();
                });
            }
        }
    }
}