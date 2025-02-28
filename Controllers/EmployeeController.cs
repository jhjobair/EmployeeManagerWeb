using Microsoft.AspNetCore.Mvc;
using ASPcoreWebAPIpractise.Models;
using Newtonsoft.Json;
using System.Text;
namespace ASPcoreWebAPIpractise.Controllers
{
    public class EmployeeController : Controller
    {
        private string url = "https://localhost:7066/api/Employee";
        private HttpClient client= new HttpClient();
        public IActionResult Index()
        {
            List<Emoloyee> emoloyees = new List<Emoloyee>();
            HttpResponseMessage responseMsg = client.GetAsync(url).Result;
            if (responseMsg != null)
            {
                string result = responseMsg.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Emoloyee>>(result);
                if (data != null)
                {
                    emoloyees = data;
                }
            }
            return View(emoloyees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Emoloyee emp)
        {
            string data= JsonConvert.SerializeObject(emp);
            StringContent content= new StringContent(data,Encoding.UTF8,"Application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response != null)
            {
                TempData["create"] = "Data Added...";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Emoloyee emoloyees = new Emoloyee();
            HttpResponseMessage responseMsg = client.GetAsync($"{url}/{id}").Result;
         
            if (!responseMsg.IsSuccessStatusCode)
            {
                return NotFound();
            }
            string result = responseMsg.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Emoloyee>(result);
            if (data != null)
            {
                emoloyees = data;
            }
            return View(emoloyees);
        }

        [HttpPost]
        public IActionResult Edit(int id, Emoloyee emp)
        {
            if (id != emp.id)
            {
                return BadRequest();
            }
            string data = JsonConvert.SerializeObject(emp);
            StringContent content = new StringContent(data, Encoding.UTF8, "Application/json");
            HttpResponseMessage response = client.PutAsync($"{url}/{id}", content).Result;
            if (response != null)
            {
                TempData["update"] = "Data Updated...";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Emoloyee emoloyees = new Emoloyee();
            HttpResponseMessage responseMsg = client.GetAsync($"{url}/{id}").Result;
            if (!responseMsg.IsSuccessStatusCode)
            {
                return NotFound();
            }
            string result = responseMsg.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Emoloyee>(result);
            if (data != null)
            {
                emoloyees = data;
            }
            return View(emoloyees);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Emoloyee emoloyees = new Emoloyee();
            HttpResponseMessage responseMsg = client.GetAsync($"{url}/{id}").Result;
            if (!responseMsg.IsSuccessStatusCode)
            {
                return NotFound();
            }
            string result = responseMsg.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Emoloyee>(result);
            if (data != null)
            {
                emoloyees = data;
            }
            return View(emoloyees);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage responseMsg = client.DeleteAsync($"{url}/{id}").Result;
            if (responseMsg.IsSuccessStatusCode)
            {
                TempData["Delete"] = "Data Deleted...";
                return RedirectToAction("Index");

            }
            return View();
        }

    }
}
