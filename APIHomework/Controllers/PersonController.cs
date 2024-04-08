using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIHomework.Controllers
{
    public class PersonController : ApiController
    {
        public IHttpActionResult GetAllStudents()
        {
            IList<Person> people = null;

            using (var ctx = new APIHomeworkEntities())
            {
                people = ctx.People.ToList();
            }
            if (people.Count == 0)
                {
                    return NotFound();
                }

                return Ok(people);

            }
        public IHttpActionResult GetPersonById(int Id)
        {
            Person persons = null;

            using (var ctx = new APIHomeworkEntities())
            {
                persons = ctx.People.Where(s => s.id == Id).FirstOrDefault<Person>();


                if (persons == null)
                {
                    return NotFound();
                }

                return Ok(persons);
            }
        }
        public IHttpActionResult Put(Person persons)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new APIHomeworkEntities())
            {
                var existingPerson = ctx.People.Where(s => s.id == persons.id)
                                                        .FirstOrDefault<Person>();

                if (existingPerson != null)
                {
                    existingPerson.Name = persons.Name;
                    existingPerson.age = persons.age;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid persons id");

            using (var ctx = new  APIHomeworkEntities())
            {
                var persons = ctx.People
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.Entry(persons).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult PostNewPerson(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new APIHomeworkEntities())
            {
                ctx.People.Add(new Person()
                {
                    Name = person.Name,
                        age = person.age
            });

                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
