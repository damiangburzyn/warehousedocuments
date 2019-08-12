using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseDocuments.Data;

namespace WarehouseDocuments
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           

            WarehouseDocumetDbContext con = new WarehouseDocumetDbContext();
            con.Database.Initialize(true);
            con.Database.CreateIfNotExists();

            Application.Run(new FormList());
        }
    }
}
