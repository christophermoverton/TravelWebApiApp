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
            var products = _webAPIDataContext.Trips.Where(p => p.UserID == userId && p.TripID == tripID);
            return products;
        }

        [HttpGet("UserTrip")]
        public IEnumerable<Trips> GetUserTrip(string UserApiKeyId, string UserId, int TripId)
        {
            IQueryable<Userapikey> query = _webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == UserApiKeyId);
            Userapikey ukey = query.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            if (ukey != null && ukey.Verified)
            {
                Console.WriteLine(UserId);
                var tquery = _webAPIDataContext.Trips.Where(p => p.UserID == UserId && p.TripID == TripId);
                return tquery;
            }
            return new Trips[] { };
        }

        [HttpGet("TripsList")]
        public IEnumerable<Trips> GetTripsList(string UserApiKeyId, string UserId)
        {
            IQueryable<Userapikey> query = _webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == UserApiKeyId);
            Userapikey ukey = query.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            if (ukey != null && ukey.Verified)
            {
                Console.WriteLine(UserId);
                var tquery = _webAPIDataContext.Trips.Where(p => p.UserID == UserId).GroupBy(p => p.TripID);
                List<Trips> rettrips = new List<Trips>();
                
                foreach(var item in tquery)
                {
                    rettrips.Add(item.FirstOrDefault());
                }
                return rettrips;
            }
            else
            {
                Console.WriteLine("User api key not verified or doesn't exist!");
                
            }
            return new Trips[] { };
        } 

        [HttpGet("User")]
        public IEnumerable<Trips> GetUserTrips(string userId)
        {
            var products = _webAPIDataContext.Trips.Where(p => p.UserID == userId);
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
        public SFResponse Post([FromBody]Tripsparams value)
        {
            Trips trip = value.trip;
            IQueryable<Userapikey> queryreq =_webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == value.userApiKeyId);
            Userapikey ukey = queryreq.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            SFResponse sres = new SFResponse();
            sres.Success = false;
            if (ukey != null && ukey.Verified)
            {
                //value.CreatedOn = DateTime.Now;
                _webAPIDataContext.Trips.Add(trip);
                _webAPIDataContext.SaveChanges();
                sres.Success = true;
            }
            else
            {
                Console.WriteLine("User api key not verified or doesn't exist!");
            }
            return sres;
        }

        // POST api/<controller>
        [HttpPost("Tripspost")]
        public SFResponse PostTrips([FromBody]TripsListParams value)
        {
            //Trips trip = value.UserApiKeyId;
            IQueryable<Userapikey> queryreq = _webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == value.UserApiKeyId && p.UserId == value.UserId);
            Userapikey ukey = queryreq.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            SFResponse sres = new SFResponse();
            sres.Success = false;
            if (ukey != null && ukey.Verified)
            {
                //value.CreatedOn = DateTime.Now;
                
                foreach (var trip in value.Triplist)
                {

                    //tripId = trip.TripID;
                    _webAPIDataContext.Trips.Add(trip);

                }
                _webAPIDataContext.SaveChanges();
                sres.Success = true;

            }
            return sres;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("Tripsdelete")]
        public SFResponse Delete(string UserApiKeyId, int Id)
        {
            IQueryable<Userapikey> query = _webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == UserApiKeyId);
            Userapikey ukey = query.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            SFResponse sres = new SFResponse();
            sres.Success = false;

            if (ukey != null && ukey.Verified)
            {
                var queryreq = _webAPIDataContext.Trips.Where(p => p.Id == Id && p.UserID == ukey.UserId);
                Trips trip = queryreq.FirstOrDefault();
                //Console.WriteLine(trip.Id);
                if (trip != null)
                {
                    _webAPIDataContext.Trips.Remove(trip);
                    _webAPIDataContext.SaveChanges();
                    sres.Success = true;
                    return sres;
                }
            }
            return sres;
        }

        [HttpDelete("TripsTripIdDelete")]
        public SFResponse tDelete(string UserApiKeyId, int tripId)
        {
            IQueryable<Userapikey> query = _webAPIDataContext.Userapikey.Where(p => p.ApiKeyId == UserApiKeyId);
            Userapikey ukey = query.FirstOrDefault();
            Console.WriteLine("Apikey match: ");
            Console.WriteLine(ukey);
            SFResponse sres = new SFResponse();
            sres.Success = false;
           
            if (ukey != null && ukey.Verified)
            {
                var queryreq = _webAPIDataContext.Trips.Where(p => p.TripID == tripId && p.UserID == ukey.UserId);
                //Trips trip = queryreq.FirstOrDefault();
                List<Trips> trips = queryreq.ToList<Trips>();
                //Console.WriteLine(trip.Id);
                foreach (var trip in trips)
                {
                    _webAPIDataContext.Trips.Remove(trip);
                    
                }
                _webAPIDataContext.SaveChanges();
                /*
                if (trip != null)
                {
                    _webAPIDataContext.Trips.Remove(trip);
                    _webAPIDataContext.SaveChanges();
                    sres.Success = true;
                    return sres;
                }*/

                sres.Success = true;
                return sres;
            }
            return sres;
        }


    }
}
