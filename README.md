# GetApi-MemoryCache-Swagger

GETAPI

Çalıştırma:

1-Terminal ekranın projenin bulunduğu klasöre gelinir.

2-Terminal ekranın şu komut yazılır:

  ```Csharp
  dotnet run
  ```

3-localhost:xxxx/api/users local linkine otomatik olarak yönlendirilir.

Güncelleme:

1-Program.cs ile UsersController.cs dosyaları, Memory Cache için güncelleme yapıldı.

2-Artık program başlatıldıktan sonra eğer cache içerisi boş ise konsolda "Cache miss - Veri API'den alındı ve cache'e eklendi." mesajı verecek.

3-Eğer cache içerisi dolu ise konsolda "Cache hit - Veri cache'den alındı." mesajı verecek.

2.Güncelleme

1-Programa swagger özelliği eklendi. Bu özelliğin daha düzgün hale getirtmek için çalışılmaya devam edilecek.
