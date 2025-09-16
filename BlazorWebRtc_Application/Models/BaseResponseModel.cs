namespace BlazorWebRtc_Application.Models
{
    public class BaseResponseModel
    {
        public string Message { get; set; } = string.Empty; // işlem mesajı (ör: "Kayıt başarılı")
        public bool IsSuccess { get; set; }  // başarılı mı, değil mi?
        public object? Data { get; set; } // ekstra döndürülmek istenen veri (ör: token, kullanıcı bilgisi vs.)
    }
}
