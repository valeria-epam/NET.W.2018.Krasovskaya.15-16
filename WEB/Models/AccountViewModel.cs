using System.ComponentModel.DataAnnotations;
using BLL.Interface.Entities;

namespace WEB.Models
{
    public class AccountViewModel
    {
        public string Number { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public decimal Bonus { get; set; }

        [Required]
        public AccountState State { get; set; }

        [Required]
        public string AccountType { get; set; }
    }
}