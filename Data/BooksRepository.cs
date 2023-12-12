using LibrarySvalero.Models;
using Newtonsoft.Json;
namespace LibrarySvalero.Data
{
    public class BooksRepository
    {

        List<BooksModels> listBooks = new List<BooksModels>();
        string pathFile = "../Data/DatabaseBooks.json";
        public void loadData()
        {
            string archivo = File.ReadAllText(pathFile);
            listBooks = JsonConvert.DeserializeObject<List<BooksModels>>(archivo) ?? new List<BooksModels>();
        }

        public BooksModels searchBooks(string nameBook)
        {
            loadData();
            return listBooks.Find(name => name.Title == nameBook);
        }

        public List<BooksModels> getList()
        {
            loadData();
            return listBooks;
        }

      
        public void insertDetailsBook(BooksModels book)
        {
            loadData();
            listBooks.Add(book);
            updateData();
        }
        public void updateData()
        {
            string jsonClientesData = JsonConvert.SerializeObject(listBooks, Formatting.Indented);
            File.WriteAllText(pathFile, jsonClientesData);
        }

        public void deleteBook(string name)
        {
            loadData();
            BooksModels book = searchBooks(name);
            listBooks.Remove(book);
            updateData();
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