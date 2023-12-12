namespace LibrarySvalero.Models;

public class RecomendationsModels
{
    public string title {get; set;}
    public string author {get; set;}
    public string year {get; set;}
    public double money {get; set;}
    public string gender {get; set;}
    public string description {get; set;}
    public string clientName {get; set;}
    public List<string> list {get; set;}= new List<string>();
}

