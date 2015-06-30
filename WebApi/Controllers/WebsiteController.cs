using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Service.Converters;
using SharedKernel;

namespace WebApi.Controllers
{
    [RoutePrefix("websites")]
    public class WebsiteController : ApiController
    {
        private ISet<Website> websites = DatabaseTest.SampleCompany.OwnedWebsites;

        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(WebsiteConverter.ToDto(this.websites));
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Website website = this.websites.FirstOrDefault(i => i.Id == id);
            if (website == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(WebsiteConverter.ToDto(website));
            }
        }

        [Route("{id:int}/issues")]
        public IHttpActionResult GetIssues(int id)
        {
            Website website = this.websites.FirstOrDefault(i => i.Id == id);
            if (website == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(IssueConverter.ToDto(DatabaseTest.SampleCompany.Issues.Where(i => i.Website == website)));
            }
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            Website website = this.websites.FirstOrDefault(i => i.Name == name);
            if (website == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(WebsiteConverter.ToDto(website));
            }
        }

        [Route]
        public IHttpActionResult Post(Website website)
        {
            website.ValidateNotNullParameter("website");
            this.websites.Add(website);
            return this.Created(this.Request.RequestUri.AbsolutePath + "/" + website.Id, website);
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Website website)
        {
            Website existing = this.websites.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                existing.Put(website);
                return this.Ok(WebsiteConverter.ToDto(existing));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Website existing = this.websites.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                this.websites.Remove(existing);
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}