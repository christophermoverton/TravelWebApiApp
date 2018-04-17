using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAppCore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAppCore
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private travelappdbContext _webAPIDataContext;
        
        public BooksController(travelappdbContext webAPIDataContext)
        {
            _webAPIDataContext = webAPIDataContext;
            _webAPIDataContext.Database.EnsureCreated();
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Books> Get()
        {
            return _webAPIDataContext.Books.OrderByDescending(s => s.Name).ToList();
            //return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public void Post([FromBody]Books value)
        {
            value.CreatedOn =  DateTime.Now;
            _webAPIDataContext.Books.Add(value);
            _webAPIDataContext.SaveChanges();
            
        }

    }
}
