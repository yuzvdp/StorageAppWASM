using ClassLibrary.Models;

namespace StorageAppWASM.Repositories.Abstraction
{
    public interface IDocumentsIncomeRowRepository
    {
        Task<IEnumerable<DocumentIncomeRow>> GetAllAsync();
        Task<DocumentIncome> AddAsync(DocumentIncomeRow documentIncomeRow);
        Task<DocumentIncomeRow> GetByIdAsync(int Id);
        Task RemoveAsync(int Id);
    }
}
