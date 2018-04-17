using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAppCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAppCore
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private travelappdbContext _webAPIDataContext;

        public TripsController(travelappdbContext webAPIDataContext)
        {
            _webAPIDataContext = webAPIDataContext;
            _webAPIDataContext.Database.EnsureCreated();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "trip1", "value2" };
        }

        [HttpGet("UserTrips")]
        public IEnumerable<Trips> GetTrips(string userId, int tripID)
        {
            var products = _webAPIDataContext.Trips.Where(p => p.UserId == userId && p.TripId == tripID);
            return products;
        }

        [HttpGet("User")]
        public IEnumerable<Trips> GetUserTrips(string userId)
        {
            var products = _webAPIDataContext.Trips.Where(p => p.UserId == userId);
            return products;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Tripsparams value)
        {
            Trips trip = value.trip;
            IQueryable<Userapikey> queryreq =_webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == value.userApiKeyId);
            Userapikey ukey = queryreq.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            if (ukey != null && ukey.Verified)
            {
                //value.CreatedOn = DateTime.Now;
                _webAPIDataContext.Trips.Add(trip);
                _webAPIDataContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("User api key not verified or doesn't exist!");
            }
        }

        // POST api/<controller>
        [HttpPost("Tripspost")]
        public void PostTrips([FromBody]List<Trips> value)
        {
            //value.CreatedOn = DateTime.Now;
            foreach (var trip in value)
            {
                _webAPIDataContext.Trips.Add(trip);
                _webAPIDataContext.SaveChanges();
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
