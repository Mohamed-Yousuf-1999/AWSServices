using AWSService.Models;

namespace AWSService.Interfaces.IRepository
{
    public interface IRdsRepository
    {
        Task<List<EmployeesModel>> GetEmployees();
    }
}

