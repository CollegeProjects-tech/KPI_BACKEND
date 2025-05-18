using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IAdministrativeResp
    {
        Task<IEnumerable<tbl_AdministrativeResp>> AdministrativeResp();
        Task SaveAdministrativeResp(tbl_AdministrativeResp administrativeResp);
    }
}
