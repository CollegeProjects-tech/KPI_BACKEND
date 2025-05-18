using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class FdpSttpWorkshopRepository : IFdpSttpWorkshop
    {
        private readonly DapperContext context;

        public FdpSttpWorkshopRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_FdpSttpWorkshop>> FdpSttpWorkshop()
        {
            try
            {
                var query = QueryConstant.FdpSttpWorkshop;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_FdpSttpWorkshop>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveFdpSttpWorkshop(tbl_FdpSttpWorkshop fdpSttpWorkshop)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertFdpSttpWorkshop);
                parameters.Add("@teacher_id", fdpSttpWorkshop.teacher_id);
                parameters.Add("@selected_option", fdpSttpWorkshop.selected_option);
                parameters.Add("@selected_sem", fdpSttpWorkshop.selected_sem);
                parameters.Add("@selected_year", fdpSttpWorkshop.selected_year);
                parameters.Add("@details", fdpSttpWorkshop.details);
                parameters.Add("@file_path", fdpSttpWorkshop.file_path);
                parameters.Add("@date", fdpSttpWorkshop.date);



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
