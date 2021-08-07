using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.MyValidationAttribute
{
    public class AdultAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext  context)
        {
            TimeSpan givenAge = DateTime.Now - DateTime.Parse(value.ToString());
            if (givenAge.TotalDays/365 < 18)
            {
                return new ValidationResult("The minimum age must be 18");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
