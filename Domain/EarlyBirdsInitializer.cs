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
            ////List<Issue> issues = GetIssues();
            ////issues.ForEach(i => context.Issues.Add(i));
            ////context.SaveChanges();

            ////List<Notes> notes = GetNotes();
            ////notes.ForEach(i => context.Notes.Add(i));
            ////context.SaveChanges();

            List<Website> websites = GetWebsites();
            websites.ForEach(i => context.Websites.Add(i));
            context.SaveChanges();
        }

        private static List<Website> GetWebsites()
        {
            List<Website> output = new List<Website>();
            output.Add(new Website("test-website.com", "http://test-website.com"));
            return output;
        }
    }
}