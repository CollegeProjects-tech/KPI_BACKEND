using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class StudentDevelopmentRepository : IStudentDevelopment
    {
        private readonly DapperContext context;

        public StudentDevelopmentRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_StudentDevelopment>> StudentDevelopment()
        {
            try
            {
                var query = QueryConstant.StudentDevelopment;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_StudentDevelopment>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveStudentDevelopment(tbl_StudentDevelopment studentDevelopment)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertStudentDevelopment);
                parameters.Add("@teacher_id", studentDevelopment.teacher_id);
                parameters.Add("@selected_option", studentDevelopment.selected_option);
                parameters.Add("@selected_sem", studentDevelopment.selected_sem);
                parameters.Add("@selected_year", studentDevelopment.selected_year);
                parameters.Add("@details", studentDevelopment.details);
                parameters.Add("@file_path", studentDevelopment.file_path);
                parameters.Add("@date", studentDevelopment.date);



                using (var connection = context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                    //return await property;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
