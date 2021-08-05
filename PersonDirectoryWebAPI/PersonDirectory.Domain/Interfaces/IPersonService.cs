using PersonDirectory.Domain.DTOs;
using PersonDirectory.Domain.Models;
using System.Collections.Generic;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonService
    {
        public void AddPerson(PersonDto person);

        void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType);

        void ChangePerson(PersonDto person);

        IEnumerable<PersonDto> GetPeople();

        PersonDto GetPerson(int personId);

        int RelatedPersonCount(int personId, RelationType relation);

        void RemovePerson(int personId);

        void RemoveRelatedPerson(int personId, int relatedPersonId);

        void UploadPesonImage();
    }
}