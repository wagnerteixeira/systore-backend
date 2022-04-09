namespace Systore.CrossCutting.Models;

public class Product
{
    public int Id {get;init;}
    public byte SaleType {get;init;}
    public decimal Price {get;init;}
    public Int16 ExpirationDays {get;init;}
    public string Description {get;init;}
    public string ExtraInformation {get;init;}
    public byte PrintExpirationDate {get;init;}
    public byte PrintDateOfPackaging {get;init;}
    public byte ExportToBalance {get;init;}
}