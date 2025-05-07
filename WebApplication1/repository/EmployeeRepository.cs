using Microsoft.EntityFrameworkCore;
using pruebaTecnica.config;
using pruebaTecnica.models;

namespace pruebaTecnica.repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        private readonly ApplicationDBContext _context;
        public EmployeeRepository(ApplicationDBContext context) : base(context)
        {
            this._context = context;
        }

        //metodos específicos
        public async Task<List<Employee>> GetEmployeesHiredAfterAsync(DateTime hireDate)
        {
            var employees = await _context.Employees
                                         .FromSqlRaw("EXECUTE GetEmployeesHiredAfter @HireDate = {0}", hireDate)
                                         .ToListAsync();
            return employees;

        }
    }
}