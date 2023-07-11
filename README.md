# Telefon Rehberi

### Senaryo

>  Birbirleri ile haberleşen minimum iki microservice'in olduğu bir yapı tasarlayarak, basit bir telefon rehberi uygulaması oluşturulması sağlanacaktır.
##### Beklenen işlevler:

- Rehberde kişi oluşturma
- Rehberde kişi kaldırma
- Rehberdeki kişiye iletişim bilgisi ekleme
- Rehberdeki kişiden iletişim bilgisi kaldırma
- Rehberdeki kişilerin listelenmesi
- Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin 
getirilmesi
- Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor 
talebi
- Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor 
talebi
- Sistemin oluşturduğu raporların listelenmesi
- Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi


### Teknik Tasarım

> Kişiler: Sistemde teorik anlamda sınırsız sayıda kişi kaydı yapılabilecektir. Her kişiye bağlı iletişim bilgileri de yine sınırsız bir biçimde eklenebilmelidir.

##### Karşılanması beklenen veri yapısındaki gerekli alanlar aşağıdaki gibidir:
- UUID
- Ad
- Soyad
- Firma
- İletişim Bilgisi
  - Bilgi Tipi: Telefon Numarası, E-mail Adresi, Konum
  - Bilgi İçeriği
> Rapor: Rapor talepleri asenkron çalışacaktır. Kullanıcı bir rapor talep ettiğinde, sistem 
arkaplanda bu çalışmayı darboğaz yaratmadan sıralı bir biçimde ele alacak; rapor 
tamamlandığında ise kullanıcının "raporların listelendiği" endpoint üzerinden raporun 
durumunu "tamamlandı" olarak gözlemleyebilmesi gerekmektedir.
##### Rapor basitçe aşağıdaki bilgileri içerecektir:
- Konum Bilgisi
- O konumda yer alan rehbere kayıtlı kişi sayısı
- O konumda yer alan rehbere kayıtlı telefon numarası sayısı
#### Veri yapısı olarak da:
- UUID
- Raporun Talep Edildiği Tarih
- Rapor Durumu (Hazırlanıyor, Tamamlandı)

### Http İstekleri
#### - Contact
##### Create Contact
```http
  POST http://localhost:5164/contacts/CreateContact
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `contactName` | `string` | **Gerekli**. contactCompany. |
| `contactLastName` | `string` | **Gerekli**. contactLastName. |
| `contactCompany` | `string` | **Gerekli**. contactCompany. |

##### Delete Contact
```http
  DELETE http://localhost:5164/contacts/DeleteContacts/{contactId}
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `contactId` | `string` | **Gerekli**. contactId. |

##### Get Contact By Id
```http
  GET http://localhost:5164/contacts/GetContactById/{contactId}
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `contactId` | `string` | **Gerekli**. contactId. |

##### Get All Contact List  
```http
  GET http://localhost:5164/contacts/GetAllContactList
```
#### - Contact Details
##### Create Contact Details
```http
  POST http://localhost:5164/contactDetails/CreateContactDetails
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `contactId` | `string` | **Gerekli**. contactId. |
| `contactInfoType` | `int` | **Gerekli**, **Default=0**. contactInfoType. |
| `value` | `string` | **Gerekli**. value. |

##### Get All Contact Details List
```http
  GET http://localhost:5164/contactDetails/GetAllContactDetailsList
```
##### Get Contact By Id
```http
  DELETE http://localhost:5164/contactDetails/DeleteContactDetails/{contactId}
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `contactId` | `string` | **Gerekli**. contactId. |

##### Get Contact Details By Id
```http
  GET http://localhost:5164/contactDetails/GetContactDetailsById/{contactId}
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `contactId` | `string` | **Gerekli**. contactId. |

#### - Reports
##### Get All Reports
```http
  GET http://localhost:5164/Reports/GetAllReports
```


##### Get Report By Id
```http
  GET http://localhost:5164/Reports/GetReportById/{id}
```

| Body | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `id` | `string` | **Gerekli**. id. |

##### Create Report
```http
  POST http://localhost:5164/Reports/CreateReport
```

#### - Proje Mimarisi

<p align="center">
  <img  src="https://github.com/alierguc1/TelephoneBook/blob/develop/docs/telephone_book_architec.png?raw=true">
</p>


