using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOSOshopService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CommandListen : ICommandListen
    {  
        /// <summary>
        /// 修改商品价格
        /// </summary>        
        public void DoWork(string comm)
        {
            try
            {
                SOSOshop.BLL.JTTX.Price bll = new SOSOshop.BLL.JTTX.Price();
                
            }
            catch (Exception ex)
            {
                SOSOshop.BLL.Logs.Log.LogServiceAdd(ex.Message, 0, "", "价格变动:CommandListen", ex.ToString(), 2);
            }
            
        }
    }
}
