using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Data
{
    public class UnitOfWork : IDisposable
    {
        private WarehouseDocumetDbContext db = null;
        public UnitOfWork()
        {
            this.db = new WarehouseDocumetDbContext();
        }
        // Obsługę każdego repozytorium dodajemy tutaj
        GenericRepository<WarehouseDocument> warehouseDocumentRepository = null;
        // Gettery dla każdego repozytorium dodajemy tutaj
        public GenericRepository<WarehouseDocument> WarehouseDocumentRepository
        {
            get
            {
                if (warehouseDocumentRepository == null)
                    warehouseDocumentRepository = new GenericRepository<WarehouseDocument>(db);
                return warehouseDocumentRepository;
            }
        }



        // Obsługę każdego repozytorium dodajemy tutaj
        GenericRepository<Article> articleRepository = null;
        // Gettery dla każdego repozytorium dodajemy tutaj
        public GenericRepository<Article> ArticleRepository
        {
            get
            {
                if (articleRepository == null)
                    articleRepository = new GenericRepository<Article>(db);
                return articleRepository;
            }
        }





        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await  db.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
