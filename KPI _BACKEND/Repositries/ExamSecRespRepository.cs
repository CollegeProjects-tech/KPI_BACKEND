using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class ExamSecRespRepository : IExamSecResp
    {
        private readonly DapperContext context;

        public ExamSecRespRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_ExamSecResp>> ExamSecResp()
        {
            try
            {
                var query = QueryConstant.ExamSecResp;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_ExamSecResp>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveExamSecResp(tbl_ExamSecResp examSecResp)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertExamSecResp);
                parameters.Add("@teacher_id", examSecResp.teacher_id);
                parameters.Add("@selected_option", examSecResp.selected_option);
                parameters.Add("@selected_sem", examSecResp.selected_sem);
                parameters.Add("@selected_year", examSecResp.selected_year);
                parameters.Add("@details", examSecResp.details);
                parameters.Add("@file_path", examSecResp.file_path);
                parameters.Add("@date", examSecResp.date);



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
