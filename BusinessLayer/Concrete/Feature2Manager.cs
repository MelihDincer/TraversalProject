using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class Feature2Manager : IFeature2Service
    {
        IFeature2Dal _feature2Dal;

        public Feature2Manager(IFeature2Dal feature2Dal)
        {
            _feature2Dal = feature2Dal;
        }

        public void TAdd(Feature2 t)
        {
            throw new NotImplementedException();
        }

        public void TDelete(Feature2 t)
        {
            throw new NotImplementedException();
        }

        public Feature2 TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Feature2> TGetList()
        {
            return _feature2Dal.GetList();
        }

        public void TUpdate(Feature2 t)
        {
            throw new NotImplementedException();
        }
    }
}
