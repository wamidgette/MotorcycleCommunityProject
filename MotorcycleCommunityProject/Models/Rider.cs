using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleCommunityProject.Models
{
    //Describes how to build the Rider entity in the database 
    public class Rider
    {
        //primary key RiderId
        [Key]
        public int RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public int BikeSize { get; set; }
        public int Age { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
    
    //Describes elements that make a rider 
    public class RiderDto
    {
        public int RiderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public int BikeSize { get; set; }
        public int Age { get; set; }
    }
}