using DataAccess.DBContext;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repo
{
    public interface IClientRepo
    {
        Task<ClientEntity?> GetClientById(string id);
        Task<List<ClientEntity>> GetAllClients();
        Task SaveChangesAsync();
        Task AddClient(ClientEntity entity);
    }
    public class ClientRepo : IClientRepo
    {
        private readonly ProjectDBContext _dbContext;

        public ClientRepo(ProjectDBContext context)=>
            _dbContext = context;
        
        public async Task AddClient(ClientEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ClientEntity>> GetAllClients()=>
            await _dbContext.Clients 
                    .AsNoTracking()
                    .ToListAsync();


        public async Task<ClientEntity?> GetClientById(string id) =>
          await _dbContext.Clients.FindAsync(id);


        public async Task SaveChangesAsync() =>
          await _dbContext.SaveChangesAsync();
    }
}
