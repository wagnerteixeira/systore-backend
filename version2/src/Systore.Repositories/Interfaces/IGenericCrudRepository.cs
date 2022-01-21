namespace Systore.Repositories.Interfaces;

public interface IGenericCrudRepository<Type, IdType>
{
    Task<Type> Create(Type entity);
    Task<bool> Delete(IdType id);
    Task<Type> Update(Type entity);
    Task<Type?> Read(IdType id);
    Task<IEnumerable<Type>> Read();
    
    object GetIdParam(IdType id);
    object GetIdParam(Type entity);
}