using Library_ilmasheva.Classes;
using Library_ilmasheva.Models;
using Library_ilmasheva.Views;
using System;

namespace Library_ilmasheva.ViewModels
{
    public class VMPages : Notification
    {
        /// <summary>
        /// Модель представления книг
        /// </summary>
        public VMBooks vm_books = new VMBooks();

        public VMPages()
        {
            MainWindow.init.frame.Navigate(new Views.Main(vm_books));
        }

        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.Close();
                });
            }
        }
    }
}