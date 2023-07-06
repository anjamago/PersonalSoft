namespace Entities.Interface.Repositories;

public interface IDbMongo<T> where T: class
{
    
    public Task<List<T>> GetAsync();

    public Task<T> GetAsync(string id);

    public Task CreateAsync(T newBook);

    public Task UpdateAsync(string id, T updatedBook);

    public Task RemoveAsync(string id);
    
    
}