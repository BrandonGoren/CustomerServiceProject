using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain
{
    public class Issue
    {
        private static int IdCount = 0;

        public Issue(string name, Website website, TypeOfIssue type, string description, Customer customer = null)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.DateRaised = DateTime.Now;
            this.Website = website;
            this.IssueType = type;
            this.Description = description;
            this.AffectedBrowsers = new HashSet<WebBrowser>();
            this.AffectedCustomers = new HashSet<Customer>();
            this.Notes = new List<Notes>();
            if (customer != null)
            {
                this.AffectedCustomers.Add(customer);
            }
            this.Open = true;
        }

        public Issue(int id, string name, DateTime dateRaised, DateTime? dateClosed, bool open, Website website, TypeOfIssue type, string description, int assignedTeamId, HashSet<Customer> customers)
        {
            this.Id = id;
            this.Name = name;
            this.Open = open;
            this.DateRaised = dateRaised;
            this.DateClosed = dateClosed;
            this.Website = website;
            this.IssueType = type;
            this.Description = description;
            this.AssignedTeamId = assignedTeamId;
            this.AffectedBrowsers = new HashSet<WebBrowser>();
            this.AffectedCustomers = new HashSet<Customer>(customers);
            this.Notes = new List<Notes>();
        }

        public int Id { get; }
        public string Name { get; set; }
        public DateTime DateRaised { get; private set; }
        public DateTime? DateClosed { get; private set; }
        public bool Open { get; set; }
        public string Description { get; set; }
        public ISet<Customer> AffectedCustomers { get; private set; }
        public ISet<WebBrowser> AffectedBrowsers { get; private set; }
        public IList<Notes> Notes { get; private set; }
        public int? AssignedTeamId { get; set; }
        public TypeOfIssue IssueType { get; set; }
        public Website Website { get; set; }

        public void Put(Issue input)
        {
            input.ValidateNotNullParameter("issue input");
            this.Name = input.Name;
            this.DateRaised = input.DateRaised;
            this.DateClosed = input.DateClosed;
            this.Open = input.Open;
            this.Description = input.Description;
            this.AffectedCustomers = input.AffectedCustomers;
            this.Notes = input.Notes;
            this.AssignedTeamId = input.AssignedTeamId;
            this.IssueType = input.IssueType;
            this.Website = input.Website;
        }

        public void AddAffectedBrowsers(IEnumerable<WebBrowser> affectedBrowsers)
        {
            affectedBrowsers.ValidateNotNullParameter("affected browsers");
            foreach (WebBrowser browser in affectedBrowsers)
            {
                this.AffectedBrowsers.Add(browser);
            }
        }

        public void AddAffectedCustomers(IEnumerable<Customer> customers)
        {
            customers.ValidateNotNullParameter("customers");
            foreach (Customer customer in customers)
            {
                this.AffectedCustomers.Add(customer);
            }
        }

        public void AddNotes(IEnumerable<Notes> notes)
        {
            notes.ValidateNotNullParameter("notes");
            foreach (Notes note in notes)
            {
                this.Notes.Add(note);
            }
        }

        public void CloseIssue()
        {
            this.DateClosed = DateTime.Now;
            this.Open = false;
        }
    }
}
