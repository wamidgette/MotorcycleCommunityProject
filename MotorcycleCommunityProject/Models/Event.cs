using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MotorcycleCommunityProject.Models
{
    //This class describes an Event entity and is used to generate a database table
    public class Event
    {
        //Primary key EventId
        [Key]
        public int EventId { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string EventAddress { get; set; }
        public DateTime EventTime { get; set; }
        public decimal EventCost { get; set; }
        //An event can have many trips
        public ICollection<Trip> Trips { get; set; }
    }

    //This class is a data transfer object 
    public class EventDto
    {
        public int EventId { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string EventAddress { get; set; }
        public DateTime EventTime { get; set; }
        public decimal EventCost { get; set; }
    }
}