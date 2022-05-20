namespace Systore.CrossCutting.Models;

public class ItemSale
{
    public int Id {get;init;}
    public int SaleId {get;init;}
    public int ProductId {get;init;}
    public decimal Price {get;init;}
    public decimal Quantity {get;init;}
    public decimal TotalPrice {get;init;}
}