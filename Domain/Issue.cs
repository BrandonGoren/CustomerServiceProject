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

        public Issue(string name, int websiteId, int priority, TypeOfIssue type, string description, Customer customer = null)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.DateRaised = DateTime.Now;
            this.WebsiteId = websiteId;
            this.Priority = priority;
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

        public Issue() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateRaised { get; set; }
        public DateTime? DateClosed { get; private set; }
        public bool Open { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public virtual ICollection<Customer> AffectedCustomers { get; private set; }
        public virtual ICollection<WebBrowser> AffectedBrowsers { get; private set; }
        public virtual ICollection<Notes> Notes { get; private set; }
        public int? TeamId { get; set; }
        public TypeOfIssue IssueType { get; set; }
        public virtual Website Website { get; set; }
        public int WebsiteId { get; set; }

        public void Put(Issue input)
        {
            input.ValidateNotNullParameter("issue input");
            this.Name = input.Name;
            this.DateRaised = input.DateRaised;
            this.DateClosed = input.DateClosed;
            this.Open = input.Open;
            this.Description = input.Description;
            this.Priority = input.Priority;
            this.AffectedCustomers = input.AffectedCustomers;
            this.Notes = input.Notes;
            this.TeamId = input.TeamId;
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
