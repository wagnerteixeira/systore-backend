namespace Systore.Business.Interfaces;

public interface IGenericCrudBusiness<Type, in IdType>
{
    Task<Type> Create(Type user);
    Task<bool> Delete(IdType id);
    Task<Type> Update(IdType id, Type entity);
    Task<Type?> Read(IdType id);
    Task<IEnumerable<Type>> Read();
}