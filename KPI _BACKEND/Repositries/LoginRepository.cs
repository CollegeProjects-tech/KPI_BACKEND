using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class LoginRepository : ILogin
    {
        private readonly DapperContext context;

        public LoginRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<tbl_Login> Login(tbl_Login login)
        {
            try
            {
                var query = "sp_kpi";


                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.Login);
                parameters.Add("@username", login.username);
                parameters.Add("@password", login.password);



                using (var connection = context.CreateConnection())
                {
                    var result = await connection.QuerySingleOrDefaultAsync<tbl_Login>(query, parameters, commandType: CommandType.StoredProcedure);
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
