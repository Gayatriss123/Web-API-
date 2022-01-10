using CustomerServiceWebAPI.Generic_Repository;
using CustomerServiceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//This is an example of Web API controller which uses Generic model for returning the five main properties
//i.e HttpStatusCode, Status, Message, Entity, Exception


namespace CustomerServiceWebAPI.Controllers
{
    [RoutePrefix("api/CustomerUsingGenericModel")]
    public class CustomerUsingGenericModelController : ApiController
    {
        private GenericModel<PersonalDataDetail> model;
        private GenericModel<List<PersonalDataDetail>> modelList;

        public CustomerUsingGenericModelController()
        {
            this.model = new GenericModel<PersonalDataDetail>();
            this.modelList = new GenericModel<List<PersonalDataDetail>>();
        }

        [HttpGet]
        [Route("")]
        public GenericModel<List<PersonalDataDetail>> GetAllCustomers()
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    modelList.StatusCode = HttpStatusCode.Found;
                    modelList.Status = "Success";
                    modelList.Message = "Data found successfully..";
                    modelList.EntityObject = dbcontext.PersonalDataDetails.ToList();

                    return modelList;
                }
            }
            catch (Exception ex)
            {
                modelList.StatusCode = HttpStatusCode.BadRequest;
                modelList.Status = "Failure";
                modelList.Message = "Data does not found";
                modelList.EntityObject = null;
                modelList.Exception = ex;

                return modelList;
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public GenericModel<PersonalDataDetail> GetCustomerById(int id)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity != null)
                    {
                        model.StatusCode = HttpStatusCode.Found;
                        model.Status = "Success";
                        model.Message = "Data found successfully with ID: " + id;
                        model.EntityObject = entity;
                    }
                    else
                    {
                        model.StatusCode = HttpStatusCode.NotFound;
                        model.Status = "Failure";
                        model.Message = "Customer with ID : " + id.ToString() + " not found";
                        model.EntityObject = null;
                    }
                    return model;
                }
            }
            catch (Exception ex)
            {
                model.StatusCode = HttpStatusCode.BadRequest;
                model.Status = "Failure";
                model.Message = "Customer with ID : " + id.ToString() + " not found";
                model.EntityObject = null;
                model.Exception = ex;

                return model;
            }
        }

        [HttpPost]
        public GenericModel<PersonalDataDetail> AddNewCustomer([FromBody]PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    dbcontext.PersonalDataDetails.Add(person);
                    dbcontext.SaveChanges();

                    model.StatusCode = HttpStatusCode.Created;
                    model.Status = "Success";
                    model.Message = "Data Added successfully";
                    var entity = dbcontext.PersonalDataDetails.ToList().Last();
                    model.EntityObject = entity;

                    return model;
                }
            }
            catch (Exception ex)
            {
                model.StatusCode = HttpStatusCode.BadRequest;
                model.Status = "Failure";
                model.Message = "Data does not Inserted";
                model.EntityObject = null;
                model.Exception = ex;

                return model;
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public GenericModel<List<PersonalDataDetail>> DeleteCustomer(int id)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        modelList.StatusCode = HttpStatusCode.NotFound;
                        modelList.Status = "Failure";
                        modelList.Message = "Customer with Id = " + id.ToString() + " not found to delete";
                        modelList.EntityObject = null;
                    }
                    else
                    {
                        dbcontext.PersonalDataDetails.Remove(entity);
                        dbcontext.SaveChanges();

                        modelList.StatusCode = HttpStatusCode.OK;
                        modelList.Status = "Success";
                        modelList.Message = "Data Deleted successfully with ID: " + id;
                        modelList.EntityObject = dbcontext.PersonalDataDetails.ToList();
                    }
                    return modelList;
                }
            }
            catch (Exception ex)
            {
                modelList.StatusCode = HttpStatusCode.BadRequest;
                modelList.Status = "Failure";
                modelList.Message = "Data does not Deleted";
                modelList.EntityObject = null;
                modelList.Exception = ex;

                return modelList;
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public GenericModel<PersonalDataDetail> UpdateCustomerDetails([FromUri] int id, [FromBody]PersonalDataDetail person)
        {
            try
            {
                using (Intern_DBEntities dbcontext = new Intern_DBEntities())
                {
                    var entity = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    if (entity == null)
                    {
                        model.StatusCode = HttpStatusCode.NotFound;
                        model.Status = "Failure";
                        model.Message = "Customer with Id = " + id.ToString() + " not found to update";
                        model.EntityObject = null;
                    }
                    else
                    {
                        entity.Person_Name = person.Person_Name;
                        entity.Person_Age = person.Person_Age;
                        entity.Person_Occupation = person.Person_Occupation;
                        entity.Person_Mail = person.Person_Mail;
                        
                        dbcontext.SaveChanges();
                        
                        model.StatusCode = HttpStatusCode.OK;
                        model.Status = "Success";
                        model.Message = "Data Added successfully";
                        model.EntityObject = dbcontext.PersonalDataDetails.Where(s => s.Person_ID == id).FirstOrDefault();
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                model.StatusCode = HttpStatusCode.BadRequest;
                model.Status = "Failure";
                model.Message = "Data does not Updated";
                model.EntityObject = null;
                model.Exception = ex;

                return model;
            }
        }

    }
}
