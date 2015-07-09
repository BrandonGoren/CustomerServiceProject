using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Domain
{
    public class EarlyBirdsInitializer : DropCreateDatabaseIfModelChanges<EarlyBirdsContext>
    {
        protected override void Seed(EarlyBirdsContext context)
        {
            List<Website> websites = GetWebsites();
            websites.ForEach(i => context.Websites.Add(i));
            context.SaveChanges();

            List<Team> teams = GetTeams();
            teams.ForEach(i => context.Teams.Add(i));
            context.SaveChanges();

            List<Issue> issues = GetIssues();
            issues.ForEach(i => context.Issues.Add(i));
            context.SaveChanges();
        }

        private static List<Website> GetWebsites()
        {
            List<Website> output = new List<Website>();
            output.Add(new Website("test-website.com", "http://test-website.com"));
            return output;
        }

        private static List<Team> GetTeams()
        {
            List<Team> output = new List<Team>();
            output.Add(new Team("A Team"));
            return output;
        }

        private static List<Issue> GetIssues()
        {
            List<Issue> output = new List<Issue>();
            output.Add(new Issue("test issue", 1, 4, TypeOfIssue.Database, "sample description"));
            return output;
        }
    }
}