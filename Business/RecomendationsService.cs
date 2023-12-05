using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO; // Necesario para la entrada/salida de archivos
using LibrarySvalero.Data;
using LibrarySvalero.Models;

namespace LibrarySvalero.Business
{
    public class RecomendationsService
    {
        RecomendationsRepository recomRepo = new RecomendationsRepository();

        public bool ComprobationName(String name)
        {
            try
            {
                RecomendationsModels user = recomRepo.searchBook(recomRepo.giveListUpdated(), name);
                return true;
            }
            catch (Exception ex)
            {
                recomRepo.LogException(ex);
                return false;
            }
        }

        public RecomendationsModels makeObject(string title, string author, string year, decimal money, string clientname)
        {
            try
            {
                var details = new RecomendationsModels()
                {
                    Title = title,
                    Author = author,
                    Year = year,
                    Money = money,
                    clientName = clientname,
                };

                return details;
            }
            catch (Exception ex)
            {
                recomRepo.LogException(ex);
                return null; 
            }
        }   
    }
}
