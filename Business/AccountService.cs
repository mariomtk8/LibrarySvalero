using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using LibrarySvalero.Data;
using LibrarySvalero.Models;

namespace LibrarySvalero.Business
{
    public class AccountService
    {
        AccountRepository userRepository = new AccountRepository();
        AccountModels userAccount = new AccountModels();
        AccountModels userModel = new AccountModels();

        public bool stop = false;

        public void makeAccount(AccountModels userObject)
        {
            try
            {
                userObject.register.Add("Se ha creado la cuenta el día " + DateTime.Now);
                userRepository.insertDetailsUser(userObject);
            }
            catch (Exception ex)
            {
                userRepository.LogException(ex);
            }
        }

        public void changeDetails(int value, String name, string parameter)
        {
            try
            {
                AccountModels user = userRepository.searchClient(userRepository.giveListUpdated(), name);

                switch (value)
                {
                    case 1:
                        user.register.Add("Se ha cambiado el nombre de la cuenta de " + user.clientName + " a " + parameter + " el " + DateTime.Now);
                        user.clientName = parameter;
                        break;
                    case 2:
                        user.register.Add("Se ha cambiado la dirección de la cuenta de " + user.clientAdress + " a " + parameter + " el " + DateTime.Now);
                        user.clientAdress = parameter;
                        break;
                    case 3:
                        user.register.Add("Se ha cambiado el número de teléfono de la cuenta de " + user.clientPhoneNumber + " a " + parameter + " el " + DateTime.Now);
                        user.clientPhoneNumber = parameter;
                        break;
                    case 4:
                        user.register.Add("Se ha cambiado la contraseña de la cuenta de " + user.password + " a " + parameter + " el " + DateTime.Now);
                        user.password = parameter;
                        break;
                }
                userRepository.updateData();
            }
            catch (Exception ex)
            {
                userRepository.LogException(ex);
            }
        }

        public void makeDeposit(decimal value, string name)
        {
            try
            {
                AccountModels user = userRepository.searchClient(userRepository.giveListUpdated(), name);
                user.clientMoney += value;
                user.register.Add("Se ha hecho un ingreso de " + value.ToString() + " el " + DateTime.Now);
                userRepository.updateData();
            }
            catch (Exception ex)
            {
                userRepository.LogException(ex);
            }
        }

        public bool ComprobationName(string name, string password)
        {
            try
            {
                AccountModels user = userRepository.VereficationAccount(userRepository.giveListUpdated(), name, password);
                if(user == null){
                    return false;
                }else{
                    return true;
                }
            }
            catch (Exception ex)
            {
                userRepository.LogException(ex);
                return false;
            }
        }

        public AccountModels makeObject(string name, string password, string adress, string phone, decimal money)
        {
            try
            {
                List<string> lista = new List<string>();
                var user = new AccountModels()
                {
                    clientName = name,
                    password = password,
                    clientAdress = adress,
                    clientPhoneNumber = phone,
                    clientMoney = money,
                    register = lista
                };

                return user;
            }
            catch (Exception ex)
            {
                userRepository.LogException(ex);
                return null;
            }
        }

    }
}