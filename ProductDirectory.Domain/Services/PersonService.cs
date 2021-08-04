using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Services
{
    public class PersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD
        public PersonModel GetPerson(int personId)
        {
            var p = _unitOfWork.People.GetPerson(personId);
            return p;
        }

        public void AddPerson(PersonModel person)
        {
            _unitOfWork.People.AddPerson(person);
            _unitOfWork.Save();
        }

        public void ChangePerson(PersonModel person)
        {
            _unitOfWork.People.ChangePerson(person);
            _unitOfWork.Save();
        }

        public void RemovePerson(int personId)
        {
            _unitOfWork.People.RemovePerson(personId);
            _unitOfWork.Save();
        }


        public void AddRelatedPerson(int personId, int relatedPersonId, RelationType relationType)
        {
            _unitOfWork.People.AddRelatedPerson(personId, relatedPersonId, relationType);
            _unitOfWork.Save();
        }

        public void RemoveRelatedPerson(int personId, int relatedPersonId)
        {
            _unitOfWork.People.RemoveRelatedPerson(personId, relatedPersonId);
            _unitOfWork.Save();
        }


        public int CountRelatedPeople(int personId, RelationType relation)
        {
            var count = _unitOfWork.People.RelatedPersonCount(personId, relation);
            return count;
        }
        #endregion
    }
}
