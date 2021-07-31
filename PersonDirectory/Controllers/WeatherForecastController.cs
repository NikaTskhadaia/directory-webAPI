using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonDirectory.Domain;
using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDirectory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<PersonModel> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WeatherForecastController(ILogger<PersonModel> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ObjectResult Get()
        {
            var p = _unitOfWork.People.GetPerson(1);
            return Ok(p);
        }
    }
}
