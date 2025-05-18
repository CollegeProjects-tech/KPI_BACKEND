using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IInternalRevnuGen
    {
        Task<IEnumerable<tbl_InternalRevnuGen>> InternalRevnuGen();
        Task SaveInternalRevnuGen(tbl_InternalRevnuGen internalRevnuGen);
    }
}
