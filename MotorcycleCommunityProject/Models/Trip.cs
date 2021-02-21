using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleCommunityProject.Models
{
    //Builds the Trip entity in database
    public class Trip
    {
        //Trip has a primary key of TripId
        [Key]
        public int TripId { get; set; }
        public int LeadRiderId { get; set; }
        public string TripName { get; set; }
        public string TripRoute { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Rider> Riders { get; set; }
    }

    //Describes elements that comprise a trip 
    public class TripDto
    {
        public int TripId { get; set; }
        public string TripName { get; set; }
        public string TripRoute { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }
    }
}