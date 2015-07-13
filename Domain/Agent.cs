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
            this.Teams = new List<Team>();
            this.Issues = new List<Issue>();
        }

        public Agent() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Team> Teams { get; private set;  }
        public virtual ICollection<Issue> Issues { get; private set; }

        public void Put(Agent input)
        {
            input.ValidateNotNullParameter("agent");
            this.Name = input.Name;
            this.Email = input.Email;
        }
    }
}
