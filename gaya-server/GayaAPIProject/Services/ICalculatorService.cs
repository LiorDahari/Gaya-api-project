using GayaAPIProject.DTOs;
using GayaAPIProject.Models;

namespace GayaAPIProject.Services
{
    public interface ICalculatorService
    {
        CalculateResponse Calculate(CalculateRequest request);

        // החזרת כל הפעולות הזמינות מה DB
        List<Operation> GetAllOperations();

        //החזרת שלושת הפעולות האחרונות
        List<CalculationHistory> GetLastHistory();
    }
}