using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Services.Abstract;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc_Client.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SignUp(RegisterCommand command)
        {
            var content = JsonConvert.SerializeObject(command);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        }

      
    }
}
