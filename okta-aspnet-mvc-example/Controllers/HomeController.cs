using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace okta_aspnet_mvc_example.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://localhost:44388/";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsmxTest()
        {

            webservicetest.WebService2 ws = new webservicetest.WebService2()
            {
                Credentials = new NetworkCredential("user1", "p@ssw0rd"),
                PreAuthenticate = true
            };
            try
            {
                var result = ws.HelloWorld();
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();



            return View("AsmxTest");
        }

        //public ActionResult AsmxTest()
        //{

        //    ViewBag.Message = "Calling Asmx Web Service.";
        //    var EmpInfo = "";

        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url  
        //        client.BaseAddress = new Uri(Baseurl);

        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format  
              
        //        String token = "";


        //        IEnumerable<System.Security.Claims.Claim> claims = HttpContext.GetOwinContext().Authentication.User.Claims;

        //        foreach (var claim in claims)
        //        {
        //            if (claim.Type == "access_token")
        //            {
        //                token = claim.Value;
        //            }
        //        }


        //        AuthenticationHeaderValue header = new AuthenticationHeaderValue();
        //        client.DefaultRequestHeaders.Authorization.
        //        client.DefaultRequestHeaders.("xx" , token);
        //        client.DefaultRequestHeaders.Authorization = new SecuredTokenWebService("ilyas", "isik", token);
        //        //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
        //        var Res = await client.GetAsync("api/listitems/1");

        //        //Checking the response is successful or not which is sent using HttpClient  
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api   
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;

        //            //Deserializing the response recieved from web api and storing into the Employee list  
        //            EmpInfo = EmpResponse;

        //        }

        //    }

        //    ViewBag.Message = EmpInfo;

        //    return View("AsmxTest");
        //}


        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Calling Web Service.";
            var EmpInfo = "";

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                String token = "";


                IEnumerable<System.Security.Claims.Claim> claims = HttpContext.GetOwinContext().Authentication.User.Claims;

                foreach (var claim in claims)
                {
                    if (claim.Type == "access_token")
                    {
                        token = claim.Value;
                    }
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",  token);
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                var Res = await client.GetAsync("api/listitems/1");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = EmpResponse;

                }

            }

                ViewBag.Message = EmpInfo;

                return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            return View(HttpContext.GetOwinContext().Authentication.User.Claims);
        }
    }
}