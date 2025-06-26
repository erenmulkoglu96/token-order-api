# Token Order API

Token yönetimi + sipariş sorgulama örneğidir. (Token management + order query example.)

Token cache + zaman kontrolü + token yenileme stratejisi gerektiren gerçek dünya entegrasyonu (Real world integration requiring token cache + time control + token refresh strategy)

# Hedef(Target)

- Token’ı sadece gerektiğinde alıyoruz.
- Her sipariş sorgusunda önce token süresini kontrol ediyor.
- Token expired ise yeni alır.
- 1 saat içinde maksimum 5 token isteği sınırını geçmeyecek.

# Çözüm (Solution)

- Token bilgisi (access_token + expiration) bellekte tutulur (örneğin Singleton ya da MemoryCache).
- Sipariş API’si çağrılmadan önce:
    A. Token süresi kontrol edilir.
    B.Süresi dolmuşsa (ya da 1 dakikadan az kalmışsa) yeni token alınır.
- Token alma işlemi rate-limit altında tutulur.

  # Sonuç (Result)
  
- TokenService ile token yönetimi yapılır.

- OrderService ile token’ı kullanarak sipariş verisi çeker.

- Token sadece gerektiğinde alınıyor.

- Rate limit aşımı önleriz.

- <table>

<tr>
<td>REST API'den sipariş listesini sorgulama</td>
  <td>/get-orders endpoint'i ile çalışıyor.</td>
</tr>

  
</table>
  

# Author:
Eren Mülkoğlu


