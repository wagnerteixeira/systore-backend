namespace Systore.CrossCutting.Models;

public record Sale
{
    public int Id {get;init;}
    public int ClientId {get;init;}
    public decimal FinalValue {get;init;}
    public DateTime SaleDate {get;init;}
    public string Vendor {get;init;}
}