namespace WarehouseDocuments.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
  

    public class WarehouseDocumetDbContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WarehouseDocuments.Data.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public WarehouseDocumetDbContext()
            : base("name=WarehouseDocumetDbContext")
        {
          //  Database.SetInitializer<WarehouseDocumetDbContext>(new DropCreateDatabaseIfModelChanges<WarehouseDocumetDbContext>( ));           
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<WarehouseDocument> WarehouseDocumets { get; set; }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}