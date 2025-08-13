using ClassLibrary.Models;

namespace StorageAppWASM.Repositories.Abstraction
{
    public interface IDocumentsIncomeRepository
    {
        Task<IEnumerable<DocumentIncome>> GetAllAsync();
        Task<DocumentIncome> AddAsync(DocumentIncome documentIncome);
        Task<DocumentIncome> GetByIdAsync(int Id);
        Task RemoveAsync(int Id);
        Task UpdateAsync(DocumentIncome documentIncome);
    }
}
