using CustomerServiceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Here i used Attribute Routing by using [Route] attribute
//Attribute routing gives more control over the URIs than conventional based routing
//Creating URI patterns like hierarchies of resources is very difficult with conventional based routing
//Here i used [RoutePrefix] attribute to specify the "Common Route Prefix" at the controller level to eliminate the need to repeat that common route prefix on every controller action method
//Here i also used Attribute Routing Constraint

namespace CustomerServiceWebAPI.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        //List of Students
        static List<Students> students = new List<Students>()
        {
            new Students() {Id = 1, Name = "Gayatri"},
            new Students() {Id = 2, Name = "Samreen"},
            new Students() {Id = 3, Name = "Siya"}
        };

        //[Route("api/Students")]
        [Route("")]
        public IEnumerable<Students> GetAllStudents()
        {
            return students;
        }


        [Route("~/api/Teachers")]
        public IEnumerable<Teachers> GetAllTeachers()
        {
            List<Teachers> teachers = new List<Teachers>()
            {
                new Teachers() {Id = 1, Name = "Radhika"},
                new Teachers() {Id = 2, Name = "Kanika"},
                new Teachers() {Id = 3, Name = "Pruvika"}
            };
            return teachers;
        }
        //[Route("api/Students/{id}")]
        //attribute routing contraints
        [Route("{id:int:range(1,3)}")]
        public Students GetStudentById(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        [Route("{name:alpha}")]
        public Students GetStudentByName(string name)
        {
            return students.FirstOrDefault(s => s.Name == name);
        }

        //[Route("api/Students/{id}/courses")]
        [Route("{id}/courses")]
        public IEnumerable<string> GetStudentCourses(int id)
        {
            if (id == 1)
            {
                return new List<String>() { "C#", "Asp.Net", "SQL Server" };
            }
            else if (id == 2)
            {
                return new List<String>() { "Asp.Net Web API", "Asp.Net", "SQL Server" };
            }
            else
            {
                return new List<String>() { "Asp.Net MVC", "WCF", "SQL Server" };
            }
        }
        [HttpPost]
        [Route("Post")]
        public HttpResponseMessage AddNewStudent([FromBody]Students student)
        {
            students.Add(student);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("GetStudentById", new { id = student.Id }));
            return response;
        }
    }
}
