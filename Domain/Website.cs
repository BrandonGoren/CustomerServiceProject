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

        public Website(string name, Uri url)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.Url = url;
        }

        public int Id { get; }
        public string Name { get; set; }
        public Uri Url { get; set; }

        public void Put(Website input)
        {
            input.ValidateNotNullParameter("website input");
            this.Name = input.Name;
            this.Url = input.Url;
        }
    }
}
