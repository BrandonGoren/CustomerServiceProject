using Facebook;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    public class Class1
    {

        static public void doThis()
        {
            var client = new FacebookClient();
            dynamic me = client.Get("4");
            string firstName = me.first_name;
            string lastName = me.last_name;
        }

        //public void UserInfo()
        //{
        //    var accessToken = Session["AccessToken"].ToString();
        //    var client = new FacebookClient(accessToken);
        //    dynamic result = client.Get("me", new { fields = "name,id" });
        //    string id = result.id;
        //    string name = result.name;
        //}
    }
}
