using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IResearch
    {
        Task<IEnumerable<tbl_Research>> Researches();
        Task SaveResearch(tbl_Research research);
    }
}
