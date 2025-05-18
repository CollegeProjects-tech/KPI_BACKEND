using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class InternalRevnuGenRepository : IInternalRevnuGen
    {
        private readonly DapperContext context;

        public InternalRevnuGenRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_InternalRevnuGen>> InternalRevnuGen()
        {
            try
            {
                var query = QueryConstant.InternalRevnuGen;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_InternalRevnuGen>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveInternalRevnuGen(tbl_InternalRevnuGen internalRevnuGen)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertInternalRevnuGen);
                parameters.Add("@teacher_id", internalRevnuGen.teacher_id);
                parameters.Add("@selected_option", internalRevnuGen.selected_option);
                parameters.Add("@selected_sem", internalRevnuGen.selected_sem);
                parameters.Add("@selected_year", internalRevnuGen.selected_year);
                parameters.Add("@details", internalRevnuGen.details);
                parameters.Add("@file_path", internalRevnuGen.file_path);
                parameters.Add("@date", internalRevnuGen.date);



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
