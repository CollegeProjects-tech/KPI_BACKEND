using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class AcaGustlecArrangedRepository : IAcaGustlecArranged
    {
        private readonly DapperContext context;

        public AcaGustlecArrangedRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_AcaGustlecArranged>> AcaGustlecArranged()
        {
            try
            {
                var query = QueryConstant.AcaGustlecArranged;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_AcaGustlecArranged>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveAcaGustlecArranged(tbl_AcaGustlecArranged acaGustlecArranged)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertAcaGustlecArranged);
                parameters.Add("@teacher_id", acaGustlecArranged.teacher_id);
                parameters.Add("@selected_option", acaGustlecArranged.selected_option);
                parameters.Add("@selected_sem", acaGustlecArranged.selected_sem);
                parameters.Add("@selected_year", acaGustlecArranged.selected_year);
                parameters.Add("@details", acaGustlecArranged.details);
                parameters.Add("@file_path", acaGustlecArranged.file_path);
                parameters.Add("@date", acaGustlecArranged.date);



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
