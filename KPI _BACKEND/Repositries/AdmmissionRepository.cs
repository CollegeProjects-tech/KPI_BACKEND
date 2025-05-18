using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class AdmmissionRepository : IAdmmission
    {
        private readonly DapperContext context;

        public AdmmissionRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_Admmission>> Admmission()
        {
            try
            {
                var query = QueryConstant.Admmission;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_Admmission>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveAdmmission(tbl_Admmission admmission)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertAdmmission);
                parameters.Add("@teacher_id", admmission.teacher_id);
                parameters.Add("@selected_option", admmission.selected_option);
                parameters.Add("@selected_sem", admmission.selected_sem);
                parameters.Add("@selected_year", admmission.selected_year);
                parameters.Add("@details", admmission.details);
                parameters.Add("@file_path", admmission.file_path);
                parameters.Add("@date", admmission.date);



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
