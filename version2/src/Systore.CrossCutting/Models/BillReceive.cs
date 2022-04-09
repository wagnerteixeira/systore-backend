namespace Systore.CrossCutting.Models;

public record BillReceive
{
    public int Id {get;init;}
    public int ClientId {get;init;}
    public int Code {get;init;}
    public Int16 Quota {get;init;}
    public decimal OriginalValue {get;init;}
    public decimal Interest {get;init;}
    public decimal FinalValue {get;init;}
    public DateTime PurchaseDate {get;init;}
    public DateTime DueDate {get;init;}
    public DateTime PayDate {get;init;}
    public int DaysDelay {get;init;}
    public byte Situation {get;init;}
    public string Vendor {get;init;}
}