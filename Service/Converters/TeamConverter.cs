using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmn = Domain;
using Dto = Service.Dto;

namespace Service.Converters
{
    public static class TeamConverter
    {
        public static Dto.Team ToDto(Dmn.Team domain)
        {
            return new Dto.Team()
            {
                Id = domain.Id,
                Name = domain.Name,
                AssignedIssueIds = domain.AssignedIssues.Select(i => i.Id),
                TeamMemberIds = domain.TeamMembers.Select(i => i.Id),
                Expertise = domain.Expertise
            };
        }

        public static IEnumerable<Dto.Team> ToDto(IEnumerable<Dmn.Team> domain)
        {
            return domain.Select(i => ToDto(i));
        }

        public static Dmn.Team ToNewDmn(Dto.Team dataObject)
        {
            Dmn.Team output = new Dmn.Team(dataObject.Name);
            if (dataObject.TeamMemberIds != null)
            {
                output.AddTeamMembers(Dmn.DatabaseTest.SampleCompany.Agents.Where(i => dataObject.TeamMemberIds.Contains(i.Id)));
            }
            if (dataObject.Expertise != null)
            {
                foreach (Dmn.TypeOfIssue field in dataObject.Expertise)
                {
                    output.Expertise.Add(field);
                }
            }
            return output;
        }

        public static void PutInDomain(Dto.Team teamData, Dmn.Team team)
        {
            team.Name = teamData.Name;
            team.TeamMembers.Clear();
            team.Expertise.Clear();
            foreach (Dmn.TypeOfIssue field in teamData.Expertise)
            {
                team.Expertise.Add(field);
            }
            team.AddTeamMembers(Dmn.DatabaseTest.SampleCompany.Agents.Where(i => teamData.TeamMemberIds.Contains(i.Id)));
        }
    }
}
