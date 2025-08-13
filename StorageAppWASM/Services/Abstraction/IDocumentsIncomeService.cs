using ClassLibrary.Dto;

namespace StorageAppWASM.Services.Abstraction
{
    public interface IDocumentsIncomeService
    {
        Task<IEnumerable<DocumentIncomeDto>> GetAllAsync();
        Task AddAsync(DocumentIncomeInsertDto documentIncomeInsertDto);
        Task<DocumentIncomeDto> GetByIdAsync(int Id);
        Task RemoveAsync(int Id);
        Task UpdateAsync(DocumentIncomeDto documentIncomeDto);
    }
}
