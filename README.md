\# Flappy Bird – C# Windows Forms



YMH 219 Nesne Tabanlı Programlama dersi ek uygulama ödevi kapsamında geliştirilmiş, Flappy Bird tarzı 2D bir oyundur. Oyuncu, kuşu zıplatarak sağdan gelen boruların arasındaki boşluktan geçirmeye çalışır.



\*\*Öğrenci:\*\* HASAN KARA – 250541015



\## Ekran Görüntüleri



| Başlangıç | Oyun | Oyun Sonu |

|---|---|---|

| !\[Başlangıç](screenshots/baslangic.png) | !\[Oyun](screenshots/oyun.png) | !\[Game Over](screenshots/gameover.png) |



\## Kullanılan Teknolojiler



\- C#

\- .NET 8

\- Windows Forms

\- GDI+ (`System.Drawing`) ile kodla çizilmiş grafikler (harici görsel dosyası kullanılmadı)



\## Kurulum ve Çalıştırma



1\. Depoyu klonlayın:git clone https://github.com/Hasankr-27/YMH219-FlappyBird.git



2\. `YMH219-FlappyBird.sln` dosyasını Visual Studio 2022 veya üzeriyle açın.

3\. .NET 8 SDK yüklü değilse Visual Studio'nun önerdiği şekilde yükleyin.

4\. `F5` ile çalıştırın.



\## Kontroller



| Tuş | İşlev |

|---|---|

| `Space` | Oyunu başlatır / kuşu zıplatır |

| `Enter` veya \*\*Yeniden Başlat\*\* butonu | Oyun bittikten sonra yeniden başlatır |



\## Özellikler



\- Yer çekimi ve zıplama fiziği (kuş hızına göre eğilir, kanat çırpar)

\- Rastgele yükseklikte, sağdan sola kayan sonsuz borular

\- Boru, zemin ve tavan çarpışma kontrolü

\- Geçilen her boru için skor

\- Dosyaya kaydedilen \*\*en yüksek skor\*\* (oyun kapatılıp açılsa da korunur)

\- Rekor kırıldığında "YENİ REKOR!" bildirimi

\- Skor arttıkça hızlanan oyun (her 5 puanda bir)

\- Kayan bulutlar ve zemin ile hareketli arka plan

\- Oyun sonu paneli ve yeniden başlatma



\## Proje Yapısı



| Sınıf | Görevi |

|---|---|

| `Bird` | Kuşun konumu, hızı, yer çekimi, zıplama ve çizimi |

| `Pipe` | Boru çiftinin konumu, hareketi, çarpışma kontrolü ve çizimi |

| `Background` | Gökyüzü, bulutlar ve zeminin hareketi ve çizimi |

| `GameState` | Skor, en yüksek skor, hız ve oyun durumu yönetimi |

| `Form1` | Oyun döngüsü (Timer), klavye girişi ve sahnenin çizdirilmesi |



Her nesne kendi çizimini `Draw(Graphics g)` metoduyla yapar; `Form1` yalnızca bu metotları sırayla çağırır.



\## Bilinen Sınırlamalar



\- Ses efekti ve müzik yoktur.

\- Pencere boyutu sabittir (400x600).

\- En yüksek skor, programın çalıştığı klasördeki `highscore.txt` dosyasında tutulur; proje temizlenirse (`bin` klasörü silinirse) sıfırlanır.

\- Yalnızca tek oyunculu ve klavye ile oynanır.

