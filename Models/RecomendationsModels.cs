
namespace LibrarySvalero.Models;

public class RecomendationsModels
{
    public string Title {get; set;}
    public string Author {get; set;}
    public string Year {get; set;}
    public double Money {get; set;}
    public string clientName {get; set;}
    public List<string> list {get; set;}= new List<string>();
}
