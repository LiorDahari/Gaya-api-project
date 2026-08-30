using GayaAPIProject.DTOs;
using GayaAPIProject.Models;
using GayaAPIProject.Repository;
using NCalc;

namespace GayaAPIProject.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IOperationRepository _repository;

        // בנאי
        public CalculatorService(IOperationRepository repository)
        {
            _repository = repository;
        }

        public CalculateResponse Calculate(CalculateRequest request)
        {
            // שליפת הפעולה מה-DB לפי OperationId
            var operation = _repository.GetOperation(request.OperationId);

            string result;

            try
            {
                // Ncalc operation
                var expression = new Expression(operation.Implementation);
                expression.Parameters["a"] = request.ValueA;
                expression.Parameters["b"] = request.ValueB;

                var evaluatedResult = expression.Evaluate();
                result = evaluatedResult?.ToString() ?? "";
            }
            catch (Exception)
            {
                // במקרה של שגיאת פורמט או חישוב לא חוקי ב-NCalc
                return new CalculateResponse
                {
                    Name = operation?.Name ?? "",
                    Result = "פעולה לא חוקית"
                };
            }

            // כתיבה להסטוריה (רק במקרה של חישוב מוצלח)
            var history = new CalculationHistory
            {
                OperationId = request.OperationId,
                ValueA = request.ValueA,
                ValueB = request.ValueB,
                Result = result,
                CreatedAt = DateTime.Now
            };
            _repository.SaveHistory(history);

            // החזרת תשובה עם התוצאה ושם הפעולה
            return new CalculateResponse
            {
                Name = operation.Name,
                Result = result
            };
        }

        // החזרת כל הפעולות הזמינות מה-DB
        public List<Operation> GetAllOperations()
        {
            return _repository.GetAllOperations();
        }
        //החזרת הסטוריה - שלוש פעולות אחרונות
        public List<CalculationHistory> GetLastHistory() 
        { 
            return _repository.GetLastHistory(); 
        }
    }
}