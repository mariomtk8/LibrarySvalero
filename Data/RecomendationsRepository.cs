using Newtonsoft.Json;
using LibrarySvalero.Models;
using System.Reflection.Metadata;
using System.Net;
using Microsoft.VisualBasic;
namespace LibrarySvalero.Data
{
    public class RecomendationsRepository
    {
        AccountModels userModels = new AccountModels();
        List<RecomendationsModels> listRecomendations = new List<RecomendationsModels>();

        string pathFile = "../Data/DatabaseRecomendations.json";

        public void insertDetails(RecomendationsModels user)
        {
            loadData();
            user.list.Add("La sugerencia se ha realiazo el " + DateTime.Now);
            listRecomendations.Add(user);
            updateData();
        }
        public void updateData()
        {
            string jsonClientesData = JsonConvert.SerializeObject(listRecomendations, Formatting.Indented);
            File.WriteAllText(pathFile, jsonClientesData);
        }

        public List<RecomendationsModels> giveListUpdated()
        {
            string archivo = File.ReadAllText(pathFile);
            listRecomendations = JsonConvert.DeserializeObject<List<RecomendationsModels>>(archivo) ?? new List<RecomendationsModels>();
            return listRecomendations;
        }




        public void loadData()
        {
            string archivo = File.ReadAllText(pathFile);
            listRecomendations = JsonConvert.DeserializeObject<List<RecomendationsModels>>(archivo) ?? new List<RecomendationsModels>();
        }


        public RecomendationsModels searchBook(List<RecomendationsModels> recomendations, string nameBook)
        {

            return recomendations.Find(title => title.title == nameBook);
        }
       
         public List<RecomendationsModels> getList()
        {
            loadData();
            return listRecomendations;
        }
        public void LogException(Exception ex)
        {
        string path = "../Data/Exception.json";            
        using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("Fecha: " + DateTime.Now.ToString());
                writer.WriteLine(ex.ToString());
            }
        }

    }
}
