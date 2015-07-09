using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain
{
    public class Website
    {
        private static int IdCount = 0;

        public Website(string name, string url)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.Url = url;
        }

        public Website() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public void Put(Website input)
        {
            input.ValidateNotNullParameter("website input");
            this.Name = input.Name;
            this.Url = input.Url;
        }
    }
}
