using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContactUsService : IGenericService<ContactUs>
    {
        List<ContactUs> GetListContactUsByTrue(); //Statusu true olanları getir.
        List<ContactUs> GetListContactUsByFalse(); //Statusu false olanları getir.
        void ContactUsStatusChangeToFalse(int id); //Statusu false yap.
    }
}
