using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Domain
{
    public static class DatabaseTest
    {
        private static readonly SqlDatabase db = new SqlDatabase(ConfigurationManager.ConnectionStrings["EarlyBirds"].ConnectionString);

        public static Company SampleCompany = new Company("Vintage Software").SampleCompany();

        public static IEnumerable<Issue> GetIssues()
        {
            DbCommand cmd = db.GetSqlStringCommand("SELECT * FROM Issues");
            DataSet ds = db.ExecuteDataSet(cmd);

            int firstId = (int)ds.Tables[0].Rows[0]["Id"];

            return null;
        }
    }
}