using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;
namespace Domain
{
    public class Team
    {
        private static int IdCount = 0;

        public Team(string name)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.TeamMembers = new HashSet<Agent>();
            this.Expertise = new HashSet<TypeOfIssue>();
        }

        public int Id { get; }
        public string Name { get; set; }
        public ISet<Agent> TeamMembers { get; private set; }
        public ISet<TypeOfIssue> Expertise { get; private set; }

        public IEnumerable<Issue> AssignedIssues
        {
            get
            {
                return DatabaseTest.SampleCompany.Issues.Where(i => i.AssignedTeamId == this.Id);
            }
        }

        public void AddTeamMembers(IEnumerable<Agent> agents)
        {
            agents.ValidateNotNullParameter("agents");
            foreach (Agent agent in agents)
            {
                this.TeamMembers.Add(agent);
            }
        }

        public void Put(Team input)
        {
            input.ValidateNotNullParameter("team input");
            this.Name = input.Name;
            this.TeamMembers = input.TeamMembers;
            this.Expertise = input.Expertise;
        }
    }
}
