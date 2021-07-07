using HomeEnvironmentLifePlanner.Server.Models;
using HomeEnvironmentLifePlanner.Shared.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BankStatementHeader> BankStatmentHeaders { get; set; }
        public DbSet<BankStatementPosition> BankStatementPositions { get; set; }
        public DbSet<BankStatementSubPosition> BankStatementSubPositions{ get; set; }
        public DbSet<ShoppingListHeader> ShoppingListHeaders { get; set; }
        public DbSet<ShoppingListPosition> ShoppingListPositions { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<ContractorGroup> ContractorGroups { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<TransactionHeader> TransactionHeaders { get; set; }
        public DbSet<TransactionPosition> TransactionPositions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<BankStatementHeader>().ToTable("BankStatmentHeaders");
            builder.Entity<BankStatementPosition>().ToTable("BankStatmentPositions");
            builder.Entity<BankStatementSubPosition>().ToTable("BankStatmentSubPositions");
            builder.Entity<ShoppingListHeader>().ToTable("ShoppingListHeaders");
            builder.Entity<ShoppingListPosition>().ToTable("ShoppingListPositions");

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<CategoryType>().ToTable("CategoryTypes");
            builder.Entity<Contractor>().ToTable("Contractors");
            builder.Entity<ContractorGroup>().ToTable("ContractorGroups");
            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<PaymentType>().ToTable("PaymentTypes");
            builder.Entity<TransactionHeader>().ToTable("TransactionHeaders");
            builder.Entity<TransactionPosition>().ToTable("TransactionPositions");

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<ProductGroup>().ToTable("ProductGroups");
            builder.Entity<ProductPrice>().ToTable("ProductPrices");
            builder.Entity<Category>().HasOne(i => i.Category1).WithMany(i => i.CaT_Children).HasForeignKey(i => i.CaT_ParentId).HasPrincipalKey(i => i.CaT_Id).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ContractorGroup>().HasOne(i => i.ContractorGroup1).WithMany(i => i.CtG_Children).HasForeignKey(i => i.CtG_ParentId).HasPrincipalKey(i => i.CtG_Id).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductGroup>().HasOne(i => i.ProductGroup1).WithMany(i => i.PrG_Children).HasForeignKey(i => i.PrG_ParentId).HasPrincipalKey(i => i.PrG_Id).OnDelete(DeleteBehavior.Restrict);

            //----------------------------init values-----------------------------------------------------


            builder.Entity<ContractorGroup>().HasData(new ContractorGroup { CtG_Id = 1, CtG_Name = "Grupa Główna", CtG_ParentId = null });
            builder.Entity<ProductGroup>().HasData(new ProductGroup { PrG_Id = 1, PrG_Name = "Grupa Główna", PrG_ParentId = null ,PrG_Code="GŁÓWNA"});

            builder.Entity<Currency>().HasData(new Currency { CuR_Id = 1, CuR_Name = "PLN" }, new Currency { CuR_Id = 2, CuR_Name = "EUR" }, new Currency { CuR_Id = 3, CuR_Name = "GBP" });
            builder.Entity<Account>().HasData(
                new Account { AcC_Id = 1, AcC_Name = "Gotówka_Rafał", AcC_ReferenceNumber = " " },
                new Account { AcC_Id = 2, AcC_Name = "Gotówka_Paulina", AcC_ReferenceNumber = " " },
                new Account { AcC_Id = 3, AcC_Name = "Gotówka_Wakacje", AcC_ReferenceNumber = " " },
                new Account { AcC_Id = 4, AcC_Name = "Konto", AcC_ReferenceNumber = " " });
            builder.Entity<CategoryType>().HasData(
                new CategoryType { CtY_Id = 1, CtY_Name = "Przychód/Wydatek", CtY_Value = 0 },
                new CategoryType { CtY_Id = 2, CtY_Name = "Przychód", CtY_Value = 1 },
                new CategoryType { CtY_Id = 3, CtY_Name = "Wydatek", CtY_Value = 2 }
                );
            builder.Entity<Category>().HasData(
                //  new Category { CaT_Id = , CaT_Name = "", CaT_CTYID = 1, CaT_Description = "", CaT_ParentId =  },
                new Category { CaT_Id = 1, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Grupa Główna", CaT_CTYID = 1, CaT_Description = "Grupa o najwyższym poziomie", CaT_ParentId = null },
                new Category { CaT_Id = 2, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Całkowite przychody", CaT_CTYID = 2, CaT_Description = "Wszystkie źródła przychodów", CaT_ParentId = 1 },
                new Category { CaT_Id = 3, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Jedzenie", CaT_CTYID = 3, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 4, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Mieszkania", CaT_CTYID = 3, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 5, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Transport", CaT_CTYID = 2, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 6, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Telekomunikacja", CaT_CTYID = 3, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 7, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Opieka zdrowotna", CaT_CTYID = 3, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 8, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Ubrania", CaT_CTYID = 2, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 9, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Higiena", CaT_CTYID = 2, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 10, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Rozrywka", CaT_CTYID = 2, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 11, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Inne wydatki", CaT_CTYID = 2, CaT_Description = "", CaT_ParentId = 1 },
                new Category { CaT_Id = 12, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Pensja Rafał", CaT_CTYID = 2, CaT_Description = "Wypłata bez premii", CaT_ParentId = 2 },
                new Category { CaT_Id = 13, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Pensja Paulina", CaT_CTYID = 2, CaT_Description = "Wypłata bez premii", CaT_ParentId = 2 },
                new Category { CaT_Id = 14, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Premia Roczna Rafał", CaT_CTYID = 2, CaT_Description = "Premia roczna (tzw świąteczna)", CaT_ParentId = 2 },
                new Category { CaT_Id = 15, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Premia Roczna Paulina", CaT_CTYID = 2, CaT_Description = "Trzynastka", CaT_ParentId = 2 },
                new Category { CaT_Id = 16, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Wynajem Mieszkania Szarych Szeregów", CaT_CTYID = 2, CaT_Description = "Kwota tylko wynajmu ( bez opłat za rachunki)", CaT_ParentId = 2 },
                new Category { CaT_Id = 17, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Wpłata Najmcy Prąd", CaT_CTYID = 2, CaT_Description = "Wpłata najemcy na poczet rachunku za prąd", CaT_ParentId = 2 },
                new Category { CaT_Id = 18, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Wpłata Najmcy Czynsz", CaT_CTYID = 2, CaT_Description = "Wpłata najemcy na poczet czynszu( wraz z zaliczką na ogrzewanie i wode)", CaT_ParentId = 2 },
                new Category { CaT_Id = 19, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Wynajem Garażu", CaT_CTYID = 2, CaT_Description = " ", CaT_ParentId = 2 },
                new Category { CaT_Id = 20, CaT_ReferenceNumber = "<BRAK>", CaT_Name = "Pięćset plus", CaT_CTYID = 2, CaT_Description = "500+ ", CaT_ParentId = 2 }

                //new Category { CaT_Id =, CaT_Name = "Mieszkanie Szarych Szeregów (do 31.09.2020)", CaT_CTYID = 2, CaT_Description = "Koszty związane z lokalem na Szarych Szeregów 32/9 (przed wynajmem)", CaT_ParentId = 3 },
                //new Category { CaT_Id = , CaT_Name = "Mieszkanie Szarych Szeregów (od 01.10.2020)", CaT_CTYID = 2, CaT_Description = "Koszty związane z lokalem na Szarych Szeregów 32/9 (wynajem)", CaT_ParentId = 3 },

                //new Category { CaT_Id = , CaT_Name = "Mieszkanie Dzierzążenko", CaT_CTYID = 2, CaT_Description = "Koszty związane z lokalem na Dzierzążenko 1E", CaT_ParentId = 3 },



                //new Category { CaT Id = 11, CaT Name = "", CaT CTYID = 1, CaT Description = "500+ ", CaT ParentId = 0 },


                );


        }
    }
}
