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

        public int Id { get; }
        public string Name { get; set; }
        public Dictionary<MethodOfCommunication, string> ContactInfo { get; set; }
        public MethodOfCommunication PreferredCommunicationMethod { get; set; }
        public IEnumerable<Issue> Issues
        {
            get
            {
                return DatabaseTest.SampleCompany.Issues.Where(i => i.AffectedCustomers.Contains(this));
            }
        }

        public void Put(Customer input)
        {
            input.ValidateNotNullParameter("customer");
            this.Name = input.Name;
            this.ContactInfo = input.ContactInfo;
            this.PreferredCommunicationMethod = input.PreferredCommunicationMethod;
        }
    }
}
