using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface ILogin
    {
        Task<tbl_Login> Login(tbl_Login login);
    }
}
