using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using PersonDirectory.Persistence.Data;
using PersonDirectory.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Persistence.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleDb _db;

        public PersonRepository(PeopleDb db)
        {
            _db = db;
        }

        public PersonModel GetPerson(int personId)
        {
            var p = _db.People.Include(p => p.PhoneNumbers).Include(p => p.RelationPeople).SingleOrDefault(p => p.Id == personId);

            PersonModel pModel = new PersonModel
            {
                Id = p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth,
                PersonalNumber = p.PersonalNumber,
                RelatedPeople = new List<RelatedPerson>(),
                PhoneNumbers = new List<Domain.Models.PhoneNumber>()
            };

            foreach (var item in p.RelationPeople)
            {
                pModel.RelatedPeople.Add(new RelatedPerson { PersonId = item.PersonId, RelatedPersonId = item.RelatedPersonId, RelationId = item.RelationId });
            }

            foreach (var item in p.PhoneNumbers)
            {
                pModel.PhoneNumbers.Add(new Domain.Models.PhoneNumber { Id = item.Id, Number = item.Number, NumberType = item.PhoneNumberType });
            }

            return pModel;
        }

        public void AddPerson(PersonModel person)
        {
            Person p = new Person
            {
                Firstname = person.Firstname,
                Lastname = person.Lastname,
                Gender = person.Gender,
                PersonalNumber = person.PersonalNumber,
                DateOfBirth = person.DateOfBirth
            };
            _db.People.Add(p);
        }

        public void ChangePerson(PersonModel person)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                p.Firstname = person.Firstname;
                p.Lastname = person.Lastname;
                p.Gender = person.Gender;
                p.PersonalNumber = person.PersonalNumber;
                p.DateOfBirth = person.DateOfBirth;
                p.CityId = person.CityId;
            }
        }

        public void RemovePerson(int personId)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == personId);
            if (p is not null)
            {
                _db.People.Remove(p);
            }
        }

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

        public IEnumerable<PersonModel> GetPeopleByIdOrName(string searchCriteria, int numberOfObjectsPerPage, int pageNumber)
        {
            IEnumerable<Person> people = _db.People.FromSqlRaw($"SP_GetPeople {searchCriteria},{numberOfObjectsPerPage},{pageNumber}").ToList();
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
    }
}
