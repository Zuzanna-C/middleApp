using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Net.WebRequestMethods;

namespace middleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnovaController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public EnovaController()
        {
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true,
                UseDefaultCredentials = false
            };

            _httpClient = new HttpClient(httpClientHandler);
        }

        //[HttpPost("logIn")]
        //public void Login(LoggingModel model)
        //{
            
        //}
    }
}
