using pruebaTecnica.models;

public interface IEmployeesRepository : IGenericRepository<Employee>
{
    public Task<List<Employee>> GetEmployeesHiredAfterAsync(DateTime hireDate);
}
