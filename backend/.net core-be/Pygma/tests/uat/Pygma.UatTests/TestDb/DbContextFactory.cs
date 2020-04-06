using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pygma.Data;

namespace Pygma.UatTests.TestDb
{
    public class DbContextFactory
    {
        public PygmaDbContext CreateContextForSqServer()  
        {  
            var connection = new SqlConnection("Server=.;Database=Pygma_Test;Integrated Security=SSPI");  
            connection.Open();  
  
            var option = new DbContextOptionsBuilder<PygmaDbContext>().UseSqlServer(connection).Options;  
  
            var context = new PygmaDbContext(option);

            context.Database.EnsureDeleted();  
            context.Database.EnsureCreated();

            return context;  
        }  
    }
}