using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YXShop.Model.Order
{
    /// <summary>
    /// Business entity used to model items in a shopping cart
    /// </summary>
    [Serializable]
    public class CartItemInfo
    {

        // Internal member variables
        private string cartkey;
        private string productid;
        private int quantity = 1;
        private string specification;
        private string fittingsProductId;
        private string fittingsProductCount;
        private string sparepartId;
        private string uniqueid;
        private int uid = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CartItemInfo() { }
        public CartItemInfo(string Cartkey, string ProductId, int count, string uniqueid, string Specification, string FittingsProductId, string FittingsProductCount, string SparepartId, int uid)
        {
            this.cartkey = Cartkey;
            this.productid = ProductId;
            this.quantity = count;
            this.specification = Specification;
            this.fittingsProductId = FittingsProductId;
            this.fittingsProductCount = FittingsProductCount;
            this.sparepartId = SparepartId;
            this.uniqueid = uniqueid;
            this.uid = uid;
        }
        public string CartKey
        {
            get { return cartkey; }
            set { cartkey = value; }
        }
        // Properties
        public string ProductId
        {
            get { return productid; }
            set { productid = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }


        public string FittingsProductId
        {
            get { return fittingsProductId; }
        }

        public string FittingsProductCount
        {
            get { return fittingsProductCount; }
        }
        public string Specification
        {
            get { return specification; }
        }
        public string SparepartId
        {
            get { return sparepartId; }
        }
        public int  Uid
        {
            get { return uid; }
        }
    }
}
