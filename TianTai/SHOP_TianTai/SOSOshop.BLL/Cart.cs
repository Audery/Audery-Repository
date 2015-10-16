using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YXShop.Model.Order;
using System.Data;
using SOSOshop.BLL;
using System.Data.SqlClient;

namespace YXShop.BLL.Order
{
    [Serializable]
    public class Cart : Db
    {
        // 内部储存车	  
        private Dictionary<string, CartItemInfo> cartItems = new Dictionary<string, CartItemInfo>();


        /// <summary>
        /// Update the quantity for item that exists in the cart
        /// </summary>
        /// <param name="itemId">Item Id</param>
        /// <param name="qty">Quantity</param>
        public void SetQuantity(string itemId, int qty)
        {

            cartItems[itemId].Quantity = qty;
        }
        /// <summary>
        /// 根据会员ID，获取他的购物车信息。
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public double GetCardMoneyByMemberId(string MemberId)
        {
            DataTable table = base.ExecuteTableForCache("select isnull(sum(v.pro_ShopPrice * c.quantity),0) from yxs_product v inner join yxs_cart c on v.pro_ID=c.productid where c.isShoppingCart=1 and c.uid=" + MemberId, "shoppingcart_money");
            if (table != null && table.Rows.Count > 0)
                return Convert.ToDouble(table.Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 根据会员ID，获取他的购物车信息。
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public DataTable GetCardListByMemberId(string MemberId)
        {
            return base.ExecuteTableForCache("select * from  yxs_cart where isShoppingCart=1 and uid=" + MemberId, "shoppingcart_getlist");
        }


        /// <summary>
        /// 商品详情页面添加购物车。
        /// </summary>
        /// <param name="ProId"></param>
        /// <param name="productCount"></param>
        /// <param name="memberId"></param>
        public bool AddByContent(string ProId, decimal productCount, string memberId, bool ExpirationTime)
        {
            //将商品加入访问日志
            MemberBrowserProductContentLog bllLog = new SOSOshop.BLL.MemberBrowserProductContentLog();
            bllLog.RecordBrowse(memberId, ProId);
            if (productCount <= 0)
            {
                productCount = 1;
            }
            //是否近效期
            String ExpirationTimeStr = (ExpirationTime ? 1 : 0).ToString();
            //加入购物车
            string query = "select count(1) from yxs_cart where uid=" + memberId + " and productid=" + ProId + " and isShoppingCart=1 and IsExpirationProduct=" + ExpirationTimeStr;
            int cart = (int)base.ExecuteScalar(query);
            if (cart > 0)
            {
                return UpdateByCount(ProId, productCount, memberId, ExpirationTimeStr);
            }
            else
            {
                return AddCart(ProId, productCount, memberId, ExpirationTimeStr);
            }

        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="CartKey"></param>
        /// <param name="ProId"></param>
        /// <param name="productCount"></param>
        /// <param name="memberId"></param>
        public bool AddCart(string ProId, decimal productCount, string memberId, String ExpirationTimeStr)
        {

            StringBuilder strSql = new StringBuilder();
            string CartKey = ChangeHope.Common.DEncryptHelper.GetRandomNumber();
            strSql.Append("insert yxs_cart ");
            strSql.AppendFormat("values({0},{1},{2},{3},'',0,'','',1,'{4}',{5},{6})", memberId, CartKey, ProId, productCount, DateTime.Now, memberId, ExpirationTimeStr);
            return (int)db.ExecuteNonQuery(CommandType.Text, strSql.ToString()) == 1;

        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public bool DelShoppingCartProduct(int PID, int UID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete yxs_cart");
            sql.AppendFormat(" where [uid]={0} and productid in ({1})", UID, PID);
            return base.ExecuteNonQuery(sql.ToString()) > 0;
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="pro_id"></param>
        /// <param name="buycount"></param>
        /// <param name="memberid"></param>
        public bool UpdateByCount(string pro_id, decimal buycount, string memberid, string ExpirationTimeStr)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.AppendFormat("UPDATE yxs_cart SET quantity=quantity+{0} where uid={1} and productid={2}", buycount, memberid, pro_id);
            strSql.AppendFormat("UPDATE yxs_cart SET quantity={0} where uid={1} and productid={2} AND IsExpirationProduct={3}", buycount, memberid, pro_id, ExpirationTimeStr);
            return (int)db.ExecuteNonQuery(CommandType.Text, strSql.ToString()) == 1;
        }

        /// <summary>
        /// Add an item to the cart.
        /// When ItemId to be added has already existed, this method will update the quantity instead.
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(CartItemInfo item)
        {
            CartItemInfo cartItem;
            if (!cartItems.TryGetValue(item.CartKey.ToString(), out cartItem))
                cartItems.Add(item.CartKey.ToString(), item);
            else
                cartItem.Quantity += item.Quantity;
        }

        /// <summary>
        /// 从购物车中删除
        /// </summary>
        /// <param name="itemId">删除ID</param>
        public void Remove(string itemId)
        {
            cartItems.Remove(itemId);
        }

        /// <summary>
        /// Returns all items in the cart. Useful for looping through the cart.
        /// </summary>
        /// <returns>Collection of CartItemInfo</returns>
        public ICollection<CartItemInfo> CartItems
        {
            get { return cartItems.Values; }
        }


        /// <summary>
        /// Clear the cart
        /// </summary>
        public void Clear()
        {
            cartItems.Clear();
        }
        /// <summary>
        /// 订单保存完成后，删除购物车表中数据
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ProIdList"></param>
        public void DeleteCartByMemberId(string uid, string ProIdList)
        {
            string query = "delete from yxs_cart where uid=" + uid + " and productid in (" + ProIdList + ")";
            base.ExecuteNonQuery(query);
        }


    }
}

