using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonDirectory.ActionFilters;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDirectory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonModel> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(ILogger<PersonModel> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetPerson")]
        public ObjectResult GetPerson(int personId)
        {
            var p = _unitOfWork.People.GetPerson(personId);
            return Ok(p);
        }


        [HttpPost("AddPerson")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public void AddPerson([FromBody] PersonModel person)
        {
            _unitOfWork.People.AddPerson(person);
            _unitOfWork.Save();
        }

        [HttpPut("ChangePerson")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public void ChangePerson([FromBody] PersonModel person)
        {
            _unitOfWork.People.ChangePerson(person);
            _unitOfWork.Save();
        }

        [HttpDelete("RemovePerson")]
        public void RemovePerson(int personId)
        {
            _unitOfWork.People.RemovePerson(personId);
            _unitOfWork.Save();
        }


        [HttpPost("AddRelatedPerson")]
        public void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType)
        {
            _unitOfWork.People.AddRelatedPerson(personId, relatedPersonId, relationType);
            _unitOfWork.Save();
        }

        [HttpDelete("RemoveRelatedPerson")]
        public void RemoveRelatedPerson(int personId, int relatedPersonId)
        {
            _unitOfWork.People.RemoveRelatedPerson(personId, relatedPersonId);
            _unitOfWork.Save();
        }


        [HttpGet("CountRelatedPeople")]
        public int CountRelatedPeople(int personId, RelationType relation)
        {
            var count = _unitOfWork.People.RelatedPersonCount(personId, relation);
            return count;
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file, string personalNumber)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
            FileStream stream = new (path, FileMode.Create);
            file.CopyToAsync(stream);
            string relativepath = Path.Combine(@"wwwroot\images", file.FileName);
            _unitOfWork.People.UploadPesonImage(relativepath, personalNumber);
            _unitOfWork.Save();
            return Ok(new { length = file.Length, name = file.FileName });
        }

        [HttpGet("GetPeopleByIdOrName")]
        public IEnumerable<PersonModel> GetPeopleByIdOrName(string searchCrieteria, int numberOfObjectsPerPage, int pageNumber)
        {            
            return _unitOfWork.People.GetPeopleByIdOrName(searchCrieteria, numberOfObjectsPerPage, pageNumber); ;
        }

        [HttpGet("GetPeopleByAny{numberOfObjectsPerPage},{pageNumber}")]
        public IEnumerable<PersonModel> GetPeopleByAny(Gender gender, DateTime dob, string firstname, string lastname, string personalNumber, int? cityId, int numberOfObjectsPerPage, int pageNumber)
        {
            return _unitOfWork.People.GetPeopleByAny(firstname, lastname, gender, personalNumber, dob, cityId, numberOfObjectsPerPage, pageNumber);
        }
    }
}