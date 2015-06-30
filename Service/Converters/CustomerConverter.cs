using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmn = Domain;
using Dto = Service.Dto;

namespace Service.Converters
{
    public static class CustomerConverter
    {
        public static Dto.Customer ToDto(Dmn.Customer domain)
        {
            return new Dto.Customer
            {
                Id = domain.Id,
                Name = domain.Name,
                ContactInfo = domain.ContactInfo,
                Issues = domain.Issues.Select(i => IssueConverter.ToDto(i)),
                PreferredCommunicationMethod = domain.PreferredCommunicationMethod
            };
        }

        public static IEnumerable<Dto.Customer> ToDto(IEnumerable<Dmn.Customer> domain)
        {
            return domain.Select(i => ToDto(i));
        }

        public static Dmn.Customer ToNewDmn(Dto.Customer dataObject)
        {
            return new Dmn.Customer(dataObject.Name)
            {
                ContactInfo = dataObject.ContactInfo,
                PreferredCommunicationMethod = dataObject.PreferredCommunicationMethod
            };
        }

        public static void PutInDomain(Dto.Customer customerData, Dmn.Customer customer)
        {
            customer.Name = customerData.Name;
            customer.ContactInfo = customerData.ContactInfo;
            customer.PreferredCommunicationMethod = customerData.PreferredCommunicationMethod;
        }
    }
}
