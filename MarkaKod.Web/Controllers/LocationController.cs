using MarkaKod.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MarkaKod.Web.Controllers
{
    public class LocationController : Controller
    {
        string Baseurl = "https://localhost:7006/";

        public async Task<ActionResult> Index()
        {
            List<Location> locationList = new List<Location>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiUrl = "api/route/list";

                HttpResponseMessage Res = await client.GetAsync(apiUrl);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    locationList = JsonConvert.DeserializeObject<List<Location>>(response);
                }
                //returning the employee list to view
                return View(locationList);
            }



        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Location location)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiUrl = "api/route/create";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(location);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PostAsync(apiUrl, data);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                }
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            Location location = new Location();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiUrl = "api/route/getbyid?id=" + Id;

                HttpResponseMessage Res = await client.GetAsync(apiUrl);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    location = JsonConvert.DeserializeObject<Location>(response);
                }

                location.LatString = location.Lat.ToString().Replace(",",".");
                location.LongString = location.Long.ToString().Replace(",", ".");
                return View(location);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Location location)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiUrl = "api/route/update";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(location);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage Res = await client.PutAsync(apiUrl, data);

                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                }
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
