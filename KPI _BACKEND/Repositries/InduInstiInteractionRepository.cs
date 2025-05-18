using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class InduInstiInteractionRepository : IInduInstiInteraction
    {
        private readonly DapperContext context;

        public InduInstiInteractionRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_InduInstiInteraction>> InduInstiInteraction()
        {
            try
            {
                var query = QueryConstant.InduInstiInteraction;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_InduInstiInteraction>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveInduInstiInteraction(tbl_InduInstiInteraction induinstiinteraction)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertInduInstiInteraction);
                parameters.Add("@teacher_id", induinstiinteraction.teacher_id);
                parameters.Add("@selected_option", induinstiinteraction.selected_option);
                parameters.Add("@selected_sem", induinstiinteraction.selected_sem);
                parameters.Add("@selected_year", induinstiinteraction.selected_year);
                parameters.Add("@details", induinstiinteraction.details);
                parameters.Add("@file_path", induinstiinteraction.file_path);
                parameters.Add("@date", induinstiinteraction.date);



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
