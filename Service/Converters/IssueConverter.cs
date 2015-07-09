using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmn = Domain;
using Dto = Service.Dto;

namespace Service.Converters
{
    public static class IssueConverter
    {
        public static Dto.Issue ToDto(Dmn.Issue domain)
        {
            return new Dto.Issue()
            {
                Id = domain.Id,
                Name = domain.Name,
                Open = domain.Open,
                Priority = domain.Priority,
                Description = domain.Description,
                DateRaised = domain.DateRaised,
                DateClosed = domain.DateClosed,
                AssignedTeamId = domain.TeamId,
                AffectedCustomersIds = domain.AffectedCustomers.Select(i => i.Id),
                ////AffectedBrowsers = domain.AffectedBrowsers,
                IssueType = domain.IssueType,
                WebsiteId = domain.Website.Id,
                Notes = domain.Notes
            };
        }

        public static IEnumerable<Dto.Issue> ToDto(IEnumerable<Dmn.Issue> domain)
        {
            return domain.Select(i => ToDto(i));
        }

        public static Dmn.Issue ToNewDmn(Dto.Issue dataObject)
        {
            Dmn.EarlyBirdsContext context = new Dmn.EarlyBirdsContext();
            Dmn.Issue output = new Dmn.Issue(dataObject.Name,
               dataObject.WebsiteId, dataObject.Priority,
                dataObject.IssueType, dataObject.Description)
            {
                TeamId = dataObject.AssignedTeamId,
            };
            //output.AddAffectedCustomers(Dmn.DatabaseTest.SampleCompany.CustomerAccounts.Where(i => dataObject.AffectedCustomersIds.Contains(i.Id)));
            //output.AddAffectedBrowsers(dataObject.AffectedBrowsers);
            return output;
        }

        public static void PutInDomain(Dto.Issue issueData, Dmn.Issue issue)
        {
            Dmn.EarlyBirdsContext context = new Dmn.EarlyBirdsContext();

            //issue.AffectedBrowsers.Clear();
            issue.AffectedCustomers.Clear();
            issue.Notes.Clear();
            issue.Name = issueData.Name;
            issue.WebsiteId = issueData.WebsiteId;
            issue.Priority = issueData.Priority;
            issue.IssueType = issueData.IssueType;
            issue.Description = issueData.Description;
            issue.Open = issueData.Open;
            issue.TeamId = issueData.AssignedTeamId;
            issue.AddAffectedCustomers(context.Customers.Where(i => issueData.AffectedCustomersIds.Contains(i.Id)));
            issue.AddNotes(issueData.Notes);
        }
    }
}
