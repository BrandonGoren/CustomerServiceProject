using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Company
    {
        private static int IdCount = 0;

        public Company(string name)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.OwnedWebsites = new HashSet<Website>();
            this.Teams = new HashSet<Team>();
            this.Agents = new HashSet<Agent>();
            this.CustomerAccounts = new HashSet<Customer>();
            this.Issues = new HashSet<Issue>();
        }

        public int Id { get; }
        public string Name { get; set; }
        public ISet<Website> OwnedWebsites { get; }
        public ISet<Team> Teams { get; }
        public ISet<Agent> Agents { get; }
        public ISet<Customer> CustomerAccounts { get; }
        public ISet<Issue> Issues { get; }

        public Company SampleCompany()
        {
            Website estateSales = new Website("EstateSales.net", new Uri("http://estatesales.net"));
            this.OwnedWebsites.Add(estateSales);
            Website anotherWebsite = new Website("anotherWebsite.net", new Uri("http://estatesales.net"));
            this.OwnedWebsites.Add(anotherWebsite);
            Team ATeam = new Team("A Team");
            Team BTeam = new Team("B Team");
            this.Teams.Add(ATeam);
            this.Teams.Add(BTeam);
            Agent brandon = new Agent("Brandon Goren", "brandon.goren@wustl.edu");
            Agent paul = new Agent("Paul", "p@vintage.com");
            this.Agents.Add(paul);
            this.Agents.Add(brandon);
            ATeam.TeamMembers.Add(brandon);
            this.CustomerAccounts.Add(new Customer("Something"));
            this.CustomerAccounts.Add(new Customer("Something Else"));
            this.CustomerAccounts.Add(new Customer("Blah Blah Blah"));
            Customer homer = new Customer("Homer");
            homer.ContactInfo[MethodOfCommunication.Email] = "homer.j.simpson@egt.com";
            homer.PreferredCommunicationMethod = MethodOfCommunication.Email;
            this.CustomerAccounts.Add(homer);
            this.CustomerAccounts.Add(new Customer("Marge"));
            this.CustomerAccounts.Add(new Customer("Bart"));
            Customer bort = new Customer("Bort");
            this.CustomerAccounts.Add(bort);
            this.CustomerAccounts.Add(new Customer("Lisa"));
            Issue testIssue = new Issue("Hello World", estateSales, TypeOfIssue.Database, "hello world", bort);
            testIssue.Notes.Add(new Notes("this is a note"));
            Issue doh = new Issue("Closed Issue", estateSales, TypeOfIssue.Database, "This should be closed", homer);
            testIssue.AssignedTeamId = ATeam.Id;
            doh.AssignedTeamId = ATeam.Id;
            this.Issues.Add(testIssue);
            this.Issues.Add(doh);

            //DatabaseTest.GetIssues().ToList().ForEach(i => this.Issues.Add(i));
            doh.CloseIssue();
            return this;
        }
    }
}
