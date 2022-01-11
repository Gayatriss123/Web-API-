using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CustomerServiceWebAPI.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        //On GetAllCustomers() method basic authentication is implemented, so username and password will required to execute this method
        [HttpGet]
        [Authorize]
        [Route("")]
        public IEnumerable<PersonalDataDetail> GetAllCustomers()
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                return dbcontext.PersonalDataDetails.ToList();
            }
        }
        [HttpGet]
        [Route("List")]
        public IEnumerable<PersonalDataDetail> GetCustomerList()
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                return dbcontext.PersonalDataDetails.ToList();
            }
        }

        /// <summary>
        /// Query String parameter Example 
        /// </summary>
        /// <param name="occupation"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{occupation:alpha}")]
        public HttpResponseMessage LoadCustomerByOccupation(string occupation = "Engineer")
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                switch (occupation.ToLower())
                {
                    case "engineer":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            dbcontext.PersonalDataDetails.Where(e => e.Person_Occupation.ToLower() == "engineer").ToList());
                    case "actor":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            dbcontext.PersonalDataDetails.Where(e => e.Person_Occupation.ToLower() == "actor").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for Occupation must be Engineer or Actor " + occupation + " is invalid.");
                }
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetCustomerById(int id)
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Customer with ID : " + id.ToString() + " not found");
                }
            }
        }

        [HttpPost]
        [Route("Post")]
        public HttpResponseMessage AddNewCustomer([FromBody]PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    dbcontext.PersonalDataDetails.Add(person);
                    dbcontext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, person);
                    message.Headers.Location = new Uri(Request.RequestUri + person.Person_ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Customer with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbcontext.PersonalDataDetails.Remove(entity);
                        dbcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage UpdateCustomerDetails(int id, PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Customer with Id = " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Person_Name = person.Person_Name;
                        entity.Person_Age = person.Person_Age;
                        entity.Person_Occupation = person.Person_Occupation;
                        entity.Person_Mail = person.Person_Mail;


                        dbcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }

}

