using Microsoft.EntityFrameworkCore;

namespace Smd.EntityFramework.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}
