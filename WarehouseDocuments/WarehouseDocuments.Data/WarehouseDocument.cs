using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Data
{
    [Table("WarehouseDocument")]
    public class WarehouseDocument :IEntity
    {

        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientNo { get; set; }
        public string Name { get; set; }
        public decimal NetPrice { get; set; }
        public decimal GrossPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual List <Article> Articles { get; set; }

    }
}
