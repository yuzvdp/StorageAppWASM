using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using ClassLibrary.Dto;

namespace StorageAppWASM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ClassLibrary.Models.Client>? Clients { get; set; }
        public DbSet<DocumentIncome>? DocumentIncomes { get; set; }
        public DbSet<DocumentIncomeRow>? DocumentIncomeRows { get; set; }
        public DbSet<Resource>? Resources { get; set; }
        public DbSet<Unit>? Units { get; set; }
        public DbSet<Balance>? Balances { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClassLibrary.Models.Client>().HasIndex(u => new { u.Title, u.Address }).IsUnique();
            modelBuilder.Entity<DocumentIncome>().HasIndex(u => new { u.Number, u.Date }).IsUnique();
            modelBuilder.Entity<DocumentOutcome>().HasIndex(u => new { u.Number, u.Date }).IsUnique();
            modelBuilder.Entity<Resource>().HasIndex(u => new { u.Title }).IsUnique();
            modelBuilder.Entity<Unit>().HasIndex(u => new { u.Title }).IsUnique();
        }
        public DbSet<ClassLibrary.Dto.BalanceDto> BalanceDto { get; set; } = default!;
    }
}
