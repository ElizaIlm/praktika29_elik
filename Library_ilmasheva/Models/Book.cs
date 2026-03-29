using Library_ilmasheva.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace Library_ilmasheva.Models
{
    public class Book : Notification
    {
        public int Id { get; set; }

        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                Match match = Regex.Match(value, "^.{1,100}$"); // До 100 символов для названия
                if (!match.Success)
                    MessageBox.Show("Название не должно быть пустым и не более 100 символов.", "Некорректный ввод");
                else
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                Match match = Regex.Match(value, "^.{1,50}$"); // До 50 символов для автора
                if (!match.Success)
                    MessageBox.Show("Автор не должен быть пустым и не более 50 символов.", "Некорректный ввод");
                else
                {
                    author = value;
                    OnPropertyChanged("Author");
                }
            }
        }

        private int yearPublish;
        public int YearPublish
        {
            get { return yearPublish; }
            set
            {
                // Простая проверка: год не меньше 1000 и не больше текущего + 1
                if (value < 1000 || value > DateTime.Now.Year + 1)
                    MessageBox.Show("Год издания некорректен.", "Некорректный ввод");
                else
                {
                    yearPublish = value;
                    OnPropertyChanged("YearPublish");
                }
            }
        }

        private string genre;
        public string Genre
        {
            get { return genre; }
            set
            {
                Match match = Regex.Match(value, "^.{1,30}$");
                if (!match.Success)
                    MessageBox.Show("Жанр не должен быть пустым и не более 30 символов.", "Некорректный ввод");
                else
                {
                    genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        private bool isRead;
        public bool IsRead
        {
            get { return isRead; }
            set
            {
                isRead = value;
                OnPropertyChanged("IsRead");
                OnPropertyChanged("IsReadText");
            }
        }

        [Schema.NotMapped]
        private bool isEnable;
        [Schema.NotMapped]
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [Schema.NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";
            }
        }

        [Schema.NotMapped]
        public string IsReadText
        {
            get
            {
                if (IsEnable) return "Не прочитано";
                else return "Прочитано";
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsEnable = !IsEnable;
                    if (!IsEnable)
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_books.libraryContext.SaveChanges();
                });
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить книгу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_books.Books.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_books.libraryContext.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VMPages).vm_books.libraryContext.SaveChanges();
                    }
                });
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnRead
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsRead = !IsRead;
                });
            }
        }
    }
}