using LibrarySvalero.Models;
using LibrarySvalero.Data;
namespace LibrarySvalero.Business
{

    public class AdministratorService
    {

        AdministratorRepository adminRepo = new AdministratorRepository();
        public void makeAccount(AdministratorModels userObject)
        {
            try
            {

                adminRepo.insertDetailsUser(userObject);
            }
            catch (Exception ex)
            {
                adminRepo.LogException(ex);
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
                adminRepo.LogException(ex);
                return null;
            }
        }

        public bool ComprobationName(string name, string password)
        {
            
            try
            {
                var admin = adminRepo.VereficationAccount(adminRepo.giveListUpdated(), name, password);
                return admin != null;
            }
            catch (Exception ex)
            {
                adminRepo.LogException(ex);
                return false;
            }
        }


    }


}

