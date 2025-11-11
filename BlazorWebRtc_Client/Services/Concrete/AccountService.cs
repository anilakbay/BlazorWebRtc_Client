using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;
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

        public async Task<ResponseModel> SignUp(RegisterCommand command)
        {
            var content = JsonConvert.SerializeObject(command);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

            if (response.IsSuccessStatusCode)
            {               
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<ResponseModel> SignIn(LoginCommand command)
        {
            var content = JsonConvert.SerializeObject(command);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseModel>(contentTemp);

            if (response.IsSuccessStatusCode)
            {               
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
