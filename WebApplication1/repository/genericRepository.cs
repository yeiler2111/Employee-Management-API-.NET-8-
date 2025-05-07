using Microsoft.EntityFrameworkCore;
using pruebaTecnica.config;
using pruebaTecnica.models;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDBContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) =>
        await _dbSet.FindAsync(id);

    public async Task<T?> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch(DbUpdateException ex)
        {
            return null;
        }
        
    }

    public async Task<T?> UpdateAsync(int id, T entity)
    {
        T existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity == null) return null;
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (property.Name == "EmployeeId") continue;
            var newValue = property.GetValue(entity);

            if (newValue != null)
            {
                property.SetValue(existingEntity, newValue);
            }
        }

        await _context.SaveChangesAsync();

        return existingEntity;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
           return true;
        }
        return false;
    }


}
