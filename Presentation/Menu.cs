using LibrarySvalero.Data;
using LibrarySvalero.Models;
using LibrarySvalero.Business;
using System;
using System.Reflection;

namespace LibrarySvalero.Presentation
{
    public class Menu
    {
        AccountModels userModel = new AccountModels();
        AccountService userBusiness = new AccountService();
        AccountRepository userRepository = new AccountRepository();
        BooksService booksBusiness = new BooksService();
        BooksRepository booksRepository = new BooksRepository();
        RecomendationsRepository recoRepository = new RecomendationsRepository();
        RecomendationsService recoService = new RecomendationsService();
        AdministratorService adminService = new AdministratorService();
        AdministratorRepository adminRepo = new AdministratorRepository();
        AdministratorModels adminModel = new AdministratorModels();

        bool stop = false;

        public void welcomeMenu()
        {
            int number;
            do
            {
                Console.WriteLine("Hola, Bienvenido a LibrarySvalero");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Tiene diferentes opciones: (pulse el número correspondiente)");
                Console.WriteLine("1 Crear una cuenta");
                Console.WriteLine("2 Entrar en una cuenta");
                Console.WriteLine("3 Ver todos los libros de nuestra tienda");
                Console.WriteLine("4 Para ver excepciones producidas");
                Console.WriteLine("5 Para entrar en el menú de administrador");

                if (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > 5)
                {
                    Console.WriteLine("Lo siento, lo has introducido mal, tienes que introducir 1, 2, 3, 4, 5");
                    Console.WriteLine("");
                }

            } while (number < 1 || number > 5);

            switch (number)
            {
                case 1: { createAccountMenu(); break; }
                case 2: { comprobatioNameMenu(); break; }
                case 3:
                    {
                        Console.WriteLine("Estos son nuestros libros");
                        Console.WriteLine("-------------------------");
                        booksBusiness.seeBooksbuy(booksRepository.getList());
                        break;
                    }
                case 4:
                    {
                        try
                        {
                            StreamReader reader = new StreamReader("./Data/Exception.json");
                            Console.WriteLine(reader);
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Todavía no hay niguna excepción apuntada");
                            welcomeMenu();
                            break;
                        }

                    }
                    case 5: { administratorMenu(); break; }
            }
            
        }

        public void createAccountMenu()
        {
            Console.WriteLine("Has entrado en crear una cuenta");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1 Para volver atrás");
            Console.WriteLine("2 Para crear la cuenta");
            string response = Console.ReadLine();
            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para volver atrás");
                Console.WriteLine("2 Para crear la cuenta");
                response = Console.ReadLine();
            }

            if (number == 1)
            {
                manageAccountMenu();
            }
            else
            {

                Console.WriteLine("Vamos a proceder a rellenar los datos para crear la cuenta");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("Introduce el nombre y apellidos");
                string clientName = Console.ReadLine();
                Console.WriteLine("Introduce tu dirección");
                string clientAddress = Console.ReadLine();
                Console.WriteLine("Introduce tu número de teléfono");
                string clientPhoneNumber = Console.ReadLine();
                Console.WriteLine("Introduce la cantidad con la que quieres iniciar tu cuenta");
                double clientMoney = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Introduce tu número de seguridad");
                string password = Console.ReadLine();
                userBusiness.makeAccount(userBusiness.makeObject(clientName, password, clientAddress, clientPhoneNumber, clientMoney));
                Console.WriteLine("La cuenta se ha creado con éxito");
            }

        }

        public void manageAccountMenu()
        {
            Console.WriteLine("Has entrado en gestionar cuenta");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1 Para cambiar los datos de la cuenta");
            Console.WriteLine("2 Para ingresar dinero");
            Console.WriteLine("3 Para comprar libro");
            Console.WriteLine("4 Para ver tus libros comprados");
            Console.WriteLine("5 Para eliminar tu cuenta");
            Console.WriteLine("6 Para recomendarnos un libro que te gustaría que estuviese en nuestra tienda");
            Console.WriteLine("7 Para terminar las operaciones");
            Console.WriteLine("8 Para volver atrás");

            string response = Console.ReadLine();
            int number;

            while (!int.TryParse(response, out number) && number < 1 || number > 8)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para cambiar los datos de la cuenta");
                Console.WriteLine("2 Para ingresar dinero");
                Console.WriteLine("3 Para comprar libro");
                Console.WriteLine("4 Para ver tus libros comprados");
                Console.WriteLine("5 Para eliminar tu cuenta");
                Console.WriteLine("6 Para recomendarnos un libro que te gustaría que estuviese en nuestra tienda");
                Console.WriteLine("7 Para terminar las operaciones");
                Console.WriteLine("8 Para volver atrás");
                response = Console.ReadLine();
            }

