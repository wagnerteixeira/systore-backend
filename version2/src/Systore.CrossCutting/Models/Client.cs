namespace Systore.CrossCutting.Models;

public record Client
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime RegistryDate { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Address { get; init; }
    public string Neighborhood { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string PostalCode { get; init; }
    public string Cpf { get; init; }
    public string Seller { get; init; }
    public string JobName { get; init; }
    public string Occupation { get; init; }
    public string PlaceOfBirth { get; init; }
    public string Spouse { get; init; }
    public string Note { get; init; }
    public string Phone1 { get; init; }
    public string Phone2 { get; init; }
    public string AddressNumber { get; init; }
    public string Rg { get; init; }
    public string Complement { get; init; }
    public DateTime AdmissionDate { get; init; }
    public byte CivilStatus { get; init; }
    public string FatherName { get; init; }
    public string MotherName { get; init; }
}