using ClassLibrary.Dto;
using ClassLibrary.Models;
using StorageAppWASM.Repositories.Abstraction;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Services.Implementation
{
    public class DocumentsIncomeService : IDocumentsIncomeService
    {
        private readonly IDocumentsIncomeRepository _documentsIncomeRepository;
        private readonly IBalancesRepository _balanceRespository;

        public DocumentsIncomeService(IDocumentsIncomeRepository documentsIncomeRepository, IBalancesRepository balancesRepository)
        {
            _documentsIncomeRepository = documentsIncomeRepository;
            _balanceRespository = balancesRepository;
        }

        public async Task AddAsync(DocumentIncomeInsertDto documentIncomeInsertDto)
        {
            if (documentIncomeInsertDto.Number != 0)
            {
                await _documentsIncomeRepository.AddAsync(new DocumentIncome
                {
                    Number = documentIncomeInsertDto.Number,
                    Date = documentIncomeInsertDto.Date,
                    DocumentIncomeRows = [.. (from p in documentIncomeInsertDto.DocumentIncomeRowDtos
                                          select new DocumentIncomeRow
                                          {
                                              ResourceId = p.ResourceId,
                                              UnitId = p.UnitId,
                                              Count = p.Count
                                          })],
                });

                foreach (var row in documentIncomeInsertDto.DocumentIncomeRowDtos)
                {
                    var existBalance = await _balanceRespository.GetByResourceAndUnitAsync(row.ResourceId, row.UnitId);

                    if (existBalance != null)
                    {
                        existBalance.IncreaseCount(row.Count);
                        await _balanceRespository.UpdateAsync(existBalance);
                    }
                    else
                    {
                        await _balanceRespository.AddAsync(new Balance
                        {
                            Count = row.Count,
                            UnitId = row.UnitId,
                            ResourceId = row.ResourceId,
                        });
                    }
                }

            }

        }

        public async Task<IEnumerable<DocumentIncomeDto>> GetAllAsync()
        {
            return from item in await _documentsIncomeRepository.GetAllAsync()
                   select new DocumentIncomeDto
                   {
                       Id = item.Id,
                       Date = item.Date,
                       Number = item.Number,
                       DocumentIncomeRowDtos = [.. from p in item.DocumentIncomeRows
                                               select new DocumentIncomeRowDto
                                               {
                                                   Id = p.Id,
                                                   ResourceTitle = p.Resource?.Title,
                                                   UnitTitle = p.Unit?.Title,
                                                   Count = p.Count,
                                               }]
                   };
        }

        public Task<DocumentIncomeDto> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(int Id)
        {
            var doc = await _documentsIncomeRepository.GetByIdAsync(Id);

            if (doc != null)
            {
                if (doc.DocumentIncomeRows.Count > 0)
                {
                    foreach (var row in doc.DocumentIncomeRows)
                    {
                        var existBalance = await _balanceRespository.GetByResourceAndUnitAsync(row.ResourceId, row.UnitId);

                        if (existBalance != null)
                        {
                            if (existBalance.Count <= row.Count)
                            {
                                await _balanceRespository.RemoveAsync(existBalance.Id);
                            }
                            else
                            {
                                existBalance.DecreaseCount(row.Count);
                                await _balanceRespository.UpdateAsync(existBalance);
                            }
                        }
                    }
                }
                await _documentsIncomeRepository.RemoveAsync(Id);
            }

        }

        public Task UpdateAsync(DocumentIncomeDto documentIncomeDto)
        {
            throw new NotImplementedException();
        }
    }
}
