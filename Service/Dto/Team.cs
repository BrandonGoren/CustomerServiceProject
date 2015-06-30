using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class Team
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> TeamMemberIds { get; set; }
        public ISet<Domain.TypeOfIssue> Expertise { get; set; }
        public IEnumerable<int> AssignedIssueIds { get; set; }
    }
}
