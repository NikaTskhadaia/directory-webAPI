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
        public void AddPerson(Person person)
        {
            _db.People.Add(person);
        }

        public void AddRelatedPerson(Person person, Person relatedPerson)
        {
            _db.Relations.Add(new Relation() { PersonId = person.Id, RelatedPersonId = relatedPerson.Id });
        }

        public void ChangePerson(Person person)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                p = person;
                _db.People.Update(p);
            }
        }

        public IEnumerable<Person> GetPeople()
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            return _db.People.SingleOrDefault(p => p.Id == id);
        }

        public int RelatedPersonCount(Person person, byte relation)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                return p.RelationPeople.Where(r => r.RelationId == relation).Count();
            }
            return 0;
        }

        public void RemovePerson(Person person)
        {
            var p = _db.People.SingleOrDefault(p => p.Id == person.Id);
            if (p is not null)
            {
                _db.People.Remove(person);
            }
        }

        public void RemoveRelatedPerson(Person person, Person relatedPerson)
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
