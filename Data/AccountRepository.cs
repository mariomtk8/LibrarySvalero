using Newtonsoft.Json;
using LibrarySvalero.Models;
using System;
using System.IO;
using System.Collections.Generic;

namespace LibrarySvalero.Data
{
    public class AccountRepository
    {
        AccountModels userModels = new AccountModels();
        List<AccountModels> userDetails = new List<AccountModels>();

    string pathFile = "../Data/DatabaseUser.json";



        public void insertDetailsUser(AccountModels user)
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

        public List<AccountModels> giveListUpdated()
        {
            string archivo = File.ReadAllText(pathFile);
            userDetails = JsonConvert.DeserializeObject<List<AccountModels>>(archivo) ?? new List<AccountModels>();
            return userDetails;
        }

        public void loadData()
        {
        
            if (File.Exists(pathFile))
            {
                string archivo = File.ReadAllText(pathFile);
                userDetails = JsonConvert.DeserializeObject<List<AccountModels>>(archivo) ?? new List<AccountModels>();
            }
            else
            {
                userDetails = new List<AccountModels>();
            }
        }

        public void deleteAccount(string clientName)
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

        public AccountModels verificationAccount(List<AccountModels> userDetails, string nameUser, string password)
        {
            return userDetails.Find(client => client.clientName == nameUser && client.password == password);
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
