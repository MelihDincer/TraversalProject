using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IContactUsDal : IGenericDal<ContactUs>
    {
        List<ContactUs> GetListContactUsByTrue(); //Statusu true olanları getir.
        List<ContactUs> GetListContactUsByFalse(); //Statusu false olanları getir.
        void ContactUsStatusChangeToFalse(int id); //Statusu false yap.
    }
}
