using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Service.Converters;
using Dto = Service.Dto;

namespace WebApi.Controllers
{
    [RoutePrefix("customers")]
    public class CustomerController : ApiController
    {
        private ISet<Customer> customers = DatabaseTest.SampleCompany.CustomerAccounts;

        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(CustomerConverter.ToDto(this.customers));
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Customer customer = this.customers.FirstOrDefault(i => i.Id == id);
            if (customer == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(CustomerConverter.ToDto(customer));
            }
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            Customer customer = this.customers.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
            if (customer == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(CustomerConverter.ToDto(customer));
            }
        }

        [Route("{id:int}/issues")]
        public IHttpActionResult GetIssues(int id)
        {
            Customer customer = this.customers.FirstOrDefault(i => i.Id == id);
            if (customer == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(IssueConverter.ToDto(customer.Issues));
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Dto.Customer customer)
        {
            Customer existing = this.customers.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                CustomerConverter.PutInDomain(customer, existing);
                return this.Ok(CustomerConverter.ToDto(existing));
            }
        }

        [Route]
        public IHttpActionResult Post(Dto.Customer customerData)
        {
            Customer customer = CustomerConverter.ToNewDmn(customerData);
            this.customers.Add(customer);
            return this.Created(this.Request.RequestUri.AbsolutePath + "/" + customer.Id, CustomerConverter.ToDto(customer));
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Customer existing = this.customers.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return this.NotFound();
            }
            else
            {
                this.customers.Remove(existing);
                return this.StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}