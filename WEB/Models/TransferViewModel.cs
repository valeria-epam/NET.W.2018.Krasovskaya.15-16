using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class TransferViewModel
    {
        [Display(Name = "Account number from")]
        [Required]
        public string NumberFrom { get; set; }

        [Display(Name = "Account number to")]
        [Required]
        public string NumberTo { get; set; }

        [Display(Name = "Amount of money")]
        [Required]
        public decimal AmountOfMoney { get; set; }
    }
}