using PersonDirectory.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository People { get; }

        int Save();
    }
}
