namespace BlazorWebRtc_Application.DTO
{
    public class UserDTOResponseModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
    }
}
