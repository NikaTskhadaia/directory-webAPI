using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public string Photo { get; set; }
        public List<RelatedPerson> RelatedPeople { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }


    public enum Gender
    {
        Female = 1,
        Male = 2
    }

    public enum PhoneNumberType
    {
        Mobile = 1,
        Office = 2,
        Home = 3
    }
}
