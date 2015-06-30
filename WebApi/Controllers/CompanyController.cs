using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("companies")]
    public class CompanyController : ApiController
    {
        [Route]
        public IHttpActionResult Get()
        {
            return this.Ok(DatabaseTest.SampleCompany);
        }

        ////[Route("{id:int}")]
        ////public IHttpActionResult Get(int id)
        ////{
        ////    Customer customer = this.data.FirstOrDefault(i => i.Id == id);
        ////    if (customer == null)
        ////    {
        ////        return this.NotFound();
        ////    }
        ////    else
        ////    {
        ////        return this.Ok(customer);
        ////    }
        ////}

        ////[Route("{name}")]
        ////public IHttpActionResult Get(string name)
        ////{
        ////    Customer customer = this.customers.FirstOrDefault(i => i.Name.ToLower() == name.ToLower());
        ////    if (customer == null)
        ////    {
        ////        return this.NotFound();
        ////    }
        ////    else
        ////    {
        ////        return this.Ok(customer);
        ////    }
        ////}

        ////[Route]
        ////public IHttpActionResult Post(Customer customer)
        ////{
        ////    this.customers.Add(customer);
        ////    return this.Created(this.Request.RequestUri.AbsolutePath + "/" + customer.Id, customer);
        ////}

        ////[Route("{id:int}")]
        ////public IHttpActionResult Delete(int id)
        ////{
        ////    Customer existing = this.customers.FirstOrDefault(i => i.Id == id);
        ////    if (existing == null)
        ////    {
        ////        return this.NotFound();
        ////    }
        ////    else
        ////    {
        ////        this.customers.Remove(existing);
        ////        return this.StatusCode(HttpStatusCode.NoContent);
        ////    }
        ////}
    }
}