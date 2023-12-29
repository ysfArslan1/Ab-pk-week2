using Microsoft.EntityFrameworkCore;
using Ab_pk_week2.Models;

namespace Ab_pk_week2.DBOperations;

public class DataGenerator
{
    //inmemory de data üretmek içinkullanılıyor // program.cs de çalıştırılıyor
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var content = new BankDbContext(serviceProvider.GetRequiredService<DbContextOptions<BankDbContext>>())) 
        {
            if (content.BankAccounts.Any()) 
            {
                return;
            }

            content.BankAccounts.AddRange(
                new BankAccount
                {
                    //accountId = 1, // auto incremant yapıldı
                    accountHolder = "Ahmet A.",
                    status = "Active",
                    accountCurrency = "EUR", // euro 
                    accountBalance = 435000,
                },
                new BankAccount
                {
                    //accountId = 2,
                    accountHolder = "Mehmet C.",
                    status = "Active",
                    accountCurrency = "TRY", // turkish lira 
                    accountBalance = 21450,
                },
                new BankAccount
                {
                    //accountId = 3,
                    accountHolder = "Zeynet T.",
                    status = "Active",
                    accountCurrency = "USD", // us dolar 
                    accountBalance = 54600,
                },
                new BankAccount
                {
                    //accountId = 4,
                    accountHolder = "Selin B.",
                    status = "Active",
                    accountCurrency = "JPY", // japanese yen
                    accountBalance = 96237,
                },
                new BankAccount
                {
                    //accountId = 5,
                    accountHolder = "Recep D.",
                    status = "Active",
                    accountCurrency = "CNY", // chinese yuan renminbi 
                    accountBalance = 348234,
                }
            );

            content.SaveChanges();
        }
    }
}

