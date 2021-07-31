using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Repository
{
    public interface IPersonRepository
    {
        public void AddPerson(PersonModel person);

        void AddRelatedPerson(PersonModel person, PersonModel relatedPerson);

         void ChangePerson(PersonModel person);

         IEnumerable<PersonModel> GetPeople();

        PersonModel GetPerson(int id);

         int RelatedPersonCount(PersonModel person, RelationType relation);

         void RemovePerson(PersonModel person);

         void RemoveRelatedPerson(PersonModel person, PersonModel relatedPerson);

         void UploadPesonImage();
    }
}
