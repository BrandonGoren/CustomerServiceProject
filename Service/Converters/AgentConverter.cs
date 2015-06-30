using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmn = Domain;
using Dto = Service.Dto;

namespace Service.Converters
{
    public static class AgentConverter
    {
        public static Dto.Agent ToDto(Dmn.Agent domain)
        {
            return new Dto.Agent()
            {
                Id = domain.Id,
                Name = domain.Name,
                Email = domain.Email,
                TeamIds = domain.Teams.Select(i => i.Id), 
                IssueIds = domain.Issues.Select(i => i.Id)
            };
        }

        public static IEnumerable<Dto.Agent> ToDto(IEnumerable<Dmn.Agent> domain)
        {
            return domain.Select(i => ToDto(i));
        }

        public static Dmn.Agent ToNewDmn(Dto.Agent dataObject)
        {
            return new Dmn.Agent(dataObject.Name, dataObject.Email);

        }

        public static void PutInDomain(Dto.Agent agentData, Dmn.Agent agent)
        {
            agent.Name = agentData.Name;
            agent.Email = agentData.Email;
        }
    }
}