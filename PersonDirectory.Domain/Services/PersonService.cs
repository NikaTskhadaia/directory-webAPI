using AutoMapper;
using PersonDirectory.Domain.DTOs;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PersonDirectory.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddPerson(PersonDto personDto)
        {
            var person = _mapper.Map<PersonModel>(personDto);
            _unitOfWork.People.Add(person);
            _unitOfWork.Save();
        }

        public PersonDto GetPerson(int personId)
        {
            var personModel = _unitOfWork.People.Get(personId);
            return _mapper.Map<PersonDto>(personModel);
        }

        public void UpdatePerson(PersonDto personDto)
        {
            var personModel = _mapper.Map<PersonModel>(personDto);
            _unitOfWork.People.Update(personModel);
            _unitOfWork.Save();
        }

        public void RemovePerson(int personId)
        {
            _unitOfWork.People.Remove(personId);
            _unitOfWork.Save();
        }

        public void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType)
        {
            _unitOfWork.People.AddRelatedPerson(personId, relatedPersonId, relationType);
            _unitOfWork.Save();
        }

        public void RemoveRelatedPerson(int personId, int relatedPersonId)
        {
            _unitOfWork.People.RemoveRelatedPerson(personId, relatedPersonId);
            _unitOfWork.Save();
        }

        public int RelatedPeopleCount(int personId, RelationType relation)
        {
            return _unitOfWork.People.RelatedPeopleCount(personId, relation);
        }

        public void UploadPesonImage(byte[] image, string fileName, string personalNumber)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            FileStream stream = new(path, FileMode.Create);
            File.WriteAllBytes(path, image);
            string relativepath = Path.Combine(@"wwwroot\images", fileName);
            _unitOfWork.People.UploadPesonImage(relativepath, personalNumber);
            _unitOfWork.Save();
        }

        public IEnumerable<PersonDto> SearchPeople(string searchCrieteria, int numberOfObjectsPerPage, int pageNumber)
        {
            var people = _unitOfWork.People.GetAll(searchCrieteria, numberOfObjectsPerPage, pageNumber);
            return _mapper.Map<List<PersonDto>>(people);
        }

        public IEnumerable<PersonDto> SearchPerson(Gender gender, DateTime dob, string firstname, string lastname, string personalNumber, int? cityId, int numberOfObjectsPerPage, int pageNumber)
        {
            var people = _unitOfWork.People.GetPeopleByAny(firstname, lastname, gender, personalNumber, dob, cityId, numberOfObjectsPerPage, pageNumber);
            return _mapper.Map<List<PersonDto>>(people);
        }
    }
}
