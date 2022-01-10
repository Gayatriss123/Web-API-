using CustomerServiceWebAPI.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//This is an example of Controller which uses Generic repository to perform CRUD operation in EF.

namespace CustomerServiceWebAPI.Controllers
{
    [RoutePrefix("api/Generic")]
    public class GenericController : ApiController
    {
        private IGenericRepositoryEF<PersonalDataDetail> repository = null;
        public GenericController()
        {
            this.repository = new GenericRepositoryEF<PersonalDataDetail>();
        }
        public GenericController(IGenericRepositoryEF<PersonalDataDetail> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        [Route("")]
        public IEnumerable<PersonalDataDetail> GetAllCustomers()
        {
            return repository.GetAll();
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCustomerById(int id)
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                var entity = repository.GetById(id);
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
                repository.Insert(person);
                repository.Save();

                var message = Created(new Uri(Request.RequestUri + person.Person_ID.ToString()), person);
                return message;
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
                repository.Delete(id);
                repository.Save();
                return Ok();
                
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


                        repository.Update(entity);
                        repository.Save();
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
