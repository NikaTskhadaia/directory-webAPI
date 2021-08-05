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
    }
}
