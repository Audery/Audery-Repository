using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSWebService.BLL.Data_Centre;

namespace TestProject
{
    [TestClass]
    public class Product_Centre_Test
    {
        [TestMethod]
        public void InitializeData()
        {
            Product_Centre pc = new Product_Centre();
           // pc.initStock();
            pc.InitializeData(50003);
            pc.InitializeDataNotfiling(50003);
        }
    }
}
