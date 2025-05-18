using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IFdpSttpWorkshop
    {
        Task<IEnumerable<tbl_FdpSttpWorkshop>> FdpSttpWorkshop();
        Task SaveFdpSttpWorkshop(tbl_FdpSttpWorkshop fdpSttpWorkshop);
    }
}
