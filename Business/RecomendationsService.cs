using System;
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

        public RecomendationsModels makeObject(string title, string author, string year, double money, string clientname, string gender, string description)
        {
            try
            {
                var details = new RecomendationsModels()
                {
                    title = title,
                    author = author,
                    gender = gender,
                    description = description,
                    year = year,
                    money = money,
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
