using DemoCallApi.Models;
using DemoCallApi.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace DemoCallApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProduct()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5131/");

            var response = await client.GetAsync("api/products");
            //var response1 = await client.DeleteAsync("api/products/5");

            //if(response.IsSuccessStatusCode)
            //{
            //    return Ok();
            //}

            var content = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<List<OutputProduct>>(content);

            return Ok(json);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(InputProduct input)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5131/");


            //var formdata = new MultipartFormDataContent();
            //var formdata1 = new FormUrlEncodedContent();

            var json = JsonConvert.SerializeObject(input);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/products", data);
            //var response1 = await client.PutAsync("api/products", data);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
