﻿using pruebaTecnica.models;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> AddAsync(T entity);
    Task<T?> UpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
}
