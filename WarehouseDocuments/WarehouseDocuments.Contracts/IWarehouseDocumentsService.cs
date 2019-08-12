using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Contracts
{
    public interface IWarehouseDocumentsService
    {
        IEnumerable<WarehouseDocumentViewModel> GetWarehouseDocumets();
        WarehouseDocumentViewModel GetWareHouseDocumentById(int id);
        void UpdateWarehouseDocumet(WarehouseDocumentViewModel vm);
        int SaveWarehouseDocumet(WarehouseDocumentViewModel vm);
        void DeleteWareHouseDocument(int id);
    }
}
