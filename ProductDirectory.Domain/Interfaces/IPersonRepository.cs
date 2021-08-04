using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonRepository
    {
        public void AddPerson(PersonModel person);

        void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType);

        void ChangePerson(PersonModel person);

        IEnumerable<PersonModel> GetPeopleByIdOrName(string searchCriteria, int take, int page);

        IEnumerable<PersonModel> GetPeopleByAny(string firstname, string lastname, Gender gender, string personalNumber, DateTime dob, int? cityId, int numberOfObjectsPerPage, int pageNumber);

        PersonModel GetPerson(int personId);

        int RelatedPersonCount(int personId, RelationType relation);

        void RemovePerson(int personId);

        void RemoveRelatedPerson(int personId, int relatedPersonId);

        void UploadPesonImage(string path, string personalNumber);
    }
}
