using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Contracts
{
    public class WarehouseDocumentViewModel : INotifyPropertyChanged  
    {
        public  int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string ClientNo { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal NetPrice { get; set; }
        public decimal GrossPrice => decimal.Round( NetPrice * 1.23m , 2, MidpointRounding.AwayFromZero) ;

        public virtual List <ArticleViewModel> Articles { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
