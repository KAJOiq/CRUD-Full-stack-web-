using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mohammed_hazim.Context;
using mohammed_hazim.Model.DTOs;
using System.Text.Json;
using System.Text;

namespace mohammed_hazim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarYousifController : ControllerBase
    {
        private readonly MainAppContext _mainAppContext;
        private readonly string _api = "https://2398-185-90-106-60.ngrok-free.app/";

        public CarYousifController(MainAppContext mainAppContext)
        {
            _mainAppContext = mainAppContext;
        }
        [HttpGet()]
        public async Task<IActionResult> CallOtherApi()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_api}api/Cars"); // Use your friend's Ngrok URL
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_api}api/Cars/{id}"); // Use your friend's Ngrok URL
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarsDTO carsDTO)
        {
            using (var httpClient = new HttpClient())
            {

                var content = new StringContent(JsonSerializer.Serialize(carsDTO), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{_api}api/Cars/Register", content); // Replace with your friend's Ngrok URL
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent);
            }
        }
        // Delete existing car info
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{_api}api/Cars/{id}"); // Replace with your friend's Ngrok URL
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent);
            }
        }

        // Update existing car info
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarsDTO CarsDTO)
        {
            using (var httpClient = new HttpClient())
            {

                var content = new StringContent(JsonSerializer.Serialize(CarsDTO), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{_api}api/Cars/{id}", content); // Replace with your friend's Ngrok URL
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(responseContent);
            }
        }
    }
}
