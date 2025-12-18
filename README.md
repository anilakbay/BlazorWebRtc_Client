# Blazor WebRTC & Chat Uygulaması

**.NET Core** ve **Blazor WebAssembly** kullanılarak geliştirilmiş; **Clean Architecture** (Temiz Mimari) prensiplerine sadık kalınarak, **CQRS** ve **MediatR** tasarım desenleri üzerine inşa edilmiş, full-stack gerçek zamanlı bir iletişim uygulamasıdır.

## 🚀 Proje Hakkında

Bu proje, ölçeklenebilirlik ve sürdürülebilirlik odaklı, sağlam bir backend mimarisini demonstre etmek amacıyla geliştirilmiştir. Temel odak noktası; Komut (Command) ve Sorgu (Query) sorumluluklarını birbirinden ayırarak (CQRS), istemci ve sunucu arasındaki veri akışını en temiz ve düzenli hale getirmektir.

## 🛠 Teknolojiler ve Mimari Yapı

- **Backend:** .NET Core (Web API)
- **Frontend:** Blazor WebAssembly
- **Veritabanı:** Entity Framework Core (Code-First Yaklaşımı)
- **Mimari:** CQRS (Command Query Responsibility Segregation)
- **Tasarım Desenleri:** Mediator Pattern (MediatR kütüphanesi ile)
- **Güvenlik:** Password Hashing & Salting, JWT Authentication (Geliştirme aşamasında)

## ✨ Öne Çıkan Özellikler ve Teknik Detaylar

### Backend ve Mimari Kurgusu
- **CQRS Implementasyonu:**
  - Yazma (Write) işlemlerini ayırmak için `RegisterCommand` ve `RegisterHandler` yapıları kurgulandı.
  - İstek/Yanıt (Request/Response) süreçlerindeki bağımlılığı azaltmak (decoupling) için **MediatR** entegrasyonu sağlandı.
- **Kimlik Doğrulama ve Güvenlik:**
  - Özel `AccountService` ve `IAccountService` arayüzleri geliştirildi.
  - Güvenlik mantığına tam hakimiyet sağlamak adına, hazır kütüphaneler yerine manuel **Password Hash** ve **Password Salt** mekanizmaları kodlandı.
  - Validasyon süreçlerini içeren kullanıcı kayıt (Registration) mantığı oluşturuldu.
- **Veritabanı Yönetimi:**
  - Özel entity konfigürasyonları için `OnModelCreation` metodu override edildi.
  - Entity Framework Core migration yapıları kuruldu ve uygulandı.
- **API ve Ağ Yönetimi:**
  - Controller yapıları, yeni Command Handler'ları destekleyecek şekilde RESTful standartlarda revize edildi.
  - Blazor istemcisiyle iletişim için **CORS (Cross-Origin Resource Sharing)** politikaları yapılandırıldı.
  - Dosya yükleme (File Upload) mantığı ve klasör hiyerarşisi backend tarafında işlendi.

### Frontend (Blazor)
- **Bileşen Yapısı:** Modüler bir UI gelişimi için `Pages` ve `Shared` klasör yapısı organize edildi.
- **State ve Servis Yönetimi:** API tüketimi için istemci tarafı HTTP yönetim servisleri entegre edildi.
- **Kullanıcı Etkileşimleri:**
  - Login ve Register formları backend uç noktalarına (endpoints) bağlandı.
  - Arkadaşlık isteği gönderme ve yanıtlama mantığı arayüze işlendi.

## 🚧 Proje Durumu

Proje şu anda aktif geliştirme aşamasındadır. Yapılan son güncellemeler şunlardır:
- Controller katmanının refactor edilmesi.
- Temel Auth (Kimlik Doğrulama) handler'larının yazılması.
- Arkadaşlık isteği sisteminin backend ve frontend tarafında kurgulanması.
