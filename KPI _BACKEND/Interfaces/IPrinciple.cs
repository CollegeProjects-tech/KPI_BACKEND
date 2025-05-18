using Models;

namespace KPI_BACKEND.Interfaces
{
    public interface IPrinciple
    {
        Task<IEnumerable<tbl_ViewDetails>> ViewDetails(int teacher_id);
        Task<IEnumerable<tbl_Users>> DipartmentsWiseTeachers(string department);

    }
}
