using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class RecordDatabaseRepository : IRecordRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<Record> _dbSet;
        public RecordDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Record>();
        }
        public PagedResult<Record> GetPagedByTouristId(int page, int pageSize, long touristId)
        {
            var task = _dbSet.Where(x => x.TouristId == touristId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
