using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IAcaGustlecArranged
    {
        Task<IEnumerable<tbl_AcaGustlecArranged>> AcaGustlecArranged();
        Task SaveAcaGustlecArranged(tbl_AcaGustlecArranged acaGustlecArranged);
    }
}
