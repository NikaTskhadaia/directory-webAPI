using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IPersonRepository : IRepositoryBase<PersonModel>
    {
        void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType);

        IEnumerable<PersonModel> GetPeopleByAny(string firstname, string lastname, Gender gender, string personalNumber, DateTime dob, int? cityId, int numberOfObjectsPerPage, int pageNumber);

        int RelatedPersonCount(int personId, RelationType relation);

        void RemoveRelatedPerson(int personId, int relatedPersonId);

        void UploadPesonImage(string path, string personalNumber);
    }
}
