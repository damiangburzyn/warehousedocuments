using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using WarehouseDocuments.Contracts;
using WarehouseDocuments.Data;
using WarehouseDocuments.Services;

namespace WarehouseDocuments
{
    static class Program
    { private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log.Info("App Start");
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IWarehouseDocumentsService, WarehouseDocumentsService>();
            container.RegisterType<IArticlesService, ArticlesService>();


            WarehouseDocumetDbContext con = new WarehouseDocumetDbContext();
            con.Database.Initialize(true);
            con.Database.CreateIfNotExists();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormListDocument>() );
        }
    }
}
