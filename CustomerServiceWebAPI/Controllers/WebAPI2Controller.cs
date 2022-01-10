using CustomerWebAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


//Here in this controller i used IHttpActionResult which is introduced in Web API 2 and is a new type that a controller action method can return
//Benefits of using IHttpActionResult instead of HttpResponseMessage are :
//1)The code is cleaner and easier to read
//2)Unit testing Controller action is much simpler



namespace CustomerServiceWebAPI.Controllers
{
    [RoutePrefix("api/WebAPI2")]
    public class WebAPI2Controller : ApiController
    {
        [HttpGet]
        [Route("")]
        [BasicAuthenticationFilter]
        public IEnumerable<PersonalDataDetail> GetAllCustomers()
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                return dbcontext.PersonalDataDetails.ToList();
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCustomerById(int id)
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                if (entity != null)
                {
                    return Ok(entity);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Customer with ID : " + id.ToString() + " not found");
                }
            }
        }

        [HttpPost]
        public IHttpActionResult AddNewCustomer([FromBody]PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    dbcontext.PersonalDataDetails.Add(person);
                    dbcontext.SaveChanges();

                    var message = Created(new Uri(Request.RequestUri + person.Person_ID.ToString()), person);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Customer with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbcontext.PersonalDataDetails.Remove(entity);
                        dbcontext.SaveChanges();
                        return Ok(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomerDetails(int id, PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Customer with Id = " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Person_Name = person.Person_Name;
                        entity.Person_Age = person.Person_Age;
                        entity.Person_Occupation = person.Person_Occupation;
                        entity.Person_Mail = person.Person_Mail;


                        dbcontext.SaveChanges();
                        return Ok(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}

