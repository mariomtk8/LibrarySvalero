using System;
using System.Collections.Generic;
using System.IO;
using LibrarySvalero.Data;
using LibrarySvalero.Models;

namespace LibrarySvalero.Business
{
    public class BooksService
    {
        AccountRepository userRepository = new AccountRepository();
        BooksRepository RepositoryBooks = new BooksRepository();
        AccountModels userModel = new AccountModels();

     

        public void buyBooks(string choice, string name)
        {
            try
            {
                BooksModels book = RepositoryBooks.searchBooks(choice);
                AccountModels user = userRepository.searchClient(userRepository.giveListUpdated(), name);
                double clientMoney = user.clientMoney;
                int bookMoney = Convert.ToInt32(book.Money);

                if (clientMoney > bookMoney)
                {
                    clientMoney -= bookMoney;
                    user.clientMoney = clientMoney;
                    user.register.Add("Has comprado " + choice + " que ha costado " + book.Money + " este día y la hora: " + DateTime.Now);
                    userRepository.updateData();
                }
            }
            catch (Exception ex)
            {
                RepositoryBooks.LogException(ex);
            }
        }

        public void seeBooksbuy(List<BooksModels> list)
        {
            try
            {
                foreach (var book in list)
                {
                    Console.WriteLine($"Título: {book.Title}");
                }
            }
            catch (Exception ex)
            {
                RepositoryBooks.LogException(ex);
            }
        }
    }
}
