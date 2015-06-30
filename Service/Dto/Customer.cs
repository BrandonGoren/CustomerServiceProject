using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class Customer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Domain.MethodOfCommunication, string> ContactInfo { get; set; }
        public Domain.MethodOfCommunication PreferredCommunicationMethod { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
    }
}
