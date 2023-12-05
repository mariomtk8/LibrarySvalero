using System.Reflection.Metadata;
using Microsoft.Win32;

namespace LibrarySvalero.Models;

public class AccountModels
{
    public string clientName {get; set;}
    public string password {get; set;}
    public string clientAdress {get; set;}
    public string clientPhoneNumber {get; set;}
    public decimal clientMoney {get; set;}
    public List<string> register  {get; set;} = new List<string>();
}
