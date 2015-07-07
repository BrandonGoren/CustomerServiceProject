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
        public static Company SampleCompany = new Company("Vintage Software").SampleCompany();
    }
}