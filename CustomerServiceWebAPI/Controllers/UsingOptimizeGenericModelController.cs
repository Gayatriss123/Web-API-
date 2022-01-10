using CustomerServiceWebAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//This is an example of Web API controller which uses optimize Generic model for returning the five main properties
//i.e HttpStatusCode, Status, Message, Entity, Exception

namespace CustomerServiceWebAPI.Controllers
{
    [RoutePrefix("api/UsingOptimizeGenericModel")]
    public class UsingOptimizeGenericModelController : ApiController
    {
        private OptimizeGenericModel<PersonalDataDetail> model;
        private OptimizeGenericModel<List<PersonalDataDetail>> modelList;

        [HttpGet]
        [Route("")]
        public OptimizeGenericModel<List<PersonalDataDetail>> GetAllCustomers()
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    modelList = new OptimizeGenericModel<List<PersonalDataDetail>>(HttpStatusCode.Found, "Success",
                        "Data found successfully..", dbcontext.PersonalDataDetails.ToList(), null);

                    return modelList;
                }
            }
            catch (Exception ex)
            {
                modelList = new OptimizeGenericModel<List<PersonalDataDetail>>(HttpStatusCode.NotFound, "Failure",
                        "Data not found..", null, ex);
                return modelList;
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public OptimizeGenericModel<PersonalDataDetail> GetCustomerById(int id)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity != null)
                    {
                        model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.Found, "Success",
                       "Data found successfully..", entity, null);
                    }
                    else
                    {
                        model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.NotFound, "Failure",
                       "Customer with ID : " + id.ToString() + " not found", null, null);
                    }
                    return model;
                }
            }
            catch (Exception ex)
            {
                model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.NotFound, "Failure",
                         "Data not found..", null, ex);
                return model;
            }
        }
        [HttpPost]
        public OptimizeGenericModel<PersonalDataDetail> AddNewCustomer([FromBody]PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    dbcontext.PersonalDataDetails.Add(person);
                    dbcontext.SaveChanges();
                    var entity = dbcontext.PersonalDataDetails.ToList().Last();
                    model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.Created, "Success",
                       "Data Added successfully..", entity, null);
                    return model;
                }
            }
            catch (Exception ex)
            {
                model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.BadRequest, "Failure",
                         "Data does not Inserted", null, ex);
                return model;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public OptimizeGenericModel<List<PersonalDataDetail>> DeleteCustomer(int id)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        modelList = new OptimizeGenericModel<List<PersonalDataDetail>>(HttpStatusCode.NotFound, "Failure",
                       "Customer with Id = " + id.ToString() + " not found to delete", null, null);
                    }
                    else
                    {
                        dbcontext.PersonalDataDetails.Remove(entity);
                        dbcontext.SaveChanges();
                        modelList = new OptimizeGenericModel<List<PersonalDataDetail>>(HttpStatusCode.OK, "Success",
                        "Data Deleted successfully with ID: " + id, dbcontext.PersonalDataDetails.ToList(), null);
                    }
                    return modelList;
                }
            }
            catch (Exception ex)
            {
                modelList = new OptimizeGenericModel<List<PersonalDataDetail>>(HttpStatusCode.BadRequest, "Failure",
                        "Data does not Deleted", null, ex);
                return modelList;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public OptimizeGenericModel<PersonalDataDetail> UpdateCustomerDetails([FromUri] int id, [FromBody]PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.NotFound, "Failure",
                       "Customer with Id = " + id.ToString() + " not found to update", null, null);
                        return model;
                    }
                    else
                    {
                        entity.Person_Name = person.Person_Name;
                        entity.Person_Age = person.Person_Age;
                        entity.Person_Occupation = person.Person_Occupation;
                        entity.Person_Mail = person.Person_Mail;

                        dbcontext.SaveChanges();

                        model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.OK, "Success",
                       "Data Updated successfully..", dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault(), null);

                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                model = new OptimizeGenericModel<PersonalDataDetail>(HttpStatusCode.BadRequest, "Failure",
                          "Data does not Updated", null, ex);
                return model;
            }
        }
    }
}
