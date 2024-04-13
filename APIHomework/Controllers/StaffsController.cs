using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIHomework.Controllers
{
    public class StaffsController : ApiController
    {
        public IHttpActionResult GetAllStaffs()
        {
            IList<Staffs> staffs = null;

            using (var ctx = new StaffsModel())
            {
                people = ctx.Staffs.ToList();
            }
            if (staffs.Count == 0)
                {
                    return NotFound();
                }

                return Ok(staffs);

            }
        public IHttpActionResult GetStaffById(int Id)
        {
            Staffs staffs = null;

            using (var ctx = new StaffsModel())
            {
                staffs = ctx.People.Where(s => s.id == Id).FirstOrDefault<Staffs>();


                if (staffs == null)
                {
                    return NotFound();
                }

                return Ok(staffs);
            }
        }
        public IHttpActionResult Put(Staffs staffs)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new StaffsModel())
            {
                var existingStaffs = ctx.Staffs.Where(s => s.id == staffs.id)
                                                        .FirstOrDefault<Staffs>();

                if (existingStaffs != null)
                {
                    existingStaffs.Name = persons.Name;
                    existingStaffs.age = persons.age;

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
                return BadRequest("Not a valid staffs id");

            using (var ctx = new StaffsModel())
            {
                var staffs = ctx.Staffs
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.Entry(staffs).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult PostNewStaffs(Staffs staffs)   
       {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new StaffsModel())
            {
                ctx.People.Add(new Person()
                {
                    Name = staffs.Name,
                        age = staffs.age
            });

                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
