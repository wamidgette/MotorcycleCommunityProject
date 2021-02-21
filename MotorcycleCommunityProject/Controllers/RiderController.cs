using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Results;
using MotorcycleCommunityProject.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Description;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace MotorcycleCommunityProject.Controllers
{   //Rider Controller 
    public class RiderController : Controller
    {
        //Connect to web api using Http client
        private static readonly HttpClient client;
        private JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

        //HTTP Handler 
        static RiderController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("https://localhost:44368/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Rider/List
        public ActionResult List()
        {
            //Request data from API controller via http request 
            string request = "RiderData/GetRiders";
            HttpResponseMessage response = client.GetAsync(request).Result;
            //The IHTTPActionResult should send an OK response as well as a RiderDto object list 
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<RiderDto> ListOfRiders = response.Content.ReadAsAsync<IEnumerable<RiderDto>>().Result;
                return View(ListOfRiders);

            }

            else
            {
                return RedirectToAction("Error");
            }



            /* Previously attempted to access directly - IHTTPActionResult 
            var controller = new RiderDataController();
            var result = controller.GetRiders() as OkNegotiatedContentResult<List<RiderDto>>;
            List<RiderDto> RidersDto = result.Content;
            return View(RidersDto);*/
        }

        public ActionResult Error()
        {
            return View()
        }

        // GET: Rider/Show/5
        public ActionResult Show(int id)
        {
            return View(id);
        }

        
        // GET: Rider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rider/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rider/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rider/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rider/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rider/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
