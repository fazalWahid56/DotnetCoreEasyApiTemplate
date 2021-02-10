using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entites.DTO;
using AutoMapper;
using App.Db.Tables;
using App.Db.Repositories;

namespace App.Services.GeneralLeadger
{
    public class GeneralLedgerService : IGeneralLedgerService
    {
        private readonly IMapper _mapper;
        private readonly IGeneralLedgerRepository _genLedRepo;
        
        public GeneralLedgerService(IGeneralLedgerRepository genLedRepo,  IMapper mapper)
        {
            _mapper = mapper;
            _genLedRepo = genLedRepo;
        }

        #region GeneralLedger
        public async Task<List<GeneralLedgerDTO>> GetAllGeneralLedgersAsync()
        {
            var accounts = await _genLedRepo.GetAllAsync();
            return _mapper.Map<List<GeneralLedgerDTO>>(accounts);
        }
        public async Task<GeneralLedgerDTO> GetGeneralLedgerAsync(int transectionId)
        {
            var account = await _genLedRepo.GetAsync(transectionId);
            return _mapper.Map<GeneralLedgerDTO>(account);
        }
        public async Task<GeneralLedgerDTO> CreateGeneralLedgerAsync(GeneralLedgerDTO generaLedgerDTO)
        {
            var account = _mapper.Map<GeneralLedger>(generaLedgerDTO);
            await _genLedRepo.CreateAsync(account);
            return _mapper.Map<GeneralLedgerDTO>(account);
        }

        public async Task<GeneralLedgerDTO> UpdateGeneralLedgerAsync(GeneralLedgerDTO generaLedgerDTO)
        {
            var account = _mapper.Map<GeneralLedger>(generaLedgerDTO);
            await _genLedRepo.UpdateAsync(account);
            return generaLedgerDTO;
        }

        public async Task<bool> DeleteGeneralLedgerAsync(int transectionId)
        {
            return await _genLedRepo.DeleteAsync(transectionId);
        }
        #endregion

    }
}
