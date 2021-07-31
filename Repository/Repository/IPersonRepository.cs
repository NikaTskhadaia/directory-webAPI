using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IPersonRepository
    {
        void AddPerson(Person person);
        void ChangePerson(Person person);
        void UploadPesonImage();
        void RemovePerson(Person person);
        void AddRelatedPerson(Person person, Person relatedPerson);
        void RemoveRelatedPerson(Person person, Person relatedPerson);
        Person GetPerson(int id);
        IEnumerable<Person> GetPeople();
        int RelatedPersonCount(Person person, Relations relation);
    }
}
