using AWSService.Models;

namespace AWSService.Interfaces.IServices{
    public interface IRdsService
    {
        Task<List<EmployeesModel>> GetEmployees();
    }
}
