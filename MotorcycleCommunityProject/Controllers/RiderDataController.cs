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

        // GET: api/Riders/5
        [ResponseType(typeof(Rider))]
        public async Task<IHttpActionResult> GetRider(int id)
        {
            Rider rider = await db.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound();
            }

            return Ok(rider);
        }

        // PUT: api/Riders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRider(int id, Rider rider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rider.RiderId)
            {
                return BadRequest();
            }

            db.Entry(rider).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Riders
        [ResponseType(typeof(Rider))]
        public async Task<IHttpActionResult> PostRider(Rider rider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Riders.Add(rider);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rider.RiderId }, rider);
        }

        // DELETE: api/Riders/5
        [ResponseType(typeof(Rider))]
        public async Task<IHttpActionResult> DeleteRider(int id)
        {
            Rider rider = await db.Riders.FindAsync(id);
            if (rider == null)
            {
                return NotFound();
            }

            db.Riders.Remove(rider);
            await db.SaveChangesAsync();

            return Ok(rider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RiderExists(int id)
        {
            return db.Riders.Count(e => e.RiderId == id) > 0;
        }
    }
}