using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Data
{
    [Table("Article")]
  public  class Article : IEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WarehouseDocument")]
        public int WarehouseDocumentId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual WarehouseDocument WarehouseDocument { get; set; }

    }
}
