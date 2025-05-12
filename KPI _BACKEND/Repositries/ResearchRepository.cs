using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using System.Data;
using Models;
using KPI_BACKEND.Interfaces;

namespace KPI_BACKEND.Repositries
{
    public class ResearchRepository : IResearch
    {
        private readonly DapperContext context;

        public ResearchRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_Research>> Researches()
        {
            try
            {
                var query = QueryConstant.Researches;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_Research>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveResearch(tbl_Research research)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertResearch);
                parameters.Add("@teacher_id", research.teacher_id);
                parameters.Add("@selected_option", research.selected_option);
                parameters.Add("@selected_sem", research.selected_sem);
                parameters.Add("@selected_year", research.selected_year);
                parameters.Add("@details", research.details);
                parameters.Add("@file_path", research.file_path);
                parameters.Add("@date", research.date);



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
