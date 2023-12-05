using Newtonsoft.Json;
using LibrarySvalero.Models;
using System.Reflection.Metadata;
using System.Net;
using Microsoft.VisualBasic;
namespace LibrarySvalero.Data
{
    public class AccountRepository
    {
        AccountModels userModels = new AccountModels();
        List<AccountModels> userDetails = new List<AccountModels>();

        string ruthFile = "../Data/DatabaseUser.json";

        public void insertDetailsUser(AccountModels user)
        {
            loadData();
            userDetails.Add(user);
            updateData();
        }
        public void updateData()
        {
            string jsonClientesData = JsonConvert.SerializeObject(userDetails, Formatting.Indented);
            File.WriteAllText(ruthFile, jsonClientesData);
        }

        public List<AccountModels> giveListUpdated()
        {
            string archivo = File.ReadAllText(ruthFile);
            userDetails = JsonConvert.DeserializeObject<List<AccountModels>>(archivo) ?? new List<AccountModels>();
            return userDetails;
        }



        public void loadData()
        {
            string archivo = File.ReadAllText(ruthFile);
            userDetails = JsonConvert.DeserializeObject<List<AccountModels>>(archivo) ?? new List<AccountModels>();
        }

        public void DeleteAccount(string clientName)
        {
            loadData();
            AccountModels user = searchClient(giveListUpdated(), clientName);
            userDetails.Remove(user);
            updateData();
        }
        public AccountModels searchClient(List<AccountModels> userDetails, string nameUser)
        {

            return userDetails.Find(client => client.clientName == nameUser);
        }

        public AccountModels VereficationAccount(List<AccountModels> userDetails, string nameUser,string password)
        {

            return userDetails.Find(client => client.clientName == nameUser && client.password == password);
        }

        public void LogException(Exception ex)
        {
        string filePath = "../Data/Exception.json";            
        using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Fecha: " + DateTime.Now.ToString());
                writer.WriteLine(ex.ToString());
            }
        }


        

    }
}
