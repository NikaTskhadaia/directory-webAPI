using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Repository
{
    public interface IPersonRepository
    {
        public void AddPerson(Person person);

        void AddRelatedPerson(Person person, Person relatedPerson);

         void ChangePerson(Person person);

         IEnumerable<Person> GetPeople();

         Person GetPerson(int id);

         int RelatedPersonCount(Person person, RelationType relation);

         void RemovePerson(Person person);

         void RemoveRelatedPerson(Person person, Person relatedPerson);

         void UploadPesonImage();
    }
}
