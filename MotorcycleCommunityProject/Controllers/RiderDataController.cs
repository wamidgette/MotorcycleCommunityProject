using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MotorcycleCommunityProject.Models;
using System.Diagnostics;

namespace MotorcycleCommunityProject.Controllers
{
    public class RiderDataController : ApiController
    {
        private MotorcycleCommunityContext db = new MotorcycleCommunityContext();

        //Get: api/RiderData/GetRiders
        /// <summary>
        /// This method fetches a list of Rider objects from the database, converts to list of RiderDto objects and returns the list
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<RiderDto>))]
        public IHttpActionResult GetRiders()
        {
            List<Rider> Riders = db.Riders.ToList();
            List<RiderDto> RiderDtos = new List<RiderDto> { };

            foreach (var Rider in Riders)
            {
                //Convert each rider from the list of riders returned by the database into a Rider Data Transfer Object
                RiderDto NewRider = new RiderDto
                {
                    RiderId = Rider.RiderId,
                    FirstName = Rider.FirstName,
                    LastName = Rider.LastName,
                    Bio = Rider.Bio,
                    BikeSize = Rider.BikeSize,
                    Age = Rider.Age,
                };
                //Add the Rider Data Transfer Object to the List<RiderDbo>
                RiderDtos.Add(NewRider);
            }

            return Ok(RiderDtos);
        }

        // PUT: api/Riders/addRider
        /// <summary>
        /// Recieves a HttpRequest with a rider json object, and adds the rider to the rider database 
        /// </summary>
        /// <param Rider="NewRider"></param>
        /// <returns>Returns Http OK status to the Rider/Create mvc controller</returns>

        [ResponseType(typeof(Rider))]
        [HttpPost]
        //FromBody accesses the httpmessage content as the type of object specified
        public IHttpActionResult addRider([FromBody] Rider NewRider)
        {
            Debug.WriteLine("THE RIDER FIRST NAME IS: " + NewRider.FirstName);
            
            if(!ModelState.IsValid)
            {
                //if rider object is not valid, return badrequest to mvc Create()
                return BadRequest(ModelState);
            }

            //if the object in the http message matches the Rider object properties, add to database
            db.Riders.Add(NewRider);
            db.SaveChanges();
            //Rider will now have been entered into the database and an Id generated
            //Send Id back to MVC controller in order to display details on "show" view
            return Ok(NewRider.RiderId);
        }

        /// <summary>
        /// Finds the rider with the unique riderId as parameter id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The rider object with the corresponding id</returns>
        [HttpGet]
        [ResponseType(typeof(RiderDto))]
        public IHttpActionResult findRider(int id)
        {
            Debug.WriteLine("YOU ARE IN THE FIND RIDER API METHOD");
            Rider Rider = db.Riders.Find(id);

            if (Rider == null)
            {
                return NotFound();
            }

            RiderDto RiderDto = new RiderDto
            {
                RiderId = Rider.RiderId,
                FirstName = Rider.FirstName,
                LastName = Rider.LastName,
                Bio = Rider.Bio,
                BikeSize = Rider.BikeSize,
                Age = Rider.Age
            };
            
            return Ok(RiderDto);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult updateRider([FromBody] Rider updatedRider)
        {
            Debug.WriteLine("YOU ARE IN THE UPDATE RIDER API METHOD");

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            db.Entry(updatedRider).State = EntityState.Modified;

           try
            {
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //Return No Content 204 message if row is successfully updated

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult deleteRider(int id)
        {
            Debug.WriteLine("IN THE DELETE DATA CONTROLLER");
            Rider Rider = db.Riders.Find(id);
            if(Rider==null)
            {
                Debug.WriteLine("NO RIDER FOUND");
                return NotFound();
            }
            else
            {
                Debug.WriteLine("FOUND RIDER");
                db.Riders.Remove(Rider);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}