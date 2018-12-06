using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new AppDbInitializer());
        }

        public ApplicationContext() : base("DbConnection") { }

        public DbSet<AccountOwnerDTO> AccountOwners { get; set; }
        public DbSet<AccountTypeDTO> AccountTypes { get; set; }
        public DbSet<BankAccountDTO> BankAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountDTO>().ToTable("BankAccount");
            modelBuilder.Entity<BankAccountDTO>().HasKey(x => x.Number);
            modelBuilder.Entity<BankAccountDTO>().HasRequired(x => x.Owner).WithMany();
            modelBuilder.Entity<BankAccountDTO>().HasRequired(x => x.AccountType).WithMany();

            modelBuilder.Entity<AccountOwnerDTO>().ToTable("AccountOwner");
            modelBuilder.Entity<AccountOwnerDTO>().HasKey(x => x.Id);
            modelBuilder.Entity<AccountOwnerDTO>().Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AccountTypeDTO>().ToTable("AccountType");
            modelBuilder.Entity<AccountTypeDTO>().HasKey(x => x.TypeName);
        }

        public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
        {
        }
    }
}
