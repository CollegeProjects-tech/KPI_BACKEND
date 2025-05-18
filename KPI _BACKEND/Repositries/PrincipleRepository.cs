using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;
using System.Security.Principal;

namespace KPI_BACKEND.Repositries
{
    public class PrincipleRepository : IPrinciple
    {
        private readonly DapperContext context;

        public PrincipleRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_ViewDetails>> ViewDetails(int teacher_id)
        {
            try
            {
                var query = "sp_kpi";


                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.ViewDetails);
                parameters.Add("@teacher_id", teacher_id);



                using (var connection = context.CreateConnection())
                {
                    var result = await connection.QueryAsync<tbl_ViewDetails>(query, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<tbl_Users>> DipartmentsWiseTeachers(string department)
        {
            try
            {
                var query = "sp_kpi";


                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.DipartmentsWiseTeachers);
                parameters.Add("@department", department);



                using (var connection = context.CreateConnection())
                {
                    var result = await connection.QueryAsync<tbl_Users>(query, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
