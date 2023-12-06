using Newtonsoft.Json;
using LibrarySvalero.Models;
using System.Reflection.Metadata;
using System.Net;
using Microsoft.VisualBasic;
namespace LibrarySvalero.Data
{
    public class AdministratorRepository
    {
        public string pathFile = "./Data/DatabaseAdministrator.json";
        public List<AdministratorModels> userDetails = new List<AdministratorModels>();
        public void insertDetailsUser(AdministratorModels user)
        {
            loadData();
            userDetails.Add(user);
            updateData();
        }
        public void updateData()
        {
            string jsonClientesData = JsonConvert.SerializeObject(userDetails, Formatting.Indented);
            File.WriteAllText(pathFile, jsonClientesData);
        }
        public void loadData()
        {
            string archivo = File.ReadAllText(pathFile);
            userDetails = JsonConvert.DeserializeObject<List<AdministratorModels>>(archivo) ?? new List<AdministratorModels>();
        }
        public AdministratorModels VereficationAccount(List<AdministratorModels> userDetails, string nameUser, string password)
        {
            
            return userDetails.Find(client => client.administratorName == nameUser && client.administratorPassword == password);
        }
        public void LogException(Exception ex)
        {
            string pathFile = "../Data/Exception.json";
            using (StreamWriter writer = new StreamWriter(pathFile, true))
            {
                writer.WriteLine("Fecha: " + DateTime.Now.ToString());
                writer.WriteLine(ex.ToString());
            }
        }
         public List<AdministratorModels> giveListUpdated()
        {
            string archivo = File.ReadAllText(pathFile);
            userDetails = JsonConvert.DeserializeObject<List<AdministratorModels>>(archivo) ?? new List<AdministratorModels>();
            return userDetails;
        }

    }
}