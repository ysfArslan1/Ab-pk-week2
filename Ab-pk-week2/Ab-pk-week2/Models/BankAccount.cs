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
}
