using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface ILogin
    {
        Task<tbl_Login> Login(int userid, string password);
    }
}
