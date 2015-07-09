using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

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
        }

        public int Id { get; }
        public string Name { get; set; }
        public ISet<Website> OwnedWebsites { get; }
        public ISet<Team> Teams { get; }
        public ISet<Agent> Agents { get; }
        public ISet<Customer> CustomerAccounts { get; }
        public ISet<Issue> Issues
        {
            get
            {
                return this.GetIssuesFromDatabase();
            }
        }

        public Company SampleCompany()
        {
            Website estateSales = new Website("EstateSales.net", "http://estatesales.net");
            this.OwnedWebsites.Add(estateSales);
            Website anotherWebsite = new Website("anotherWebsite.net", "http://google.com");
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
            return this;
        }

        public ISet<Issue> GetIssuesFromDatabase()
        {
            ISet<Issue> output = new HashSet<Issue>();
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            DbCommand cmd = db.GetSqlStringCommand("SELECT * FROM Issue");
            DataSet ds = db.ExecuteDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int id = (int)row["IssueId"];
                string name = (string)row["Name"];
                DateTime dateRaised = (DateTime)row["DateRaised"];

                object obj = row["DateClosed"];
                DateTime? dateClosed = obj == DBNull.Value ? null : (DateTime?)obj;

                bool open = (bool)row["Open"];
                string description = (string)row["Description"];
                int assignedTeamId = (int)row["AssignedTeamId"];
                int issueTypeId = (int)row["IssueTypeId"];
                int websiteId = (int)row["WebsiteId"];
                int priority = (int)row["Priority"];
                HashSet<Customer> customers = new HashSet<Customer>();
                ////Issue issue = new Issue(id, name, dateRaised, dateClosed, open, this.OwnedWebsites.FirstOrDefault(i => i.Id == websiteId), priority, TypeOfIssue.UI, description, 1, customers);
                ////issue.TeamId = assignedTeamId;
                ////DbCommand noteCmd = db.GetSqlStringCommand(string.Format("Select * FROM Note WHERE IssueId={0}", issue.Id));
                ////DataSet notes = db.ExecuteDataSet(noteCmd);
                ////foreach (DataRow noteRow in notes.Tables[0].Rows)
                ////{
                ////    int noteId = (int)noteRow["NoteId"];
                ////    string content = (string)noteRow["Content"];
                ////    DateTime noteDate = (DateTime)noteRow["Date"];
                ////    issue.Notes.Add(new Notes(noteId, content, noteDate));
                ////}
                ////output.Add(issue);
            }
            return output;
        }

        public void InsertIssue(Issue issue)
        {
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            string sqlString = string.Format("INSERT INTO Issue(Name, Description, AssignedTeamId, IssueTypeId, WebsiteId, Priority) values('{0}', '{1}', {2}, {3}, {4}, {5});", issue.Name, issue.Description, issue.TeamId, '3', issue.Website.Id, issue.Priority);
            DbCommand cmd = db.GetSqlStringCommand(sqlString);
            db.ExecuteNonQuery(cmd);
        }

        public void AddNote(Issue issue, Notes note)
        {
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            string sqlString = string.Format("INSERT INTO Note(Content, IssueId) values('{0}', {1});", note.Content, issue.Id);
            DbCommand cmd = db.GetSqlStringCommand(sqlString);
            db.ExecuteNonQuery(cmd);
        }

        public void CloseIssue(Issue issue)
        {
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            string sqlString = string.Format("UPDATE Issue SET [Open]=0, DateClosed=GETUTCDATE() WHERE IssueId = {0};", issue.Id);
            DbCommand cmd = db.GetSqlStringCommand(sqlString);
            db.ExecuteNonQuery(cmd);
        }

        public void UpdateIssueCommand(Issue issue)
        {
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            DbCommand cmd = db.GetStoredProcCommand("dbo.update_issue");
            db.AddInParameter(cmd, "@IssueId", DbType.Int32, issue.Id);
            db.AddInParameter(cmd, "@Name", DbType.String, issue.Name);
            db.AddInParameter(cmd, "@Description", DbType.String, issue.Description);
            db.AddInParameter(cmd, "@WebsiteId", DbType.Int32, issue.Website.Id);
            db.AddInParameter(cmd, "@AssignedTeamId", DbType.Int32, issue.TeamId);
            db.ExecuteNonQuery(cmd);
        }

        public void UpdateIssue(Issue issue)
        {
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            string sqlString = string.Format("UPDATE ISSUE SET Name='{1}', Description='{2}', WebsiteId={3}, AssignedTeamId={4} WHERE IssueId = {0};", issue.Id, issue.Name, issue.Description, issue.Website.Id, issue.TeamId);
            DbCommand cmd = db.GetSqlStringCommand(sqlString);
            db.ExecuteNonQuery(cmd);
        }

        public void DeleteIssue(Issue issue)
        {
            SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);
            string sqlString = string.Format("DELETE ISSUE WHERE IssueId = {0};", issue.Id);
            DbCommand cmd = db.GetSqlStringCommand(sqlString);
            db.ExecuteNonQuery(cmd);
        }
    }
}
