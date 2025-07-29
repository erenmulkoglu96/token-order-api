# Token Order API

Token yönetimi + sipariş sorgulama örneğidir. 

(Token management + order query example.)


Token cache + zaman kontrolü + token yenileme stratejisi gerektiren gerçek dünya entegrasyonu olarak tasarlanmıştır. 

(It is designed as a real world integration that requires token cache + time control + token refresh strategy.)


Minimal düzeyde, modüler yapısı vardır. Sınıflar ayrılmış ve test edilebilir olarak tasarlanmıştır. 
Zaman kontrolü vardır. Token süresini expires_in ile hesapladık.
Rate limit kontrolü yaptık. 5 istekten sonra exception fırlatılıyor.
Endpoint testleri için swagger UI görünümü yaptık. Şuanda açılıyor ve endpoint test edilebilir.
Kod okunabilirliğini basit, düzenli ve iyi isimlendirilmiş olarak tasarladım.
Gerçekçi senaryo uyumuyla beraber gerçek dış API entegrasyon senaryosuna çok uygundur.

(It has a minimal, modular structure. Classes are designed to be separated and testable. 
There is a time control. We calculated the token duration with expires_in. 
We made a rate limit control. An exception is thrown after 5 requests. 
We made a swagger UI view for endpoint tests. It is currently opening and the endpoint can be tested. 
I designed the code readability as simple, organized and well named. 
It is very suitable for the real external API integration scenario with realistic scenario compatibility.)

# Hedef(Target)

- Token’ı sadece gerektiğinde alıyoruz.
- Her sipariş sorgusunda önce token süresini kontrol ediyor.
- Token expired ise yeni alır.
- 1 saat içinde maksimum 5 token isteği sınırını geçmeyecek.

# Çözüm (Solution)

- Token bilgisi (access_token + expiration) bellekte tutulur (örneğin Singleton ya da MemoryCache).
- Sipariş API’si çağrılmadan önce:
- 
    A. Token süresi kontrol edilir.
  
    B. Süresi dolmuşsa (ya da 1 dakikadan az kalmışsa) yeni token alınır.
  
- Token alma işlemi rate-limit altında tutulur.

  # Sonuç (Result)
  
- TokenService ile token yönetimi yapılır.

- OrderService ile token’ı kullanarak sipariş verisi çeker.

- Token sadece gerektiğinde alınıyor.

- Rate limit aşımı önleriz.



# İstenenler ve Benim Uygulamam(Desired and My Application)

  <table>

<tr>
<td>REST API'den sipariş listesini sorgulama</td>
  <td>/get-orders endpoint'i ile çalışmakta</td>
</tr>
<tr>
<td>Siparişten önce token alma</td>
  <td>TokenService.GetTokenAsync() ile önce token kontrol ediliyor</td>
</tr>
<tr>
<td>Token'da token_type, expires_in, access_token gibi parametrelerin parse edilmesi yapılıyor</td>
  <td>TokenResponse modeliyle JSON'dan çözülüyor
</td>
</tr>
<tr>
<td>Saatte maksimum 5 token isteği kontrolü yapılıyor</td>
  <td>if (_requestCount >= 5) kontrolü ile mümkün</td>
</tr>
<tr>
<td>Süre bazlı token geçerliliği kontrolü</td>
  <td>DateTime.Now < _tokenExpiration kontrolü ile süresi izlenebilmekte</td>
</tr>
<tr>
<td>Anlatımlı kod, parçalanmış mantıklı sınıflar yapıldı</td>
  <td>TokenService + OrderService ayrımı ile SOLID prensiplerine uygun olarak tasarladım</td>
</tr>
<tr>
<td>Swagger ile Endpoint denemelerimiz çalışıyor ve test yapılabiliyor</td>
  <td>GET /get-orders endpoint Swagger’da görünüyor</td>
</tr>
  
</table>
  

# Yazar (Author):
Eren Mülkoğlu


