using PersonDirectory.Domain.DTOs;
using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonService
    {
        void AddPerson(PersonDto person);

        void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType);

        void UpdatePerson(PersonDto person);

        IEnumerable<PersonDto> SearchPerson(Gender gender, DateTime dob, string firstname, string lastname, string personalNumber, int? cityId, int numberOfObjectsPerPage, int pageNumber);

        IEnumerable<PersonDto> SearchPeople(string searchCrieteria, int numberOfObjectsPerPage, int pageNumber);

        PersonDto GetPerson(int personId);

        int RelatedPeopleCount(int personId, RelationType relation);

        void RemovePerson(int personId);

        void RemoveRelatedPerson(int personId, int relatedPersonId);

        void UploadPesonImage(byte[] image, string fileName, string personalNumber);
    }
}