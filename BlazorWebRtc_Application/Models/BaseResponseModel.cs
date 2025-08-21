namespace BlazorWebRtc_Application.Models
{
    public class BaseResponseModel
    {
        internal bool isSuccess;

        public string Message { get; set; } // işlem mesajı (ör: "Kayıt başarılı")
        public bool IsSuccess { get; set; }  // başarılı mı, değil mi?
        public object Data { get; set; } // ekstra döndürülmek istenen veri (ör: token, kullanıcı bilgisi vs.)
    }
}
