using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class Agent
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> TeamIds { get; set; }
        public IEnumerable<int> IssueIds { get; set; }
    }
}
