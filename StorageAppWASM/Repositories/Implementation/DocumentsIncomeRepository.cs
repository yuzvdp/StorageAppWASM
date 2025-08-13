using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using StorageAppWASM.Data;
using StorageAppWASM.Repositories.Abstraction;

namespace StorageAppWASM.Repositories.Implementation
{
    public class DocumentsIncomeRepository : IDocumentsIncomeRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public DocumentsIncomeRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<DocumentIncome> AddAsync(DocumentIncome documentIncome)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            DocumentIncome newDocumentIncome = new()
            {
                Number = documentIncome.Number,
                Date = documentIncome.Date
            };

            if (documentIncome.DocumentIncomeRows.Count > 0)
            {
                foreach (var row in documentIncome.DocumentIncomeRows)
                {
                    DocumentIncomeRow _row = new()
                    {
                        ResourceId = row.ResourceId,
                        UnitId = row.UnitId,
                        Count = row.Count
                    };

                    newDocumentIncome.DocumentIncomeRows.Add(_row);
                }
            }
            await db.DocumentIncomes.AddAsync(newDocumentIncome);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(newDocumentIncome);
        }

        public async Task<IEnumerable<DocumentIncome>> GetAllAsync()
        {
            var db = await _dbFactory.CreateDbContextAsync();

            return await db.DocumentIncomes
                .Include(r => r.DocumentIncomeRows)
                .Include(x => x.DocumentIncomeRows)
                    .ThenInclude(p => p.Resource)
                .Include(x => x.DocumentIncomeRows)
                    .ThenInclude(p => p.Unit)
                .ToListAsync();
        }

        public async Task<DocumentIncome> GetByIdAsync(int Id)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            return await db.DocumentIncomes
                .Include(x => x.DocumentIncomeRows)
                .Include(x => x.DocumentIncomeRows)
                    .ThenInclude(p => p.Resource)
                .Include(x => x.DocumentIncomeRows)
                    .ThenInclude(p => p.Unit)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task RemoveAsync(int Id)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            var item = await db.DocumentIncomes.FirstOrDefaultAsync(x => x.Id == Id);
            if (item == null) return;

            db.DocumentIncomes.Remove(item);

            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocumentIncome documentIncome)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            var item = await db.DocumentIncomes.FirstOrDefaultAsync(p => p.Id == documentIncome.Id);

            if (item == null) return;

            //item.Title = resource.Title;
            //item.IsActive = resource.IsActive;

            await db.SaveChangesAsync();
        }
    }
}
