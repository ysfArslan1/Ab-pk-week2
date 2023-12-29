# Akbank .Net Bootcamp Cohort Ödevi 2

Akbank ve patikadev tarafından gerçekleştirilen Asp.Net eğitimi sürecinde verilen cohort üzerinden verilen ikici hafta ödevi. 

[Akbank .Net Bootcamp Cohort Ödev 1 link](https://github.com/ysfArslan1/Ab-pk-week1)

## Bizden istenilenler:
-  İlk hafta geliştirdiğiniz api kullanılacaktır.
-   Rest standartlarına uygun olmalıdır.
-   solid prensiplerine uyulmalıdır.
-   Fake servisler geliştirilerek Dependency injection kullanılmalıdır.
-   Ap’ nizde kullanılmak üzere extension geliştirin.
-   Projede swagger implementasyonu gerçekleştirilmelidir.
-   Global loglama yapan bir middleware(sadece actiona girildi gibi çok basit düzeyde)


## Servisler ve Dependecy Injection:
Dependency Injektion görevini tamamlamak için `Services/` klasörü altında iki oluşturdum. ilk olarak `IBankAccountService` interfacesini oluşturdum sonrsında bu interfaceden kalıtım alan `BankAccountService1` ve `BankAccountService2` sınıflarını oluşturdum . Dependency Injection kullanılması için `Service` sınıfını oluşturdum.

![Resim Açıklaması](images/s1.jpeg)
![Resim Açıklaması](images/s2.jpeg)
![Resim Açıklaması](images/s3.jpeg)

- `GET /GetBankAccountByIdFromS1/{id}` :

  Servis 1'i kullanarak Banka hesabı alan metot.
  
- `GET /GetBankAccountByIdFromS2/{id}`:

  Servis 2'i kullanarak Banka hesabı alan metot.

## Extension geliştirme:
CommonControllerde kullanılmak üzere BankAccount sınıfında bulunan `GetFormattedBalance` metodu geliştirildi.

- `GET /GetBankAccountBalanceById/{id}`:

  Bu metot BankAccount sınıfından extension edilen `GetFormattedBalance` metot kullanılarak oluşturulmuştur.
![Resim Açıklaması](images/e1.jpeg)

## Middleware oluşturma:
projedeki respons ve rejuestlerin `LoggingMiddleware/log.txt` dosyasına loglanması için  `LoggingMiddleware/Midlleware.cs` dosysını oluşturdum.


![Resim Açıklaması](images/m1.jpeg)





