using Dapper;
using KPI_BACKEND.Constants;
using KPI_BACKEND.Contest;
using KPI_BACKEND.Interfaces;
using Models;
using System.Data;

namespace KPI_BACKEND.Repositries
{
    public class AdminRepository : IAdmin
    {
        private readonly DapperContext context;

        public AdminRepository(DapperContext context)
        {
            //_dbContext = dBContext;
            this.context = context;
        }

        public async Task<IEnumerable<tbl_Users>> Users()
        {
            try
            {
                var query = QueryConstant.Users;

                using (var connection = context.CreateConnection())
                {
                    var tests = await connection.QueryAsync<tbl_Users>(query);
                    return tests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveUsers(tbl_Users users)
        {
            try
            {
                var query = "sp_kpi";

                var parameters = new DynamicParameters();
                parameters.Add("@Action", QueryConstant.InsertUser);
                parameters.Add("@user_name", users.user_name);
                parameters.Add("@department", users.department);
                parameters.Add("@contact_no", users.contact_no);
                parameters.Add("@email", users.email);
                parameters.Add("@userid", users.userid);
                parameters.Add("@address", users.address);
                parameters.Add("@password", users.password);
                parameters.Add("@usertype", users.usertype);

                // Add output parameter
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

                using (var connection = context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                    // Get output value after execution
                    int result = parameters.Get<int>("@Result");
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
