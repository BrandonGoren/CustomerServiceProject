using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain
{
    public class Agent
    {
        private static int IdCount = 0;

        public Agent(string name, string email)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.Email = email;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Team> Teams
        {
            get
            {
                return DatabaseTest.SampleCompany.Teams.Where(i => i.TeamMembers.Contains(this));
            }
        }
        public IEnumerable<Issue> Issues
        {
            get
            {
                return DatabaseTest.SampleCompany.Issues.Where(i => this.Teams.Any(j => j.Id == i.AssignedTeamId));
            }
        }

        public void Put(Agent input)
        {
            input.ValidateNotNullParameter("agent");
            this.Name = input.Name;
            this.Email = input.Email;
        }
    }
}
