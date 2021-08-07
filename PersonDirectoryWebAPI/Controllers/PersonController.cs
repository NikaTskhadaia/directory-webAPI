using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PersonDirectory.Domain.DTOs;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using PersonDirectory.WebAPI.ActionFilters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDirectory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonModel> _logger;
        private readonly IPersonService _personService;
        private readonly IStringLocalizer<PersonController> _localizer;

        public PersonController(ILogger<PersonModel> logger, IPersonService personService, IStringLocalizer<PersonController> localizer)
        {
            _logger = logger;
            _personService = personService;
            _localizer = localizer;
        }


        [HttpGet("Get")]
        public ObjectResult Get(int personId)
        {
            var p = _personService.GetPerson(personId);
            return Ok(p);
        }


        [HttpPost("Add")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public void Add([FromBody] PersonDto person)
        {
            _personService.AddPerson(person);
        }

        [HttpPut("Update")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public void Update([FromBody] PersonDto person)
        {
            _personService.UpdatePerson(person);
        }

        [HttpDelete("RemovePerson")]
        public void RemovePerson(int personId)
        {
            _personService.RemovePerson(personId);
        }

        [HttpPost("AddRelatedPerson")]
        public void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType)
        {
            _personService.AddRelatedPerson(personId, relatedPersonId, relationType);
        }

        [HttpDelete("RemoveRelatedPerson")]
        public void RemoveRelatedPerson(int personId, int relatedPersonId)
        {
            _personService.RemoveRelatedPerson(personId, relatedPersonId);
        }

        [HttpGet("CountRelatedPeople")]
        public int CountRelatedPeople(int personId, RelationType relation)
        {
            var count = _personService.RelatedPeopleCount(personId, relation);
            return count;
        }

        [HttpPost("UploadImage")]
        public IActionResult UploadImage(IFormFile file, string personalNumber)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                _personService.UploadPesonImage(fileBytes, file.FileName, personalNumber);
            }
            return Ok();
        }

        [HttpGet("SearchPeople")]
        public IEnumerable<PersonDto> SearchPeople(string searchCrieteria, int numberOfObjectsPerPage, int pageNumber)
        {            
            return _personService.SearchPeople(searchCrieteria, numberOfObjectsPerPage, pageNumber);
        }

        [HttpGet("SearchPerson")]
        public IEnumerable<PersonDto> SearchPerson(Gender gender, DateTime dob, string firstname, string lastname, string personalNumber, int? cityId, int numberOfObjectsPerPage, int pageNumber)
        {
            return _personService.SearchPerson(gender, dob, firstname, lastname, personalNumber, cityId, numberOfObjectsPerPage, pageNumber);
        }
    }
}