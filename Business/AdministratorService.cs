using LibrarySvalero.Models;
using LibrarySvalero.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibrarySvalero.Business
{
    public class AdministratorService
    {
        AdministratorRepository adminRepo = new AdministratorRepository();
        BooksRepository bookRepo = new BooksRepository();

        public void makeAccount(AdministratorModels userObject)
        {
            try
            {
                adminRepo.insertDetailsUser(userObject);
            }
            catch (Exception ex)
            {
                bookRepo.LogException(ex);
            }
        }

        public AdministratorModels makeObject(string name, string password)
        {
            try
            {
                var user = new AdministratorModels()
                {
                    administratorName = name,
                    administratorPassword = password
                };

                return user;
            }
            catch (Exception ex)
            {
                bookRepo.LogException(ex);
                return null;
            }
        }

        public bool ComprobationName(string name, string password)
        {
            try
            {
                var admin = adminRepo.vereficationAccount(adminRepo.giveListUpdated(), name, password);
                return admin != null;
            }
            catch (Exception ex)
            {
                bookRepo.LogException(ex);
                return false;
            }
        }

        public void createBook(string name, string author, string year, double money, string gender, string description)
        {
            try
            {
                var book = new BooksModels()
                {
                    Title = name,
                    Author = author,
                    Year = year,
                    Money = money,
                    Gender = gender,
                    Description = description
                };
                bookRepo.insertDetailsBook(book);
            }
            catch (Exception ex)
            {
                 bookRepo.LogException(ex);
            }
        }

        public void getInfoClients(List<AccountModels> list)
        {
            try
            {
                foreach (var user in list)
                {
                    Console.WriteLine($"Nombre: {user.clientName}");
                }
            }
            catch (Exception ex)
            {
                 bookRepo.LogException(ex);
            }
        }

        public void getRecomendations(List<RecomendationsModels> list)
        {
            try
            {
                foreach (var recomendations in list)
                {
                    Console.WriteLine($"Nombre: {recomendations.title}");
                }
            }
            catch (Exception ex)
            {
                 bookRepo.LogException(ex);
            }
        }
    }
}
