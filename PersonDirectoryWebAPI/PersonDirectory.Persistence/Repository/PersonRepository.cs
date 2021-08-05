using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using PersonDirectory.Persistence.Data;
using PersonDirectory.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Persistence.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleDb _db;
        private readonly IMapper _mapper;

        public PersonRepository(PeopleDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Add(PersonModel person)
        {
            Person p = _mapper.Map<Person>(person);
            _db.People.Add(p);
        }

        public PersonModel Get(int personId)
        {
            Person p = _db.People.Include(p => p.PhoneNumbers).Include(p => p.RelationPeople).SingleOrDefault(p => p.Id == personId);
            //TODO - Correctly map collections -
            PersonModel person = _mapper.Map<PersonModel>(p);
            return person;
        }

        public void Update(PersonModel person)
        {
            var p = _db.People.AsNoTracking().SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                p = _mapper.Map<Person>(person);
                _db.People.Update(p);
            }
        }

        public IEnumerable<PersonModel> GetAll(string searchCriteria, int numberOfObjectsPerPage, int pageNumber)
        {
            IEnumerable<Person> people = _db.People.FromSqlRaw($"SP_GetPeople {searchCriteria},{numberOfObjectsPerPage},{pageNumber}").ToList();
            return _mapper.Map<List<PersonModel>>(people);
        }

        public void Remove(int personId)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == personId);
            if (p is not null)
            {
                _db.People.Remove(p);
            }
        }

        #region Initial Implementation

        public void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType)
        {
            _db.Relations.Add(new Relation() { PersonId = personId, RelatedPersonId = relatedPersonId, RelationId = relationType });
        }

        public void RemoveRelatedPerson(int personId, int relatedPersonId)
        {
            var r = _db.Relations.SingleOrDefault(r => r.PersonId == personId && r.RelatedPersonId == relatedPersonId);
            if (r is not null)
            {
                _db.Relations.Remove(r);
            }
        }

        public int RelatedPersonCount(int personId, RelationType relation)
        {
            var p = _db.People.Include(p => p.RelationPeople).SingleOrDefault(p => p.Id == personId);
            if (p is not null)
            {
                return p.RelationPeople.Where(r => r.RelationId == relation).Count();
            }
            return 0;
        }

        public void UploadPesonImage(string path, string personalNumber)
        {
            var p = _db.People.SingleOrDefault(p => p.PersonalNumber == personalNumber);
            p.Photo = path;
        }

        public IEnumerable<PersonModel> GetPeopleByAny(string firstname, string lastname, Gender gender, string personalNumber, DateTime dob, int? cityId, int numberOfObjectsPerPage, int pageNumber)
        {
            var s = $"SP_Search_Person '{firstname}','{lastname}',{(byte)gender},'{personalNumber}','{dob}','{cityId}','{numberOfObjectsPerPage}','{pageNumber}'";
            IEnumerable<Person> people = _db.People.FromSqlRaw(s).ToList();
            List<PersonModel> result = new();
            foreach (var item in people)
            {
                result.Add(new PersonModel
                {
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Gender = item.Gender,
                    DateOfBirth = item.DateOfBirth,
                    PersonalNumber = item.PersonalNumber,
                    CityId = item.CityId,
                    Photo = item.Photo
                });
            }
            return result;
        }
        #endregion
    }
}
