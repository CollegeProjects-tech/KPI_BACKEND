using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IAdmmission
    {
        Task<IEnumerable<tbl_Admmission>> Admmission();
        Task SaveAdmmission(tbl_Admmission admmission);
    }
}