            switch (number)
            {
                case 1: { changeDetailsMenu(); break; }
                case 2: { makeDepositMenu(); break; }
                case 3: { buyBookMenu(); break; }
                case 4:
                    {
                        booksBusiness.seeBooksbuy(booksRepository.getList());

                        Console.WriteLine("Seras devuelto al menu de gestionar cuenta");
                        break;
                    }
                case 5: { DeleteAccountMenu(); break; }
                case 6: { RecomendationsMenu(); break; }
                case 7: { userBusiness.stop = true; break; }
                case 8: { welcomeMenu(); break; }
            }

        }

        public void changeDetailsMenu()
        {
            Console.WriteLine("Has entrado en cambiar datos de la cuenta");
            Console.WriteLine("¿Qué quieres cambiar?");
            Console.WriteLine("--------------------");
            Console.WriteLine("1 El nombre del dueño de la cuenta");
            Console.WriteLine("2 La dirección");
            Console.WriteLine("3 El número de teléfono");
            Console.WriteLine("4 La contraseña");
            Console.WriteLine("5 Para volver al menú de gestionar cuenta");
            int number;

            if (int.TryParse(Console.ReadLine(), out number) && number >= 1 && number <= 4)
            {
                switch (number)
                {
                    case 1:
                        {
                            Console.WriteLine("Cual es el nuevo nombre");
                            string parameter = Console.ReadLine();
                            userBusiness.changeDetails(number, userModel.clientName, parameter);
                            Console.WriteLine("El cambio se ha realizado con éxito");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Cual es la nueva dirección");
                            string parameter = Console.ReadLine();
                            userBusiness.changeDetails(number, userModel.clientName, parameter);
                            Console.WriteLine("El cambio se ha realizado con éxito");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Cual es el nuevo número de teléfono");
                            string parameter = Console.ReadLine();
                            userBusiness.changeDetails(number, userModel.clientName, parameter);
                            Console.WriteLine("El cambio se ha realizado con éxito");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Cual es la nueva contraseña");
                            string parameter = Console.ReadLine();
                            userBusiness.changeDetails(number, userModel.clientName, parameter);
                            Console.WriteLine("El cambio se ha realizado con éxito");
                            break;
                        }
                    case 5:
                        {
                            manageAccountMenu();
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("Te has equivocado, vas a ser devuelto al inicio del menú");
                changeDetailsMenu();
            }
        }

        public void makeDepositMenu()
        {
            Console.WriteLine("Has entrado en hacer un ingreso");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1 Para hacer el ingreso");
            Console.WriteLine("2 Para volver atrás");
            string response = Console.ReadLine();

            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para hacer el ingreso");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
            }
            if (number == 1)
            {
                Console.WriteLine("¿De cuánto quieres hacer el ingreso?");
                string selection = Console.ReadLine();
                double iTransferDeposit;
                while (!double.TryParse(selection, out iTransferDeposit) || iTransferDeposit < 1)
                {
                    Console.WriteLine("Has introducido un número negativo o no válido. Introduce una cantidad válida:");
                    selection = Console.ReadLine();
                }

                userBusiness.makeDeposit(iTransferDeposit, userModel.clientName);
                Console.WriteLine("El ingreso se ha realizado correctamente");
            }
            else
            {
                manageAccountMenu();
            }


        }

        public void buyBookMenu()
        {
            Console.WriteLine("Has entrado a la sección de comprar un libro");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("1 Para continuar comprando el libro el libro");
            Console.WriteLine("2 Para volver atrás");
            string response = Console.ReadLine();
            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para continuar comprando el libro el libro");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
            }

            Console.WriteLine("Estos son nuestros libros");
            Console.WriteLine("-------------------------");
            booksBusiness.seeBooksbuy(booksRepository.getList());
            Console.WriteLine("¿Qué libro quieres comprar?");
            string choice = Console.ReadLine();
            BooksModels book = booksRepository.searchBooks(choice);
            Console.WriteLine("¿Estás seguro de que quieres este libro?, este libro cuesta " + book.Money.ToString() + " euros.");
            Console.WriteLine("Escribe si, si quieres comprarlo");
            if (Console.ReadLine() == "si" || Console.ReadLine() == "sí" || Console.ReadLine() == "Si" || Console.ReadLine() == "Sí")
            {
                try
                {
                    booksBusiness.buyBooks(choice, userModel.clientName);
                    manageAccountMenu();
                }
                catch
                {
                    AccountModels user = userRepository.searchClient(userRepository.giveListUpdated(), userModel.clientName);

                    Console.WriteLine("Lo siento, no se ha podido realizar bien la compra");
                    Console.WriteLine("Comprueba que hayas introducido bien el nombre o tengas dinero para poder comprarlo");
                    Console.WriteLine("-----------------------------------------------------------------------------");
                    Console.WriteLine("1 Para volver a comprar un libro");
                    Console.WriteLine("2 Para ver cuánto dinero tienes");
                    Console.WriteLine("3 Para volver al menú de gestionar cuenta");
                    Console.WriteLine("4 Para terminar cuenta");
                    int optionMenu = Convert.ToInt32(Console.ReadLine());
                    switch (optionMenu)
                    {
                        case 1:
                            {
                                buyBookMenu();
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Tienes " + user.clientMoney.ToString());
                                Console.WriteLine("Vas a ser devuelto al inicio de comprar un libro");
                                buyBookMenu();
                                break;
                            }
                        case 3:
                            {
                                manageAccountMenu();
                                break;
                            }
                        case 4:
                            {
                                userBusiness.stop = true;
                                Console.WriteLine("Las operaciones han terminado");
                                break;
                            }
                    }
                }
            }
        }

        public void comprobatioNameMenu()
        {
            Console.WriteLine("Has entrado en la parte para meter tu nombre y contraseña");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("1 Para volver atrás");
            Console.WriteLine("2 Para poner el nombre y la contraseña");
            string response = Console.ReadLine();
            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para volver atrás");
                Console.WriteLine("2 Para poner el nombre y la contraseña");
                response = Console.ReadLine();
            }
            if(number == 1){
                welcomeMenu();
            }

            Console.WriteLine("Introduce tu nombre para poder entrar en la cuenta");
            string name = Console.ReadLine();
            Console.WriteLine("Ahora pon la contraseña");
            string password = Console.ReadLine();

            bool exist = userBusiness.ComprobationName(name, password);
            if (exist)
            {
                userModel.clientName = name;
                manageAccountMenu();
            }
            else
            {
                Console.WriteLine("Los datos que has puesto no están en nuestra base de datos");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("1 Para volver a introducir el nombre");
                Console.WriteLine("2 Para terminar las operaciones");
                Console.WriteLine("3 Para crear una cuenta");
                Console.WriteLine("4 Para volver atrás");
                response = Console.ReadLine();

                while (!int.TryParse(response, out number) || number < 1 || number > 4)
                {
                    Console.WriteLine("1 Para volver a introducir el nombre");
                    Console.WriteLine("2 Para terminar las operaciones");
                    Console.WriteLine("3 Para crear una cuenta");
                    Console.WriteLine("4 Para volver atrás");
                    response = Console.ReadLine();
                }
                switch (number)
                {
                    case 1: { comprobatioNameMenu(); break; }
                    case 2: { userBusiness.stop = true; Console.WriteLine("Las operaciones han terminado"); break; }
                    case 3: { createAccountMenu(); break; }
                    case 4: { welcomeMenu(); break; }
                }
            }
        }

        public void DeleteAccountMenu()
        {
            Console.WriteLine("Has entrado a la sección de eliminar la cuenta");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Escribe 1 si quieres borrar la cuenta");
            Console.WriteLine("Escribe 2 volver atrás");
            int number;
            string selection = Console.ReadLine();
            while (!int.TryParse(selection, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Lo siento tienes que");
                Console.WriteLine("Escribe 1 si quieres borrar la cuenta");
                Console.WriteLine("Escribe 2 volver atrás");
                selection = Console.ReadLine();
            }

            if (number == 1)
            {
                userRepository.deleteAccount(userModel.clientName);
                Console.WriteLine("La cuenta se ha eliminado");
            }
            if (number == 2)
            {
                manageAccountMenu();
            }
        }

        public void RecomendationsMenu()
        {
            Console.WriteLine("Has entrado a la sección de recomendar un libro");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Escribe 1 si quieres recomendar un libro ");
            Console.WriteLine("Escribe 2 volver atrás");
            int number;
            string selection = Console.ReadLine();
            while (!int.TryParse(selection, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Lo siento tienes que");
                Console.WriteLine("Escribe 1 si quieres recomendar un libro ");
                Console.WriteLine("Escribe 2 volver atrás");
                selection = Console.ReadLine();
            }

            Console.WriteLine("¿Qué libro te gustaría que vendiéramos?");
            string title = Console.ReadLine();
            Console.WriteLine("Pon los siguientes datos del libro");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("El nombre del autor");
            string author = Console.ReadLine();
            Console.WriteLine("El año de su publicación");
            string year = Console.ReadLine();
            Console.WriteLine("Cuánto cuesta el libro");
            double money;
            Console.WriteLine("¿A qué género corresponde este libro?");
            string gender = Console.ReadLine();
            Console.WriteLine("Haz una breve descripción de el libro");
            string description = Console.ReadLine();
            while (!double.TryParse(Console.ReadLine(), out money) || money < 0)
            {
                Console.WriteLine("Has introducido un valor no válido. Introduce una cantidad válida:");
            }
            recoRepository.insertDetails(recoService.makeObject(title, author, year, money, userModel.clientName, gender, description));
        }
        public void administratorMenu()//parte nueva
        {
            Console.WriteLine("Bienvenido a la parte del administrador");
            Console.WriteLine("1 Para crear una cuenta de administrador");
            Console.WriteLine("2 Para entrar en la cuenta");
            Console.WriteLine("3 Para volver atrás");
            string response = Console.ReadLine();
            int number;
            while (!int.TryParse(response, out number) || number < 1 || number > 4)
            {
                Console.WriteLine("Lo siento tienes que");
                Console.WriteLine("1 Para crear una cuenta de administrador");
                Console.WriteLine("2 Para entrar en la cuenta");
                Console.WriteLine("3 Para volver atrás");
                response = Console.ReadLine();
            }

            switch (number)
            {
                case 1: { CreateAdministratorMenu(); break; }
                case 2: { comprobatioAdministratorName(); break; }
                case 3: { welcomeMenu(); break; }
            }
        }
        public void CreateAdministratorMenu()
        {
            Console.WriteLine("Bienvenido al menu para crear cuenta de administrador");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Escribe 1 para crear una cunta");
            Console.WriteLine("Escribe 2 para cancelar y volver atras");
            string response = Console.ReadLine();
            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 3)
            {
                Console.WriteLine("Escribe 1 para crear una cunta");
                Console.WriteLine("Escribe 2 para cancelar y volver atras");
                response = Console.ReadLine();
            }
            switch (number)
            {
                case 1:
                    {
                        Console.WriteLine("Introduce el nombre de tu usuario Administrador");
                        string name = Console.ReadLine();
                        Console.WriteLine("Introduce la contraseña de tu usuario Administrador");
                        string password = Console.ReadLine();
                        adminService.makeAccount(adminService.makeObject(name, password));
                        break;
                    }

                case 2: { administratorMenu(); break; }
            }

        }

        public void comprobatioAdministratorName()
        {
            Console.WriteLine("Has entrado en la parte para meter tu nombre y contraseña");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("1 Para volver atrás");
            Console.WriteLine("2 Para poner el nombre y la contraseña");
            string response = Console.ReadLine();
            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para volver atrás");
                Console.WriteLine("2 Para poner el nombre y la contraseña");
                response = Console.ReadLine();
            }

            Console.WriteLine("Introduce tu nombre para poder entrar en la cuenta");
            string name = Console.ReadLine();
            Console.WriteLine("Ahora pon la contraseña");
            string password = Console.ReadLine();

            bool exist = adminService.ComprobationName(name, password);
            if (exist)
            {
                adminModel.administratorName = name;
                manageAdministratorMenu();
            }
            else
            {
                Console.WriteLine("Los datos que has puesto no están en nuestra base de datos");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("1 Para volver a introducir el nombre");
                Console.WriteLine("2 Para terminar las operaciones");
                Console.WriteLine("3 Para crear una cuenta");
                Console.WriteLine("4 Para volver atrás");
                response = Console.ReadLine();

                while (!int.TryParse(response, out number) || number < 1 || number > 4)
                {
                    Console.WriteLine("1 Para volver a introducir el nombre");
                    Console.WriteLine("2 Para terminar las operaciones");
                    Console.WriteLine("3 Para crear una cuenta");
                    Console.WriteLine("4 Para volver atrás");
                    response = Console.ReadLine();
                }
                switch (number)
                {
                    case 1: { comprobatioAdministratorName(); break; }
                    case 2: { userBusiness.stop = true; Console.WriteLine("Las operaciones han terminado"); break; }
                    case 3: { CreateAdministratorMenu(); break; }
                    case 4: { welcomeMenu(); break; }
                }
            }
        }

        public void manageAdministratorMenu()
        {
            Console.WriteLine("Bienvenido al menú del administrador");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1 Para añadir un libro");
            Console.WriteLine("2 Para ver los  usuarios registrados");
            Console.WriteLine("3 Para ver las recomendaciones hechas por los usuarios");
            Console.WriteLine("4 Para borrar un libro");
            Console.WriteLine("5 Para volver atrás");
            Console.WriteLine("6 Para volver al menú principal");
            string response = Console.ReadLine();

            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 6)
            {
                Console.WriteLine("Te has equivocado tienes que poner entre 1 y 6");
                Console.WriteLine("1 Para añadir un libro");
                Console.WriteLine("2 Para ver los  usuarios registrados");
                Console.WriteLine("3 Para ver las recomendaciones hechas por los usuarios");
                Console.WriteLine("4 Para borrar un libro");
                Console.WriteLine("5 Para volver atrás");
                Console.WriteLine("6 Para volver al menú principal");
                response = Console.ReadLine();
            }
            switch (number)
            {
                case 1: { createBookMenu(); break; }
                case 2: { seeUsersMenu(); break; }
                case 3: { seeRecomendatiosMenu(); break; }
                case 4: { deleteBookMenu(); break; }
                case 5: { administratorMenu(); break; }
                case 6: { welcomeMenu(); break; }
            }
        }

        public void createBookMenu()
        {
            Console.WriteLine("Bienvenido al menú de añadir un libro");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1 Para añadir un libro");
            Console.WriteLine("2 Para volver atrás");
            string response = Console.ReadLine();

            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para añadir un libro");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
            }

            if (number == 1)
            {
                Console.WriteLine("Dime el nombre del libro");
                string nameBook = Console.ReadLine();
                Console.WriteLine("Dime el nombre del autor o autora del libro");
                string nameAuthor = Console.ReadLine();
                Console.WriteLine("Dime el año en el que libro fue publicado");
                string yearBook = Console.ReadLine();
                Console.WriteLine("Dime cuanto quieres que cueste el libro");
                double moneyBook = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Dime el género del libro");
                string genderBook = Console.ReadLine();
                Console.WriteLine("Dime la descripción que quieres que tenga este libro");
                string descriptionBook = Console.ReadLine();
                adminService.createBook(nameBook, nameAuthor, yearBook, moneyBook, genderBook, descriptionBook);
                Console.WriteLine("El libro se ha creado correctamente");
                manageAdministratorMenu();
            }
            else
            {
                administratorMenu();
            }
        }

        public void seeUsersMenu()
        {
            Console.WriteLine("Bienvenido a ver usuarios");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1 Para ver los usuarios");
            Console.WriteLine("2 Para volver atrás");
            string response = Console.ReadLine();

            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para ver los usuarios");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
            }

            if (number == 1)
            {
                adminService.getInfoClients(userRepository.giveListUpdated());
                Console.WriteLine("1 Para ver los datos de un usuario");
                Console.WriteLine("2 Para volver al menú de admisnistrador");
                response = Console.ReadLine();

                while (!int.TryParse(response, out number) || number < 1 || number > 2)
                {
                    Console.WriteLine("Te has equivocado");
                    Console.WriteLine("1 Para ver los usuarios");
                    Console.WriteLine("2 Para volver atrás");
                    response = Console.ReadLine();
                }
                if (number == 1)
                {
                    Console.WriteLine("Escribe el nombre del usuario que quieras saber más");
                    string name = Console.ReadLine();

                    AccountModels user = userRepository.searchClient(userRepository.giveListUpdated(), name);

                    while (user == null)
                    {
                        Console.WriteLine("No tenemos ningún usuario con ese nombre, escribelo de nuevo");
                        name = Console.ReadLine();
                        user = userRepository.searchClient(userRepository.giveListUpdated(), name);
                    }

                    Console.WriteLine("Estos son los datos");
                    Console.WriteLine("El nombre del usuario es " + user.clientName);
                    Console.WriteLine("Su dirección es " + user.clientAdress);
                    Console.WriteLine("Su número de teléfono es " + user.clientPhoneNumber);
                    Console.WriteLine("Tiene " + user.clientMoney.ToString() + " euros");
                    manageAdministratorMenu();
                }
            }
            else
            {
                manageAdministratorMenu();
            }
        }

        public void seeRecomendatiosMenu()
        {
            Console.WriteLine("Bienvenido a ver las recomendaciones que hacen los usuarios");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1 Para ver las recomendaciones");
            Console.WriteLine("2 Para volver atrás");
            string response = Console.ReadLine();

            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para ver las recomendaciones");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
            }

            if (number == 1)
            {
                Console.WriteLine("Estas son las recomendaciones");
                Console.WriteLine("-----------------------------");
                adminService.getRecomendations(recoRepository.giveListUpdated());
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("1 Para ver más detalles de alguna recomendación");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
                while (!int.TryParse(response, out number) || number < 1 || number > 2)
                {
                    Console.WriteLine("Te has equivocado");
                    Console.WriteLine("1 Para ver más detalles de alguna recomendación");
                    Console.WriteLine("2 Para volver atrás");
                    response = Console.ReadLine();
                }
                if (number == 1)
                {
                    Console.WriteLine("Escribe la recomendación de la que quieras saber más");
                    string name = Console.ReadLine();

                    RecomendationsModels book = adminRepo.searchBooks(name, recoRepository.getList());

                    while (book == null)
                    {
                        Console.WriteLine("No tenemos ninguna recomendación con ese nombre, escribelo de nuevo");
                        name = Console.ReadLine();
                        book = adminRepo.searchBooks(name, recoRepository.getList());
                    }
                    Console.WriteLine("Estos son los datos");
                    Console.WriteLine("El nombre del libro es " + book.title);
                    Console.WriteLine("Su autor es " + book.author);
                    Console.WriteLine("El año en el que fue publicado es " + book.year);
                    Console.WriteLine("El libro cuesta " + book.money.ToString() + " euros");
                    Console.WriteLine("El género del libro es " + book.gender);
                    Console.WriteLine("La descripción del libro es " + book.description);
                    Console.WriteLine("El nombre del usuario que lo ha recomendado es " + book.clientName);
                    Console.WriteLine(book.list);
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine("1 Para crear un libro a partir de esta recomendación");
                    Console.WriteLine("2 Para volver atrás");
                    response = Console.ReadLine();

                    while (!int.TryParse(response, out number) || number < 1 || number > 2)
                    {
                        Console.WriteLine("Te has equivocado");
                        Console.WriteLine("1 Para crear un libro a partir de esta recomendación");
                        Console.WriteLine("2 Para volver atrás");
                        response = Console.ReadLine();
                    }

                    if (number == 1)
                    {
                        adminService.createBook(book.title, book.author, book.year, book.money, book.gender, book.description);
                        Console.WriteLine("El libro se ha creado correctamente");
                        manageAdministratorMenu();
                    }
                    else
                    {
                        manageAdministratorMenu();
                    }
                }
                else
                {
                    manageAdministratorMenu();
                }
            }
            else
            {
                manageAdministratorMenu();
            }
        }

        public void deleteBookMenu()
        {
            Console.WriteLine("Bienvenido a la parte de borrar un libro");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("1 Para borrar un libro");
            Console.WriteLine("2 Para volver atrás");
            string response = Console.ReadLine();

            int number;

            while (!int.TryParse(response, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Te has equivocado");
                Console.WriteLine("1 Para borrar un libro");
                Console.WriteLine("2 Para volver atrás");
                response = Console.ReadLine();
            }

            if (number == 1)
            {
                Console.WriteLine("Estos son nuestros libros");
                Console.WriteLine("-------------------------");
                booksBusiness.seeBooksbuy(booksRepository.getList());
                
                Console.WriteLine("Escribe el nombre del libro que quieras borrar");
                string name = Console.ReadLine();
                BooksModels book = booksRepository.searchBooks(name);
                while (book == null)
                {
                    Console.WriteLine("No tenemos ningún libro con ese nombre, escribelo de nuevo");
                    name = Console.ReadLine();
                    book = booksRepository.searchBooks(name);
                }
                booksRepository.deleteBook(name);
                Console.WriteLine("El libro se ha borrado correctamente");
                manageAdministratorMenu();
            }
            else
            {
                manageAdministratorMenu();
            }

        }
    }
}
