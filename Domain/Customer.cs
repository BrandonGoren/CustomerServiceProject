using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain
{
    public class Customer
    {
        private static int IdCount = 0;

        public Customer(string name)
        {
            this.Id = ++IdCount;
            this.Name = name;
            this.ContactInfo = new Dictionary<MethodOfCommunication, string>();
        }

        public Customer() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<MethodOfCommunication, string> ContactInfo { get; set; }
        public MethodOfCommunication PreferredCommunicationMethod { get; set; }
        public virtual IEnumerable<Issue> Issues { get; private set; }

        public void Put(Customer input)
        {
            input.ValidateNotNullParameter("customer");
            this.Name = input.Name;
            this.ContactInfo = input.ContactInfo;
            this.PreferredCommunicationMethod = input.PreferredCommunicationMethod;
        }
    }
}
