using AWSService.Interfaces.IRepository;
using AWSService.Models;

namespace AWSService.Repositories{
    public class RdsRepository : IRdsRepository
    {
        public RdsRepository()
        {
            
        }
        public Task<List<EmployeesModel>> GetEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
