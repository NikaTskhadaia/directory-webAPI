using PersonDirectory.Domain.MyValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Lastname { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [StringLength(11)]
        public string PersonalNumber { get; set; }

        [Required]
        [AdultAge]
        public DateTime DateOfBirth { get; set; }

        public int? CityId { get; set; }

        public string Photo { get; set; }

        public List<RelatedPerson> RelatedPeople { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), new List<ValidationResult>(), true);
        }
    }


    public enum Gender : byte
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
