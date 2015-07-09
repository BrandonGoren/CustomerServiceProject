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
            this.TeamMembers = new List<Agent>();
            this.Expertise = new HashSet<TypeOfIssue>();
            this.AssignedIssues = new List<Issue>();
        }

        public Team() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Agent> TeamMembers { get; protected set; }
        public virtual ICollection<TypeOfIssue> Expertise { get; protected set; }
        public virtual ICollection<Issue> AssignedIssues { get; protected set; }

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
