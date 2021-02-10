using App.Entites.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.GeneralLeadger
{
    public interface IGeneralLedgerService
    {
        public Task<List<GeneralLedgerDTO>> GetAllGeneralLedgersAsync();
        public Task<GeneralLedgerDTO> GetGeneralLedgerAsync(int transectionId);
        public Task<GeneralLedgerDTO> CreateGeneralLedgerAsync(GeneralLedgerDTO generaLedgerDTO);
        public Task<GeneralLedgerDTO> UpdateGeneralLedgerAsync(GeneralLedgerDTO generaLedgerDTO);
        public Task<bool> DeleteGeneralLedgerAsync(int transectionId);

    }
}
