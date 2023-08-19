using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TraversalCoreProje.CQRS.Results.DestinationResults;

namespace TraversalCoreProje.CQRS.Handlers.DestinationHandlers
{
    public class GetAllDestinationQueryHandler
    {
        private readonly Context _context;

        public GetAllDestinationQueryHandler(Context context)
        {
            _context = context;
        }
        
        public List<GetAllDestinationQueryResult> Handle()
        {
            var values = _context.Destinations.Select(x=> new GetAllDestinationQueryResult
            {
                id = x.DestinationID,
                capacity = x.Capacity,
                city = x.City,
                daynight = x.DayNight,
                price = x.Price,
            }).AsNoTracking().ToList(); //AsNoTracking metodu: Entity Framework tarafından uygulamaların performansını optimize etmemize yardımcı olmak için geliştirilmiş bir fonksiyondur. İşlevsel olarak veritabanından sorgu neticesinde elde edilen nesnelerin takip mekanizması ilgili fonksiyon tarafından kırılarak, sistem tarafından izlenmelerine son verilmesini sağlamakta ve böylece tüm verisel varlıkların ekstradan işlenme yahut lüzumsuz depolanma süreçlerine maliyet ayrılmamaktadır.
            //Özetle; burada db tarafında herhangi bir değişiklik yapılmadığından dolayı, sadece değerleri geri döndürüğümüz için izlememize gerek kalmayacağı için bu metodu kullandık.
            return values;

        }
    }
}
