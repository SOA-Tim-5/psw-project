using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IRecordRepository
    {
        PagedResult<Record> GetPagedByTouristId(int page, int pageSize, long touristId);
    }
}
