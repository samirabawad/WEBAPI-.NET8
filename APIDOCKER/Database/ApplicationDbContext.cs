using APIDOCKER.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace APIDOCKER.Database
{
    public class ApplicationDbContext : DbContext
    {
        //tabla 'Customer' a crear, basada en nuestro Modelo Customer.
        public DbSet<Customer> Customers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            //creación de la base de datos.
            try
            {
                //IDatabaseCreator se encarga de crear la base de datos
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(dbCreator != null)
                {
                    //Si no existe/esta disponible la bd, la creamos.
                    if (!dbCreator.CanConnect())
                    {
                        Console.WriteLine("cae en if connect");
                        dbCreator.Create();
                    }
                    //Si no tiene tablas, las creamos.
                    if (!dbCreator.HasTables())
                    {
                        Console.WriteLine("cae en if has tables");
                        dbCreator.CreateTables();
                    }
                    Console.WriteLine("cae fuera de if has tables");
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}
