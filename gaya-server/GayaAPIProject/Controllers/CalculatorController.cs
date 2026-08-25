using GayaAPIProject.Models;
using GayaAPIProject.DTOs;
using GayaAPIProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GayaAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        // dependency injection - ICalculatorService
        private readonly ICalculatorService _service;

        //בנאי
        public CalculatorController(ICalculatorService service)
        {
            _service = service;
        }

        [HttpPost]
        public CalculateResponse Calculate([FromBody] CalculateRequest request)
        {
            return _service.Calculate(request);
        }
        [HttpGet]
        public List<Operation> GetAllOperations() 
            {
                return _service.GetAllOperations();
            }
    }
}
