using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace APIHomework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            // GET: Staffs

            IEnumerable<Staffs> staffs = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1126/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Staffs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Staffs>>();
                    readTask.Wait();

                    staffs = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    staffs = Enumerable.Empty<Staffs>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(staffs);

        }

        public ActionResult create(Staffs staffs)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1126/api/Staffs");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Staffs>("staffs", staffs);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(staffs);
        }


        public ActionResult Edit(Staffs staffs)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1126/api/staffs");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Staffs>("staffs", staffs);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(staffs);
        }

        public ActionResult Delete(int staff_id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1126/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("staff/" + staff_id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }


    }
}

