using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DAL.Interface.DTO;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new AppDbInitializer());
        }

        public ApplicationContext() : base("DbConnection")
        {
        }

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

        public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
        {
            protected override void Seed(ApplicationContext context)
            {
                var baseType = new AccountTypeDTO() { TypeName = "Base", BalanceCost = 5, RefillCost = 3 };
                var goldType = new AccountTypeDTO() { TypeName = "Gold", BalanceCost = 10, RefillCost = 5 };
                var platinumType = new AccountTypeDTO() { TypeName = "Platinum", BalanceCost = 15, RefillCost = 10 };
                context.AccountTypes.AddRange(new[] { baseType, platinumType, goldType });

                base.Seed(context);
            }
        }
    }
}
