namespace pruebaTecnica.services
{
    using pruebaTecnica.models;
    using System.Threading.Tasks;

    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> UpdateEmployeeAsync(int id , Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);

        Task<IEnumerable<Employee>> GetEmployeesHiredAfterAsync(DateTime hireDate);
    }
}
