using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using StorageAppWASM.Data;
using StorageAppWASM.Repositories.Abstraction;

namespace StorageAppWASM.Repositories.Implementation
{
    public class DocumentsIncomeRowRepository : IDocumentsIncomeRowRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public DocumentsIncomeRowRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public Task<DocumentIncome> AddAsync(DocumentIncomeRow documentIncomeRow)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentIncomeRow>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DocumentIncomeRow> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
