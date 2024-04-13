using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            using (var ctx = new WebAPIEntities2())
            {
                staffs = ctx.Staffs.ToList();
            }
            if (staffs.Count == 0)
                {
                    return NotFound();
                }

                return Ok(staffs);

            }
        public IHttpActionResult GetStaffById(int staff_id)
        {
            Staffs staffs = null;

            using (var ctx = new WebAPIEntities2())
            {
                staffs = ctx.Staffs.Where(s => s.staff_id == staff_id).FirstOrDefault<staff>();


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

            using (var ctx = new WebAPIEntities2())
            {
                var existingStaffs = ctx.Staffs.Where(s => s.staff_id == staffs.staff_id)
                                                        .FirstOrDefault<Staffs>();

                if (existingStaffs != null)
                {
                    existingStaffs.first_name = staffs.first_name;
                    existingStaffs.last_name = staffs.last_name;
                    existingStaffs.email = staffs.email;
                    existingStaffs.phone = staffs.phone;
                    existingStaffs.active = staffs.active; 
                    existingStaffs.store_id = staffs.store_id;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        public IHttpActionResult Delete(int staff_id)
        {
            if (staff_id <= 0)
                return BadRequest("Not a valid staff id.");

            using (var ctx = new WebAPIEntities2())
            {
                var staffs = ctx.Staffs
                    .Where(s => s.staff_id == staff_id)
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

            using (var ctx = new WebAPIEntities2())
            {
             ctx.Staffs.Add(new Staffs()
                {

                    first_name = staffs.first_name,
                    last_name = staffs.last_name,
                    email = staffs.email,
                    phone = staffs.phone,
                    active = staffs.active,
                    store_id = staffs.store_id,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
