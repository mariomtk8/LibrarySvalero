using LibrarySvalero.Data;
using LibrarySvalero.Business;
using LibrarySvalero.Presentation;


Menu menu = new Menu();
AccountService userBusiness = new AccountService();

while (userBusiness.stop == false)
{
    menu.welcomeMenu();
}


