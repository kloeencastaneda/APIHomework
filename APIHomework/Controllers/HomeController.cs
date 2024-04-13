using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
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
                    var responseTask = client.GetAsync("Person");
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
     

        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Staffs staffs)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1126/api/student");

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
    }
}
