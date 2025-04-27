using AWSService.Interfaces.IRepository;
using AWSService.Interfaces.IServices;
using AWSService.Models;

namespace AWSService.Services;

public class RdsService : IRdsService
{
    private readonly IRdsRepository _rdsRepository;

    public RdsService(IRdsRepository rdsRepository)
    {
        _rdsRepository = rdsRepository;
    }

    public async Task<List<EmployeesModel>> GetEmployees() => await _rdsRepository.GetEmployees();
    
}