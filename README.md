# GetApi-MemoryCache-Swagger

GETAPI

Çalıştırma:

1-Terminal ekranın projenin bulunduğu klasöre gelinir.

2-Terminal ekranın şu komut yazılır:

  ```Csharp
  dotnet run
  ```

3-localhost:xxxx/api/users local linkine otomatik olarak yönlendirilir.

1.Güncelleme:

1-Program.cs ile UsersController.cs dosyaları, Memory Cache için güncelleme yapıldı.

2-Artık program başlatıldıktan sonra eğer cache içerisi boş ise konsolda "Cache miss - Veri API'den alındı ve cache'e eklendi." mesajı verecek.

3-Eğer cache içerisi dolu ise konsolda "Cache hit - Veri cache'den alındı." mesajı verecek.

2.Güncelleme:

1-Programa swagger özelliği eklendi. Bu özelliğin daha düzgün hale getirtmek için çalışılmaya devam edilecek.

3.Güncelleme:

Dependency Injection Mekanizmalarını kullanarak UserController içerisinde yazmış olduğum API'deki kodları farklı bir sınıftaki fonksiyona aktarıp UserControllerdaki fonksiyon içerisinden o sınıfın objesi üzerinden ilgili fonksiyona istek attım.
Bunu ekleyerek arasındaki bağımlılıkları daha yönetilebilir ve test edilebilir hale getirmeye çalıştım.

4.Güncelleme:

1-Entity Framework Orm yapısını kullanarak User modelinden bir objeyi veritabanına kaydetme eklendi.

2-Post API oluşturup API'nin body'sine User modeline ait veriyi eklenecek.

3-Daha sonra istek atılan API' üzerinden ilgili modele Entity Framework yardımıyla ile SQL veritabanına kaydedilecek.




