using LibrarySvalero.Models;
using Newtonsoft.Json;
namespace LibrarySvalero.Data
{
    public class BooksRepository
    {

        List<BooksModels> listBooks = new List<BooksModels>();
        string ruthFile = "../Data/DatabaseBooks.json";
        public void loadData()
        {
            string archivo = File.ReadAllText(ruthFile);
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