using LibrarySvalero.Data;
using LibrarySvalero.Models;
using LibrarySvalero.Business;
using System;

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

                if (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > 4)
                {
                    Console.WriteLine("Lo siento, lo has introducido mal, tienes que introducir 1, 2, 3, 4");
                    Console.WriteLine("");
                }

            } while (number < 1 || number > 4);

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
                    try{
                        StreamReader reader = new StreamReader("./Data/Exception.json");
                        Console.WriteLine(reader);
                        break;        
                    }catch{
                        Console.WriteLine("Todavía no hay niguna excepción apuntada");
                        welcomeMenu();
                        break;
                    }

                 }
            }
            welcomeMenu();
        }

        public void createAccountMenu()
        {
            Console.WriteLine("Has entrado en crear una cuenta");
            Console.WriteLine("Si te has equivocado y quieres volver hacía atrás escribe si y si quieres continuar pulsa el enter");
            string exit = Console.ReadLine();
            if (exit == "si" || exit == "sí" || exit == "Si" || exit == "Sí")
            {
                welcomeMenu();
            }
            else
            {
                Console.WriteLine("Vamos a proceder a rellenar los datos para crear la cuenta");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("Introduce el nombre y apellidos");
                string clientName = Console.ReadLine();
                Console.WriteLine("Introduce tu dirección");
                string clientAddress = Console.ReadLine();
                Console.WriteLine("Introduce tu número de teléfono");
                string clientPhoneNumber = Console.ReadLine();
                Console.WriteLine("Introduce la cantidad con la que quieres iniciar tu cuenta");
                int clientMoney = Convert.ToInt32(Console.ReadLine());
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
            int number = Convert.ToInt32(Console.ReadLine());
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
                default:
                    {
                        Console.WriteLine("Te has equivocado");
                        manageAccountMenu();
                        break;
                    }
            }

        }

        public void changeDetailsMenu()
        {
            Console.WriteLine("Has entrado en cambiar datos de la cuenta");
            Console.WriteLine("------------------------------------------");
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
            Console.WriteLine("¿De cuánto quieres hacer el ingreso?");
            int iTransferDeposit;
            while (!int.TryParse(Console.ReadLine(), out iTransferDeposit) || iTransferDeposit < 1)
            {
                Console.WriteLine("Has introducido un número negativo o no válido. Introduce una cantidad válida:");
            }

            userBusiness.makeDeposit(iTransferDeposit, userModel.clientName);
            Console.WriteLine("El ingreso se ha realizado correctamente");
        }

        public void buyBookMenu()
        {
            Console.WriteLine("Has entrado a la sección de comprar un libro");
            Console.WriteLine("Si te has equivocado y quieres volver hacía atrás escribe si y si quieres continuar pulsa el enter");
            string exit = Console.ReadLine();
            if (exit == "si" || exit == "sí" || exit == "Si" || exit == "Sí")
            {
                manageAccountMenu();
            }
            else
            {
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
                else
                {
                    int number;
                    while (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > 3)
                    {
                        Console.WriteLine("Opciones");
                        Console.WriteLine("1 Para volver al menú para comprar un libro");
                        Console.WriteLine("2 Para volver al menú de gestionar cuenta");
                        Console.WriteLine("3 Para terminar las operaciones");
                    }
                    switch (number)
                    {
                        case 1: { buyBookMenu(); break; }
                        case 2: { manageAccountMenu(); break; }
                        case 3:
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
            Console.WriteLine("Introduce tu nombre para poder entrar en la cuenta");
            string name = Console.ReadLine();
            Console.WriteLine("Ahora pon la contraseña");
            string password = Console.ReadLine();
            bool prove = userBusiness.ComprobationName(name, password);
            if (prove)
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
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1: { comprobatioNameMenu(); break; }
                    case 2: { userBusiness.stop = true; Console.WriteLine("Las operaciones han terminado"); break; }
                    case 3: { createAccountMenu(); break; }
                    default:
                        {
                            while (option < 1 || option > 3)
                            {
                                Console.WriteLine("Te has equivocado, tienes que introducir 1, 2 o 3");
                                Console.WriteLine("¿Quieres volver a intentar 1 meter el nombre, 2 acabar las operaciones o 3 crear una cuenta?");
                                option = Convert.ToInt32(Console.ReadLine());
                            }
                            switch (option)
                            {
                                case 1: { comprobatioNameMenu(); break; }
                                case 2: { userBusiness.stop = true; Console.WriteLine("Las operaciones han terminado"); break; }
                                case 3: { createAccountMenu(); break; }
                            }
                            break;
                        }
                }
            }
        }

        public void DeleteAccountMenu()
        {
            Console.WriteLine("Has entrado a la sección de eliminar la cuenta");
            Console.WriteLine("Si te has equivocado y quieres volver hacía atrás escribe si y si quieres continuar pulsa el enter");
            string exit = Console.ReadLine();
            if (exit == "si" || exit == "sí" || exit == "Si" || exit == "Sí")
            {
                manageAccountMenu();
            }
            Console.WriteLine("¿Estás seguro de que quieres eliminar la cuenta?");
            Console.WriteLine("1 Para eliminar");
            Console.WriteLine("2 Para volver al menú de gestionar cuenta");
            int number;
            string selection = Console.ReadLine();
            while (!int.TryParse(selection, out number) || number < 1 || number > 2)
            {
                Console.WriteLine("Lo siento tienes que");
                Console.WriteLine("1 Para eliminar la cuenta");
                Console.WriteLine("2 Para volver al menú de gestionar cuenta");
                Console.WriteLine("3 Para terminar las operaciones");
                selection = Console.ReadLine();
            }
            if (number == 1)
            {
                userRepository.DeleteAccount(userModel.clientName);
                Console.WriteLine("La cuenta se ha eliminado");
                Console.WriteLine("Vas a ser devuelto al menú principal");
            }
            if (number == 2)
            {
                manageAccountMenu();
            }
        }

        public void RecomendationsMenu()
        {
            Console.WriteLine("Has entrado a la sección de recomendar un libro");
            Console.WriteLine("Si te has equivocado y quieres volver hacía atrás escribe si, y si quieres continuar pulsa el enter");
            string exit = Console.ReadLine();
            if (exit == "si" || exit == "sí" || exit == "Si" || exit == "Sí")
            {
                manageAccountMenu();
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
            decimal money;
            while (!decimal.TryParse(Console.ReadLine(), out money) || money < 0)
            {
                Console.WriteLine("Has introducido un valor no válido. Introduce una cantidad válida:");
            }
            recoRepository.insertDetails(recoService.makeObject(title, author, year, money, userModel.clientName));
        }
    }
}
