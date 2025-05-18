using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IExamSecResp
    {
        Task<IEnumerable<tbl_ExamSecResp>> ExamSecResp();
        Task SaveExamSecResp(tbl_ExamSecResp examSecResp);
    }
}
