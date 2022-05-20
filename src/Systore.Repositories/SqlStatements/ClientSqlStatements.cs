using Systore.CrossCutting.Models;

namespace Systore.Repositories;

public static class ClientSqlStatements
{
    public static string CreateSqlStatement =>
        "INSERT INTO client (" +
        "		Name," +
        "		RegistryDate," +
        "		DateOfBirth," +
        "		Address," +
        "		Neighborhood," +
        "		City," +
        "		State," +
        "		PostalCode," +
        "		Cpf," +
        "		Seller," +
        "		JobName," +
        "		Occupation," +
        "		PlaceOfBirth," +
        "		Spouse," +
        "		Note," +
        "		Phone1," +
        "		Phone2," +
        "		AddressNumber," +
        "		Rg," +
        "		Complement," +
        "		AdmissionDate," +
        "		CivilStatus," +
        "		FatherName," +
        "		MotherName" +
        "	)" +
        " VALUES(" +
        "		@Name," +
        "		@RegistryDate," +
        "		@DateOfBirth," +
        "		@Address," +
        "		@Neighborhood," +
        "		@City," +
        "		@State," +
        "		@PostalCode," +
        "		@Cpf," +
        "		@Seller," +
        "		@JobName," +
        "		@Occupation," +
        "		@PlaceOfBirth," +
        "		@Spouse," +
        "		@Note," +
        "		@Phone1," +
        "		@Phone2," +
        "		@AddressNumber," +
        "		@Rg," +
        "		@Complement," +
        "		@AdmissionDate," +
        "		@CivilStatus," +
        "		@FatherName," +
        "		@MotherName" +
        "	);";

    public static string SelectSingleSqlStatement =>
        "SELECT Id," +
        "	Name," +
        "	RegistryDate," +
        "	DateOfBirth," +
        "	Address," +
        "	Neighborhood," +
        "	City," +
        "	State," +
        "	PostalCode," +
        "	Cpf," +
        "	Seller," +
        "	JobName," +
        "	Occupation," +
        "	PlaceOfBirth," +
        "	Spouse," +
        "	Note," +
        "	Phone1," +
        "	Phone2," +
        "	AddressNumber," +
        "	Rg," +
        "	Complement," +
        "	AdmissionDate," +
        "	CivilStatus," +
        "	FatherName," +
        "	MotherName" +
        " FROM client" +
        " WHERE Id = @Id;";

    public static string SelectAllSqlStatement =>
        "SELECT Id," +
        "	Name," +
        "	RegistryDate," +
        "	DateOfBirth," +
        "	Address," +
        "	Neighborhood," +
        "	City," +
        "	State," +
        "	PostalCode," +
        "	Cpf," +
        "	Seller," +
        "	JobName," +
        "	Occupation," +
        "	PlaceOfBirth," +
        "	Spouse," +
        "	Note," +
        "	Phone1," +
        "	Phone2," +
        "	AddressNumber," +
        "	Rg," +
        "	Complement," +
        "	AdmissionDate," +
        "	CivilStatus," +
        "	FatherName," +
        "	MotherName" +
        " FROM client" +
        " WHERE 0 = 0"; //AND {0};

    public static string DeleteSqlStatement =>
        "DELETE FROM Client WHERE Id=@Id;";

    public static string UpdateSqlStatement =>
        "UPDATE client" +
        " SET Name = @Name," +
        "	RegistryDate = @RegistryDate," +
        "	DateOfBirth = @DateOfBirth," +
        "	Address = @Address," +
        "	Neighborhood = @Neighborhood," +
        "	City = @City," +
        "	State = @State," +
        "	PostalCode = @PostalCode," +
        "	Cpf = @Cpf," +
        "	Seller = @Seller," +
        "	JobName = @JobName," +
        "	Occupation = @Occupation," +
        "	PlaceOfBirth = @PlaceOfBirth," +
        "	Spouse = @Spouse," +
        "	Note = @Note," +
        "	Phone1 = @Phone1," +
        "	Phone2 = @Phone2," +
        "	AddressNumber = @AddressNumber," +
        "	Rg = @Rg," +
        "	Complement = @Complement," +
        "	AdmissionDate = @AdmissionDate," +
        "	CivilStatus = @CivilStatus" +
        "	FatherName = @FatherName," +
        "	MotherName = @MotherName" +
        " WHERE Id = @Id;";
}