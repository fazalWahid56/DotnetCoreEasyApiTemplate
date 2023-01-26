using System.Collections.Generic;
using System.Threading.Tasks;
using CoreTemplate.App.Entites.DTO;
using AutoMapper;
using CoreTemplate.App.Db.Tables;
using CoreTemplate.App.Db;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.App.Services.GeneralLeadger
{
    public class GeneralLedgerService : IGeneralLedgerService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public GeneralLedgerService(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _db = appDbContext;
        }

        #region GeneralLedger
        public async Task<List<GeneralLedgerDTO>> GetAllGeneralLedgersAsync()
        {
            var accounts = await _db.GeneralLedger.Where(acc => acc.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<GeneralLedgerDTO>>(accounts);
        }
        public async Task<GeneralLedgerDTO> GetGeneralLedgerAsync(int transectionId)
        {
            var account = await _db.GeneralLedger.FirstOrDefaultAsync(acc => acc.TransectionId == transectionId && acc.IsDeleted == false);
            return _mapper.Map<GeneralLedgerDTO>(account);
        }
        public async Task<GeneralLedgerDTO> CreateGeneralLedgerAsync(GeneralLedgerDTO generaLedgerDTO)
        {
            var account = _mapper.Map<GeneralLedger>(generaLedgerDTO);
            await _db.GeneralLedger.AddAsync(account);
            await _db.SaveChangesAsync();

            return _mapper.Map<GeneralLedgerDTO>(account);
        }

        public async Task<GeneralLedgerDTO> UpdateGeneralLedgerAsync(GeneralLedgerDTO generaLedgerDTO)
        {
            var gl = _mapper.Map<GeneralLedger>(generaLedgerDTO);
            await _db.GeneralLedger.AddAsync(gl);
            _db.Entry(gl).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return generaLedgerDTO;
        }

        public async Task<bool> DeleteGeneralLedgerAsync(int transectionId)
        {          
            var account = await _db.GeneralLedger.FirstOrDefaultAsync(acc => acc.TransectionId == transectionId);
            if (account is not null)
            {
                account.IsDeleted = true;
                _db.Entry(account).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            throw new KeyNotFoundException("TransectionId not found.");
        }
        #endregion

    }
}
