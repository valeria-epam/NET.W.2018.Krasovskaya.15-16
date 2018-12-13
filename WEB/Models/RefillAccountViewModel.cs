using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class RefillAccountViewModel
    {
        [Display(Name = "Account number")]
        [Required]
        public string Number { get; set; }

        [Display(Name = "Amount of money")]
        [Required]
        public decimal AmountOfMoney { get; set; }
    }
}