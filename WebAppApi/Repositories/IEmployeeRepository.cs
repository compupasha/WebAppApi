using DataAccessLayer;

namespace WebAppApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int Id);

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> UpdateEmployee(Employee employee);

        Task<Employee> DeleteEmployee(int Id);
    }
}
