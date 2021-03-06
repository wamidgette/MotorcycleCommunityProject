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
        }


        public ActionResult Error()
        {
            return View();
        }


        // GET: Rider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rider NewRider)
        {
            //Serialize method returns the object as a Json object - otherwise no way to see contents
            Debug.WriteLine("NEWRIDER OBJECT: " + JsSerializer.Serialize(NewRider));
            //string to send request to
            string requestAddress = "RiderData/addRider";
            //Create content which sends the NewRiderInfo as a Json object
            HttpContent content = new StringContent(JsSerializer.Serialize(NewRider));
            //Headers are message headers that preceed the http message content (the json object).
            //Set the headervalue in  "content" to json
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //Send the data object to the request address and store Result in HttpResponseMessage 
            HttpResponseMessage response = client.PostAsync(requestAddress, content).Result;
            Debug.WriteLine("THIS IS THE SERVER RESPONSE: " + response);
            //if response is success status code, display the details of the rider in the "Show" view
            if (response.IsSuccessStatusCode)
            {
                //reads riderId from response and sends to show method
                int RiderId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Show", new { id = RiderId });
            }

            else
            {
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// The show method takes an ID parameter sends the id to the riderdata/findRider method 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Show view, sending a riderDto object corresponding to that Id </returns>
        // GET: Rider/Show/5
        [HttpGet]
        public ActionResult Show(int id)
        {
            Debug.WriteLine("YOU ARE IN THE SHOW CONTROLLER");
            //resquest from the findRider controller the team with associated id
            string requestAddress = "RiderData/findRider/" + id;
            HttpResponseMessage response = client.GetAsync(requestAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                //Will later have to also request trip data so the page will display the trips a rider is on
                //Need viewmodel for this
                RiderDto Rider = response.Content.ReadAsAsync<RiderDto>().Result;
                return View(Rider);
            }

            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //logic follows 
            string requestAddress = "RiderData/findRider/" + id;
            HttpResponseMessage response = client.GetAsync(requestAddress).Result;

            if (response.IsSuccessStatusCode)
            {
                //Will later have to also request trip data so the page will display the trips a rider is on
                //Need viewmodel for this
                RiderDto Rider = response.Content.ReadAsAsync<RiderDto>().Result;
                return View(Rider);
            }

            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rider updatedRider)
        {
            //I dont see the point in putting a separate int id parameter in here like was done in the example, doesnt the int Id come from the NewRider.RiderId anyway?
            //Ask Christine to clarify this 
            string requestAddress = "RiderData/updateRider";
            Debug.WriteLine("NEW RIDER DATA: " + JsSerializer.Serialize(updatedRider));
            HttpContent content = new StringContent(JsSerializer.Serialize(updatedRider));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(requestAddress, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show", new { id = updatedRider.RiderId });
            }

            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            //Ask Christine: Cannot send an interger as http content? Doesnt it defeat the purpose of a post request to send the Id in a url?
            string requestAddress = "riderdata/deleterider/" + id;
            Debug.WriteLine("GOING TO DELETE RIDER ID: " + requestAddress);
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(requestAddress, content).Result;
            Debug.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
