using Domain;
using Service.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Dto = Service.Dto;

namespace WebApi.Controllers
{
    [RoutePrefix("teams")]
    public class TeamController : ApiController
    {
        private EarlyBirdsContext context = new EarlyBirdsContext();

        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(TeamConverter.ToDto(this.context.Teams.ToList()));
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Team team = this.context.Teams.FirstOrDefault(i => i.Id == id);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(TeamConverter.ToDto(team));
            }
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            Team team = this.context.Teams.FirstOrDefault(i => i.Name == name);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(TeamConverter.ToDto(team));
            }
        }

        [Route("{id:int}/issues")]
        public IHttpActionResult GetIssues(int id)
        {
            Team team = this.context.Teams.FirstOrDefault(i => i.Id == id);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(IssueConverter.ToDto(team.AssignedIssues).ToList());
            }
        }

        [Route("{id:int}/agents")]
        public IHttpActionResult GetAgents(int id)
        {
            Team team = this.context.Teams.FirstOrDefault(i => i.Id == id);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(AgentConverter.ToDto(team.TeamMembers).ToList());
            }
        }

        [Route("{id:int}/issues/{stateString}")]
        public IHttpActionResult GetFilteredIssues(int id, string stateValue)
        {
            bool state = stateValue == "open";
            Team team = this.context.Teams.FirstOrDefault(i => i.Id == id);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(IssueConverter.ToDto(team.AssignedIssues.Where(i => i.Open == state)).ToList());
            }
        }

        [Route]
        public IHttpActionResult Post(Dto.Team teamData)
        {
            Team team = TeamConverter.ToNewDmn(teamData);
            if (team != null)
            {
                this.context.Teams.Add(team);
                this.context.SaveChanges();
                return this.Created(this.Request.RequestUri.AbsolutePath + "/" + team.Id, TeamConverter.ToDto(team));
            }
            else
            {
                return this.BadRequest();
            }
        }

        // todo: change to put
        [Route("{teamId:int}/add-member")]
        public IHttpActionResult PostAddTeamMember(int teamId, Dto.Agent agentData)
        {
            Team team = this.context.Teams.FirstOrDefault(i => i.Id == teamId);
            Agent agent = this.context.Agents.FirstOrDefault(i => i.Id == agentData.Id);
            if (team == null || agent == null)
            {
                return this.NotFound();
            }
            else
            {
                team.TeamMembers.Add(agent);
                return this.Created(this.Request.RequestUri.AbsolutePath + "/" + teamId, TeamConverter.ToDto(team));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Dto.Team teamData)
        {
            Team existing = this.context.Teams.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                TeamConverter.PutInDomain(teamData, existing);
                this.context.SaveChanges();
                return this.Ok(TeamConverter.ToDto(existing));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Team existing = this.context.Teams.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                this.context.Teams.Remove(existing);
                this.context.SaveChanges();
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}