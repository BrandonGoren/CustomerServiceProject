using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Service.Converters;
using Dto = Service.Dto;

namespace WebApi.Controllers
{
    [RoutePrefix("issues")]
    public class IssueController : ApiController
    {
        private ISet<Issue> issues = DatabaseTest.SampleCompany.Issues;
        private Company company = DatabaseTest.SampleCompany;

        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(IssueConverter.ToDto(this.issues));
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Issue issue = this.issues.FirstOrDefault(i => i.Id == id);
            if (issue == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(IssueConverter.ToDto(issue));
            }
        }

        [Route("issues/website/{id:int}")]
        public IHttpActionResult GetWebsiteIssues(int websiteId)
        {
            return this.Ok(IssueConverter.ToDto(this.issues.Where(i => i.Website.Id == websiteId)));
        }

        ////[Route("?search")]

        [Route("{openOrClosed}")]
        public IHttpActionResult Get(string openOrClosed)
        {
            if (openOrClosed == "open" || openOrClosed == "closed")
            {
                bool open = openOrClosed == "open";
                Issue issue = this.issues.FirstOrDefault(i => i.Open == open);
                if (issue == null)
                {
                    return this.NotFound();
                }
                else
                {
                    return this.Ok(IssueConverter.ToDto(issue));
                }
            }
            else
            {
                return this.NotFound();
            }
        }

        [Route]
        public IHttpActionResult Post(Dto.Issue issueData)
        {
            Issue issue = IssueConverter.ToNewDmn(issueData);
            this.issues.Add(issue);
            return this.Created(this.Request.RequestUri.AbsolutePath + "/" + issue.Id, IssueConverter.ToDto(issue));
        }

        [Route("{issueId:int}/add-affected-customer")]
        public IHttpActionResult PutAddAffectedCustomer(int issueId, int customerId)
        {
            Issue issue = this.company.Issues.FirstOrDefault(i => i.Id == issueId);
            Customer customer = this.company.CustomerAccounts.FirstOrDefault(i => i.Id == customerId);
            if (issue == null || customer == null)
            {
                return this.NotFound();
            }
            else
            {
                issue.AffectedCustomers.Add(customer);
                return this.Created(this.Request.RequestUri.AbsolutePath + "/" + issue.Id, IssueConverter.ToDto(issue));
            }
        }

        [Route("{issueId:int}/assign-team/{teamId:int}")]
        public IHttpActionResult PutAssignTeam(int issueId, int teamId)
        {
            Issue issue = this.company.Issues.FirstOrDefault(i => i.Id == issueId);
            Team team = this.company.Teams.FirstOrDefault(i => i.Id == teamId);
            if (issue == null || team == null)
            {
                return this.NotFound();
            }
            else
            {
                issue.AssignedTeamId = team.Id;
                return this.Created(this.Request.RequestUri.AbsolutePath + "/" + issue.Id, IssueConverter.ToDto(issue));
            }
        }

        [Route("{id:int}/close-issue")]
        public IHttpActionResult PutCloseIssue(int id)
        {
            Issue issue = this.issues.FirstOrDefault(i => i.Id == id);
            if (issue == null)
            {
                return this.NotFound();
            }
            else
            {
                if (issue.Open)
                {
                    issue.CloseIssue();
                    return this.Ok(IssueConverter.ToDto(issue));
                }
                else
                {
                    return this.BadRequest();
                }
            }
        }

        [Route("{id:int}/add-note")]
        public IHttpActionResult PutAddNote(int id, Notes note)
        {
            Issue issue = this.issues.FirstOrDefault(i => i.Id == id);
            if (issue == null)
            {
                return this.NotFound();
            }
            else
            {
                if (issue.Open)
                {
                    issue.Notes.Add(new Notes(note.Content));
                    return this.Ok(IssueConverter.ToDto(issue));
                }
                else
                {
                    return this.BadRequest();
                }
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Dto.Issue issueData)
        {
            Issue existing = this.issues.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                IssueConverter.PutInDomain(issueData, existing);
                return this.Ok(IssueConverter.ToDto(existing));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Issue existing = this.issues.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                this.issues.Remove(existing);
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}