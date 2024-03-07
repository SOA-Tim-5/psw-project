using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class TransactionRecordDatabaseRepository : ITransactionRecordRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<TransactionRecord> _dbSet;
        public TransactionRecordDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TransactionRecord>();
        }
        public PagedResult<TransactionRecord> GetPagedTransactionsByTourist(int page, int pageSize, long touristId)
        {
            var task = _dbSet.Where(x => x.RecieverId == touristId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
