using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReservationService : IGenericService<Reservation>
    {
        List<Reservation> GetListWithReservationByWaitApproval(int id); //Onay bekleyen rezervasyonlar listesi
        List<Reservation> GetListWithReservationByAccepted(int id); //Onaylanan rezervasyonlar listesi
        List<Reservation> GetListWithReservationByPrevious(int id); //Geçmiş rezervasyonlar listesi

    }
}
