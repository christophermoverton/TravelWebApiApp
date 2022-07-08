using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TravelAppCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelAppCore
{
    public class Uaapikey
    {
        public String uaapikey;
    }

    [Route("api/[controller]")]
    public class UserapikeyController : Controller
    {
        private Dictionary<string,Userapikey> Idlink=new Dictionary<string, Userapikey>();
        private travelappdbContext _webAPIDataContext;

        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public UserapikeyController(travelappdbContext webAPIDataContext)
        {
            _webAPIDataContext = webAPIDataContext;
            
           // _webAPIDataContext.Database.EnsureCreated();
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]Userapikey value)
        {

            string apiKey = RandomString(10);
            string generateKey = RandomString(10);
            value.GeneratekeyId = generateKey;
            value.ApiKeyId = apiKey;
            value.Lastused = DateTime.Now;

            Console.WriteLine(value.ApiKeyId);
            var record = _webAPIDataContext.Userapikey.Where(p => p.UserId == value.UserId).FirstOrDefault();
            if (record != null)
            {
                _webAPIDataContext.Userapikey.Remove(record);
                _webAPIDataContext.SaveChanges();
            }
            _webAPIDataContext.Userapikey.Add(value);
            _webAPIDataContext.SaveChanges();
            // Replace sender@example.com with your "From" address. 
            // This address must be verified with Amazon SES.
            const String FROM = "christophermoverton@gmail.com";
            const String FROMNAME = "Christopherr Overton";

            // Replace recipient@example.com with a "To" address. If your account 
            // is still in the sandbox, this address must be verified.
            const String TO = "christophermoverton@gmail.com";

            // Replace smtp_username with your Amazon SES SMTP user name.
            const String SMTP_USERNAME = "entersmtpusernamehere";

            // Replace smtp_password with your Amazon SES SMTP user name.
            const String SMTP_PASSWORD = "entersmtpuserpasswordhere";

            // (Optional) the name of a configuration set to use for this message.
            // If you comment out this line, you also need to remove or comment out
            // the "X-SES-CONFIGURATION-SET" header below.
            //const String CONFIGSET = "ConfigSet";

            // If you're using Amazon SES in a region other than US West (Oregon), 
            // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
            // endpoint in the appropriate Region.
            const String HOST = "email-smtp.us-west-2.amazonaws.com";

            // The port you will connect to on the Amazon SES SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            const int PORT = 587;

            // The subject line of the email
            const String SUBJECT =
                "Travel App confirmation request";
            String test = "'http://travelappcore-dev.us-west-2.elasticbeanstalk.com/api/Userapikey/Register/" + generateKey + "'";
            // The body of the email
            String BODY =
                "<h1>Passages </h1>" +
                "<p>This email is sent to register your Passages app " +
                $"<a href={test}>Click here</a> to register your " +
                "Passage user id and email addresss.</p>";

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            // Create and configure a new SmtpClient
            SmtpClient client =
                new SmtpClient(HOST, PORT);
            // Pass SMTP credentials
            client.Credentials =
                new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            // Enable SSL encryption
            client.EnableSsl = true;

            // Send the email. 
            try
            {
                Console.WriteLine("Attempting to send email...");
                client.Send(message);
                Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The email was not sent.");
                Console.WriteLine("Error message: " + ex.Message);
            }
            var akey = new Uaapikey{ uaapikey = apiKey};
            var jn = Newtonsoft.Json.JsonConvert.SerializeObject(akey);
            return jn;

        }
        [HttpGet("Register/{key}")]
        public ContentResult CreateApiKey(string key)
        {
            Console.WriteLine(key);


            var record = _webAPIDataContext.Userapikey.Where(p => p.GeneratekeyId== key).FirstOrDefault();
            if (record != null)
            {
                record.Verified = true;
                _webAPIDataContext.SaveChanges();
                /*
                var resp = new HttpResponseMessage();
                resp.Content = new StringContent(
                 "<html><body><h1>Passages </h1>" +
                        "<p>Thank you! You're Passages app " +
                        "has been successfully confirmed.</p></body></html>");
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return resp;
                */
                // Replace sender@example.com with your "From" address. 
                // This address must be verified with Amazon SES.
                const String FROM = "christophermoverton@gmail.com";
                const String FROMNAME = "Christopherr Overton";

                // Replace recipient@example.com with a "To" address. If your account 
                // is still in the sandbox, this address must be verified.
                const String TO = "christophermoverton@gmail.com";

                // Replace smtp_username with your Amazon SES SMTP user name.
                const String SMTP_USERNAME = "enter_smtp_user_name_here";

                // Replace smtp_password with your Amazon SES SMTP user name.
                const String SMTP_PASSWORD = "enter_smtp_user_password_here";

                // (Optional) the name of a configuration set to use for this message.
                // If you comment out this line, you also need to remove or comment out
                // the "X-SES-CONFIGURATION-SET" header below.
                //const String CONFIGSET = "ConfigSet";

                // If you're using Amazon SES in a region other than US West (Oregon), 
                // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
                // endpoint in the appropriate Region.
                const String HOST = "email-smtp.us-west-2.amazonaws.com";

                // The port you will connect to on the Amazon SES SMTP endpoint. We
                // are choosing port 587 because we will use STARTTLS to encrypt
                // the connection.
                const int PORT = 587;

                // The subject line of the email
                const String SUBJECT =
                    "Travel App confirmation request";

                // The body of the email
                String BODY =
                    "<h1>Passages </h1>" +
                    "<p>Thank you!  Your passages app is now registered. " +
                    "</p>";

                // Create and build a new MailMessage object
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(FROM, FROMNAME);
                message.To.Add(new MailAddress(TO));
                message.Subject = SUBJECT;
                message.Body = BODY;
                // Comment or delete the next line if you are not using a configuration set
                //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

                // Create and configure a new SmtpClient
                SmtpClient client =
                    new SmtpClient(HOST, PORT);
                // Pass SMTP credentials
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                // Enable SSL encryption
                client.EnableSsl = true;

                // Send the email. 
                try
                {
                    Console.WriteLine("Attempting to send email...");
                    client.Send(message);
                    Console.WriteLine("Email sent!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                }
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><body>Thank you!  Your passages app has been successfully registered!</body></html>"
                };

            }
            else
            {
                Console.WriteLine("Unable to add user api key.  Api key not found!");
            }
            /*
            var response = new HttpResponseMessage();
            response.Content = new StringContent(
             "<html><body><h1>Passages </h1>" +
                    "<p>We are sorry! " +
                    "Unfortuantely we are unable to verify your app.  Please resubmit an authentication!</p></body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
            */
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body>Sorry!  We are unable to verify your Passages app.  Please resubmit your registration.</body></html>"
            };
   
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
