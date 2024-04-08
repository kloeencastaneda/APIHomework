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

            // GET: Student
         
                IEnumerable<Person> person = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1126/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("Person");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Person>>();
                        readTask.Wait();

                        person = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                    //log response status here..

                    person = Enumerable.Empty<Person>();

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(person);
            
        }
     

        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Person person)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1126/api/student");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Person>("person", person);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(person);
        }
    }
}
