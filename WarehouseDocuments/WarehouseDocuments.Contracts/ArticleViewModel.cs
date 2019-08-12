using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Contracts
{
  public  class ArticleViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice { get; set; }

    }
}
