using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ab_pk_week2.Models
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int accountId { get; set; }
        public string accountHolder { get; set; }
        public string status { get; set; }
        public string accountCurrency { get; set; }
        public decimal accountBalance { get; set; }
    }

    // BankAccount sınıfına ait Extension metodu
    public static class BankAccountExtensions
    {
        // BankAccount sınıfına ait özel bir işlevsellik ekleyebiliriz
        public static string GetFormattedBalance(this BankAccount bankAccount)
        {
            // Örnek bir formatlama işlemi
            return $"{bankAccount.accountBalance:C}  , Extention kullanımına örnek";
        }
    }

}
