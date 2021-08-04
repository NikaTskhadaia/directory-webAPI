using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Models
{
    public class RelatedPerson
    {
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public RelationType RelationId { get; set; }
    }

    public enum RelationType : byte
    {
        Coleague = 1,
        Familiar = 2,
        Relative = 3,
        Other = 4
    }
}
