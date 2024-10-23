using Microsoft.EntityFrameworkCore;
namespace AdaptClientProjectApi.Models
{
	public class ClientDbContext:DbContext
	{
        public ClientDbContext(DbContextOptions<ClientDbContext> opts):base(opts){}
		public DbSet<Client> Clients { get; set; }
    }
}
