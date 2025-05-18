using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IInduInstiInteraction
    {
        Task<IEnumerable<tbl_InduInstiInteraction>> InduInstiInteraction();
        Task SaveInduInstiInteraction(tbl_InduInstiInteraction induinstiinteraction);
    }
}
