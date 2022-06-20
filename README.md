master branchi üzerinden Markakod.API.bak dosyası ilgili veritabanında
restore edilir.

Markakod.API.cs içinde bulunan “private string connectionString =”Data
Source=.;Initial Catalog=MarkaKod;Integrated Security=True;“;” alanı
restore edilen connection ile değiştirilir.

Uygulama önce API ve ardından Web uygulaması çalıştırılarak
görüntülenir.

Create ve Update işlemleri tamamlanır. İlgili veeriler visited yada
current localtion olarak düzenlenir.

Birden fazla Current Location varsa uygulama ilk seçtiğini baz alır.

Uygulama POC olduğu için hepsi visited seçilirse, yada nokta yerine
virgül kullanılırsa gibi kontroller yapılmamıştır.

Performans göz önünde bulundurulmamıştır.

Listeleme sayfası her zaman verilen koordinatlardan en yakınını
hesaplar. 2nci hesaplanan koordinat a noktasından b noktasına gelindiği
düşünülerek B noktası baz alınarak hesaplanmıştır.
