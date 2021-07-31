using Repository.Data;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    class PersonRepository : IPersonRepository
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
            throw new NotImplementedException();
        }

        public void ChangePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPeople()
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public int RelatedPersonCount(Person person, Relation relation)
        {
            throw new NotImplementedException();
        }

        public int RelatedPersonCount(Person person, Relations relation)
        {
            throw new NotImplementedException();
        }

        public void RemovePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void RemoveRelatedPerson(Person person, Person relatedPerson)
        {
            throw new NotImplementedException();
        }

        public void UploadPesonImage()
        {
            throw new NotImplementedException();
        }
    }
}
