using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class AdministrativeRespRepository : IAdministrativeResp
    {
        private readonly DapperContext context;

        public AdministrativeRespRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_AdministrativeResp>> AdministrativeResp()
        {
            try
            {
                var query = QueryConstant.AdministrativeResp;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_AdministrativeResp>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveAdministrativeResp(tbl_AdministrativeResp administrativeResp)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertAdministrativeResp);
                parameters.Add("@teacher_id", administrativeResp.teacher_id);
                parameters.Add("@selected_option", administrativeResp.selected_option);
                parameters.Add("@selected_sem", administrativeResp.selected_sem);
                parameters.Add("@selected_year", administrativeResp.selected_year);
                parameters.Add("@details", administrativeResp.details);
                parameters.Add("@file_path", administrativeResp.file_path);
                parameters.Add("@date", administrativeResp.date);



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
