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
        private EarlyBirdsContext context = new EarlyBirdsContext();

        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(IssueConverter.ToDto(this.context.Issues.ToList()));
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Issue issue = this.context.Issues.FirstOrDefault(i => i.Id == id);
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
            return this.Ok(IssueConverter.ToDto(this.context.Issues.Where(i => i.WebsiteId == websiteId)).ToList());
        }

        [Route("{openOrClosed}")]
        public IHttpActionResult Get(string openOrClosed)
        {
            if (openOrClosed == "open" || openOrClosed == "closed")
            {
                bool open = openOrClosed == "open";
                Issue issue = this.context.Issues.FirstOrDefault(i => i.Open == open);
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
            this.context.Issues.Add(issue);
            this.context.SaveChanges();
            return this.Created(this.Request.RequestUri.AbsolutePath + "/" + issue.Id, IssueConverter.ToDto(issue));
        }

        [Route("{issueId:int}/add-affected-customer")]
        public IHttpActionResult PutAddAffectedCustomer(int issueId, int customerId)
        {
            Issue issue = this.context.Issues.FirstOrDefault(i => i.Id == issueId);
            Customer customer = this.context.Customers.FirstOrDefault(i => i.Id == customerId);
            if (issue == null || customer == null)
            {
                return this.NotFound();
            }
            else
            {
                issue.AffectedCustomers.Add(customer);
                this.context.SaveChanges();
                return this.Created(this.Request.RequestUri.AbsolutePath + "/" + issue.Id, IssueConverter.ToDto(issue));
            }
        }

        [Route("{issueId:int}/assign-team/{teamId:int}")]
        public IHttpActionResult PutAssignTeam(int issueId, int teamId)
        {
            Issue issue = this.context.Issues.FirstOrDefault(i => i.Id == issueId);
            Team team = this.context.Teams.FirstOrDefault(i => i.Id == teamId);
            if (issue == null || team == null)
            {
                return this.NotFound();
            }
            else
            {
                issue.TeamId = team.Id;
                this.context.SaveChanges();
                return this.Created(this.Request.RequestUri.AbsolutePath + "/" + issue.Id, IssueConverter.ToDto(issue));
            }
        }

        [Route("{id:int}/close-issue")]
        public IHttpActionResult PutCloseIssue(int id)
        {
            Issue issue = this.context.Issues.FirstOrDefault(i => i.Id == id);
            if (issue == null)
            {
                return this.NotFound();
            }
            else
            {
                if (issue.Open)
                {
                    issue.CloseIssue();
                    this.context.SaveChanges();
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
            Issue issue = this.context.Issues.FirstOrDefault(i => i.Id == id);
            if (issue == null)
            {
                return this.NotFound();
            }
            else
            {
                if (issue.Open)
                {
                    note.Date = System.DateTime.Now;
                    issue.Notes.Add(note);
                    this.context.Notes.Add(note);
                    this.context.SaveChanges();
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
            Issue existing = this.context.Issues.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                IssueConverter.PutInDomain(issueData, existing);
                this.context.SaveChanges();
                return this.Ok(IssueConverter.ToDto(existing));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Issue existing = this.context.Issues.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                this.context.Issues.Remove(existing);
                this.context.SaveChanges();
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}