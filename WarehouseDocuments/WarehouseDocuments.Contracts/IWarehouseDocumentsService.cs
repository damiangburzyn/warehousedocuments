using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Contracts
{
    public interface IWarehouseDocumentsService
    {
        Task<IEnumerable<WarehouseDocumentViewModel>> GetWarehouseDocumets();
        Task <WarehouseDocumentViewModel> GetWareHouseDocumentById(int id);
        Task UpdateWarehouseDocumet(WarehouseDocumentViewModel vm);
        Task<int> SaveWarehouseDocumet(WarehouseDocumentViewModel vm);
        Task DeleteWareHouseDocument(int id);
    }
}
