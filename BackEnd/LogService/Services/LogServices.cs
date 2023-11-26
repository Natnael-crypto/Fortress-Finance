using Dapper;
using LogService.Model;

namespace LogService.Services;

public class LogServices{
    private LogDbContext _logdbcontext;

    public LogServices(LogDbContext logDbContext){
        _logdbcontext=logDbContext;
    }

    public async Task<IEnumerable<Log>> GetAll()
    {
        using (var connection = _logdbcontext.GetConnection())
        {
            connection.Open();
            return await connection.QueryAsync<Log>("SELECT Id,LogType,IpAddress FROM LogTable");
        }
    }

    public async Task<Log> GetOne(int id)
    {
        using (var connection = _logdbcontext.GetConnection())
        {
            connection.Open();
            
            var res= await connection.QueryFirstOrDefaultAsync<Log>("SELECT * FROM LogTable WHERE Id = @Id", new { Id = id });

            if(res is null){
                return null;
            }

            return res;
        }
    }


    public async Task<int> Create(Log model)
    {
        using (var connection = _logdbcontext.GetConnection())
        {
            connection.Open();
            return await connection.ExecuteAsync("INSERT INTO LogTable ( LogType, IpAddress, RequestDetail, LogDateTime, Username, Role, DeviceInfo) VALUES ( @LogType, @IpAddress, @RequestDetail, @LogDateTime, @Username, @Role, @DeviceInfo)", model);
        }
    }

}
