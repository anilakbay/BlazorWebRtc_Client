using BlazorWebRtc_Client.Models.Response;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc_Client.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/User/getusers");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseModel>(content);
                    
                    if (result?.IsSuccess == true && result.Data != null)
                    {
                        var usersJson = JsonConvert.SerializeObject(result.Data);
                        return JsonConvert.DeserializeObject<List<UserDto>>(usersJson) ?? new List<UserDto>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUsers error: {ex.Message}");
            }

            return new List<UserDto>();
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/User/getuser/{userId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseModel>(content);
                    
                    if (result?.IsSuccess == true && result.Data != null)
                    {
                        var userJson = JsonConvert.SerializeObject(result.Data);
                        return JsonConvert.DeserializeObject<UserDto>(userJson);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserById error: {ex.Message}");
            }

            return null;
        }

        public async Task<bool> AddFriendAsync(Guid userId, Guid friendId)
        {
            try
            {
                var request = new { UserId = userId, FriendId = friendId };
                var content = JsonConvert.SerializeObject(request);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("api/UserFriend/addfriend", bodyContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddFriend error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveFriendAsync(Guid userId, Guid friendId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/UserFriend/removefriend/{userId}/{friendId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RemoveFriend error: {ex.Message}");
                return false;
            }
        }
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsOnline { get; set; }
        public bool IsFriend { get; set; }
    }
}
