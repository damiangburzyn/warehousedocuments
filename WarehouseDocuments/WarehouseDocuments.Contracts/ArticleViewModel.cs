using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Contracts
{
  public  class ArticleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice => decimal.Round(NetPrice * 1.23m, 2, MidpointRounding.AwayFromZero);

        public int WarehouseDocumentId { get; set; }

    }
}
