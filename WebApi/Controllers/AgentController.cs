using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Service.Converters;
using Dto = Service.Dto;

namespace WebApi.Controllers
{
    [RoutePrefix("agents")]
    public class AgentController : ApiController
    {
        private EarlyBirdsContext context = new EarlyBirdsContext();

        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(AgentConverter.ToDto(this.context.Agents.ToList()));
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Agent agent = this.context.Agents.FirstOrDefault(i => i.Id == id);
            if (agent == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(AgentConverter.ToDto(agent));
            }
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            Agent agent = this.context.Agents.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
            if (agent == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(AgentConverter.ToDto(agent));
            }
        }

        [Route("{id:int}/teams")]
        public IHttpActionResult GetTeams(int id)
        {
            Agent agent = this.context.Agents.FirstOrDefault(i => i.Id == id);
            if (agent == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(TeamConverter.ToDto(agent.Teams));
            }
        }

        [Route("{id:int}/issues")]
        public IHttpActionResult GetIssues(int id)
        {
            Agent agent = this.context.Agents.FirstOrDefault(i => i.Id == id);
            if (agent == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(IssueConverter.ToDto(agent.Issues));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Dto.Agent agentData)
        {
            Agent existing = this.context.Agents.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                AgentConverter.PutInDomain(agentData, existing);
                this.context.SaveChanges();
                return this.Ok(AgentConverter.ToDto(existing));
            }
        }

        [Route]
        public IHttpActionResult Post(Dto.Agent agentData)
        {
            Agent agent = AgentConverter.ToNewDmn(agentData);
            this.context.Agents.Add(agent);
            this.context.SaveChanges();
            return this.Created(this.Request.RequestUri.AbsolutePath + "/" + agent.Id, AgentConverter.ToDto(agent));
        }
        
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Agent existing = this.context.Agents.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                this.context.Agents.Remove(existing);
                this.context.SaveChanges();
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}