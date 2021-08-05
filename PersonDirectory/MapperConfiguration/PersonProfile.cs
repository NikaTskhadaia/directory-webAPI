using AutoMapper;
using PersonDirectory.Domain.Models;
using PersonDirectory.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDirectory.MapperConfiguration
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonModel, Person>();
            CreateMap<Person, PersonModel>();
            CreateMap<Persistence.Models.PhoneNumber, Domain.Models.PhoneNumber>();
            CreateMap<Relation, RelatedPerson>();
        }
    }
}
