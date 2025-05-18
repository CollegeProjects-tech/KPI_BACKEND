using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IAdmin
    {
        Task<IEnumerable<tbl_Users>> Users();
        Task<int> SaveUsers(tbl_Users users);
    }
}
