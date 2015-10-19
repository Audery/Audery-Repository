using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Linq;

namespace DSWebService.BLL
{
    public class ServiceState
    {
        [MongoDB.Attributes.MongoId]
        public string iden { get; set; }
        public DateTime UpdateTime { get; set; }
        public void insert()
        {
            using (MDbBase db = new MDbBase())
            {
                if (db.GetCollection<ServiceState>().Linq().Where(x => x.iden == this.iden).Count() == 0)
                {
                    UpdateTime = DateTime.Now;
                    db.GetCollection<ServiceState>().Insert(this);
                }
                else
                {
                    UpdateTime = DateTime.Now;
                    db.GetCollection<ServiceState>().Update<ServiceState>(this, x => x.iden == iden);
                }
            }
        }
        public ServiceState GetModel(string iden)
        {
            using (MDbBase db = new MDbBase())
            {
                return db.GetCollection<ServiceState>().FindOne(x => x.iden == iden);
            }
        }
    }
}