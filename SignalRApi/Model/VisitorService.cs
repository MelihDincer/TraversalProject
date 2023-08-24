using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRApi.DAL;
using SignalRApi.DAL.Hubs;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Model
{
    public class VisitorService
    {
        private readonly ApiAppContext _appContext;
        private readonly IHubContext<VisitorHub> _hubContext;

        public VisitorService(ApiAppContext appContext, IHubContext<VisitorHub> hubContext)
        {
            _appContext = appContext;
            _hubContext = hubContext;
        }

        public IQueryable<Visitor> GetList()
        {
            return _appContext.Visitors.AsQueryable();
        }

        public async Task SaveVisitor(Visitor visitor)
        {
            await _appContext.Visitors.AddAsync(visitor); // Visitor parametresinden gelen değerleri ekle.
            await _appContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("CallVisitorList","aaaaa");    //SignalR'da çağrılacak olan metotlar SendAsync metoduyla çağrılır.
        }

        //Ziyaretçileri 
        public List<VisitorChart> GetVisitorChartList()
        {
            List<VisitorChart> visitorCharts = new List<VisitorChart>();
            using (var command = _appContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Select * From crosstab ( 'Select VisitDate,City,CityVisitCount From Visitors Order By 1, 2') As ct(VisitDate date,City1 int, City2 int, City3 int, City4 int, City5 int);";
                command.CommandType = System.Data.CommandType.Text;
                _appContext.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VisitorChart visitorChart = new VisitorChart();
                        visitorChart.VisitDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            visitorChart.Counts.Add(reader.GetInt32(x));
                        });
                        visitorCharts.Add(visitorChart);
                    }
                }
                _appContext.Database.CloseConnection();
                return visitorCharts;
            }
        }
    }
}
