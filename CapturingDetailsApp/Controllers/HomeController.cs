using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AdaptClientProjectApi.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Text;
namespace CapturingDetailsApp.Controllers
{
    public class HomeController(IHttpClientFactory clientFactory, INotyfService notyfService) : Controller
    {
        
        private readonly IHttpClientFactory _httpClientFactory = clientFactory;
        private INotyfService _notyfService = notyfService;
        

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Client client)
        {
            if(ModelState.IsValid)
            {
                //create an httpClient instance 
                var httpClient = _httpClientFactory.CreateClient("Clients.WebApi");
                //serialize the received customer data
                var jsonData = JsonSerializer.Serialize(client);
                //create a post request with the required data
                HttpRequestMessage request = new(method: HttpMethod.Post,requestUri: "api/clients")
                {
                    Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
                };
                //send the request using the httpClient
                var response = await httpClient.SendAsync(request);

                //check the statusCode of the response
                if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError("", "Duplicate client names are not allowed");
                    
                }
                else
                {
                    _notyfService.Success("Client registered");
                    return Redirect("/");
                }
                
            }
            
            _notyfService.Error("Validation Error",3);
            return View(client);
        }

    }
       
    
}
