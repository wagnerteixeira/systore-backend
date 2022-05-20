using Systore.Business.Interfaces;
using Systore.Repositories.Interfaces;

namespace Systore.Business;

public class GenericCrudBusiness<Type, IdType> : IGenericCrudBusiness<Type, IdType>
{
    private readonly IGenericCrudRepository<Type, IdType> _genericCrudRepository;

    public GenericCrudBusiness(IGenericCrudRepository<Type, IdType> genericCrudRepository)
    {
        _genericCrudRepository = genericCrudRepository;
    }

    public async Task<Type> Create(Type entity)
    {
        return await _genericCrudRepository.Create(entity);
    }

    public async Task<bool> Delete(IdType id)
    {
        return await _genericCrudRepository.Delete(id);
    }

    public async Task<Type> Update(IdType id, Type entity)
    {
        return await _genericCrudRepository.Update(id, entity);
    }

    public async Task<Type?> Read(IdType id)
    {
        return await _genericCrudRepository.Read(id);
    }

    public async Task<IEnumerable<Type>> Read()
    {
        return await _genericCrudRepository.Read();
    }
}