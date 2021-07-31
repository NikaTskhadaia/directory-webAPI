using Microsoft.EntityFrameworkCore;
using PersonDirectory.Domain.Models;
using PersonDirectory.Domain.Repository;
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
        public void AddPerson(PersonModel person)
        {
            Person p = new Person
            {
                Id = person.Id,
                Firstname = person.Firstname
            };
            _db.People.Add(p);
        }

        public void AddRelatedPerson(PersonModel person, PersonModel relatedPerson)
        {
            _db.Relations.Add(new Relation() { PersonId = person.Id, RelatedPersonId = relatedPerson.Id });
        }

        public void ChangePerson(PersonModel person)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                p.Firstname = person.Firstname;
                _db.People.Update(p);
            }
        }

        public IEnumerable<PersonModel> GetPeople()
        {
            throw new NotImplementedException();
        }

        public PersonModel GetPerson(int id)
        {
            var p = _db.People.Include(p => p.RelationRelatedPeople).SingleOrDefault(p => p.Id == id);
            return new PersonModel { 
                Id = p.Id,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth,
                PersonalNumber = p.PersonalNumber
            };
        }

        public int RelatedPersonCount(PersonModel person, RelationType relation)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                return p.RelationPeople.Where(r => r.RelationId == relation).Count();
            }
            return 0;
        }

        public void RemovePerson(PersonModel person)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                _db.People.Remove(p);
            }
        }

        public void RemoveRelatedPerson(PersonModel person, PersonModel relatedPerson)
        {
            var r = _db.Relations.SingleOrDefault(r => r.PersonId == person.Id && r.RelatedPersonId == relatedPerson.Id);
            if (r is not null)
            {
                _db.Relations.Remove(r);
            }
        }

        public void UploadPesonImage()
        {
            throw new NotImplementedException();
        }
    }
}
