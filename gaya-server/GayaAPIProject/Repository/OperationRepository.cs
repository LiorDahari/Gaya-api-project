using GayaAPIProject.Models;
using GayaAPIProject.Data;

namespace GayaAPIProject.Repository
{
    public class OperationRepository : IOperationRepository

    {
        private readonly AppDbContext _context;
        public OperationRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Operation> GetAllOperations()
        {
            return _context.Operations.ToList();
        }
        public Operation GetOperation(int id)
        {
            return _context.Operations.FirstOrDefault(o => o.Id == id);
        }
        public void SaveHistory(CalculationHistory history)
        {
            _context.CalculationHistories.Add(history);  // הוסף לטבלה
            _context.SaveChanges();                       // שמור בDB
        }
    }
}