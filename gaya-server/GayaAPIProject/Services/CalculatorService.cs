using GayaAPIProject.DTOs;
using GayaAPIProject.Models;
using GayaAPIProject.Repository;
using NCalc;

namespace GayaAPIProject.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IOperationRepository _repository;
        //בנאי
        public CalculatorService(IOperationRepository repository)  
        {
            _repository = repository;
        }
        
        public CalculateResponse Calculate(CalculateRequest request)
        {
            //שליפת הפעולה מה DB לפי OperationId
            var operation = _repository.GetOperation(request.OperationId);

            //Ncalc operation
            var expression = new Expression(operation.Implementation);
            expression.Parameters["a"] = request.ValueA;
            expression.Parameters["b"] = request.ValueB;
            var result = expression.Evaluate().ToString();

            //כתיבה להסטוריה
            var history = new CalculationHistory
            {
                OperationId = request.OperationId,
                ValueA = request.ValueA,
                ValueB = request.ValueB,
                Result = result ?? "",
                CreatedAt = DateTime.Now
            };
            _repository.SaveHistory(history);

            //החזרת תשובה עם התוצאה ושם הפעולה
            return new CalculateResponse
            {
                Name = operation.Name,
                Result = result ?? ""
            };
        }
        // החזרת כל הפעולות הזמינות מה DB
        public List<Operation> GetAllOperations()
            {
                return _repository.GetAllOperations();
            }
        


    }
}

