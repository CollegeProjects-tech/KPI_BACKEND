using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IStudentDevelopment
    {
        Task<IEnumerable<tbl_StudentDevelopment>> StudentDevelopment();
        Task SaveStudentDevelopment(tbl_StudentDevelopment studentDevelopment);
    }
}
