using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Service.Dto
{
    public class Issue
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime DateRaised { get; set; }
        public DateTime DateClosed { get; set; }
        public bool Open { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> AffectedCustomersIds { get; set; }
        public IEnumerable<Domain.WebBrowser> AffectedBrowsers { get; set; }
        public int? AssignedTeamId { get; set; }
        public Domain.TypeOfIssue IssueType { get; set; }
        public int WebsiteId { get; set; }
        public IList<Notes> Notes { get; set; }
    }
}
