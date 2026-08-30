using GayaAPIProject.Models;

namespace GayaAPIProject.Repository
{
    public interface IOperationRepository
    {
        List<Operation> GetAllOperations();
        Operation GetOperation(int id);
        void SaveHistory(CalculationHistory history);
        List<CalculationHistory> GetLastHistory();
    }
}
