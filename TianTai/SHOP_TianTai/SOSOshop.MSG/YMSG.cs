using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOSOshop.MSG
{
    /// <summary>
    /// 亿美软通短信接口
    /// </summary>
    public class YMSG : IMSG
    {
        const string REG_KEY = " ";//注册的key值(自定义)[可在此处修改]
        const string SERIAL_NUM = "0SDK-EAA-0130-OEWPT";//序列号
        const string PASS_NUM = " ";//密码

        public YMSG()
        { }

        /// <summary>
        /// 亿美短信发送处理
        /// </summary>
        /// <param name="phoneNum">电话号码</param>
        /// <param name="sendContent">发送内容</param>
        public bool Send(string phoneNum, string sendContent)
        {
            SOSOshop.MSG.ServiceReference1.SDKClientClient service = new SOSOshop.MSG.ServiceReference1.SDKClientClient();
            Rigister(service);//注册
            return SendMsg(service, phoneNum, sendContent);//发送短信
            LogOut(service);//注销
        }

        /// <summary>
        /// 注册
        /// </summary>
        private bool Rigister(SOSOshop.MSG.ServiceReference1.SDKClientClient service)
        {
            /*
             * <params>
             * 参数1：软件序列号
             * 参数2：用户key值(自定义）
             * 参数3：软件密码
             * </params>
            */
            int returnReg = service.registEx(SERIAL_NUM, REG_KEY, PASS_NUM);

            if (returnReg != 0)
            {
                switch (returnReg)
                {
                    case 10: return false; //MessageBox.Show("客户端注册失败");
                        break;
                    case 303: return false;//MessageBox.Show("客户端网络故障");
                        break;
                    case 305: return false;// MessageBox.Show("服务器端返回错误，错误的返回值");
                        break;
                    case 999: return false;//MessageBox.Show("操作频繁");
                        break;
                    default: return false;//MessageBox.Show("注册出现未知异常");
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="service"></param>
        /// <param name="phoneNum">电话号码</param>
        /// <param name="sendContent">发送内容</param>
        private bool SendMsg(SOSOshop.MSG.ServiceReference1.SDKClientClient service, string phoneNum, string sendContent)
        {
            string[] phoneNumArray = phoneNum.Split(',');

            GetIfEnough(service, phoneNumArray.Length);//判断是否有足够的余额

            /*
             * <params>
             * 参数1：软件序列号
             * 参数2：用户key值(自定义）
             * 参数3：发送时间（如果不为空则是定时发送，当时间到达输入的时间时发送信息，如果为空则是即时发送）[目前默认为即时发送]
             * 参数4：手机号码
             * 参数5：发送内容
             * 参数6：短信扩展号码，默认为空
             * 参数7：字符编码
             * 参数8：短信等级，数值为1~5，值越大，优先等级越高
             * 参数9：短信ID（为不重复，目前为将当前时间转换为long类型的）
             * </params>
            */
            int returnValue = service.sendSMS(SERIAL_NUM,
                REG_KEY,
                "",
                phoneNumArray,
                sendContent,
                "",
                "GBK",
                3,
                Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff"))
                );

            switch (returnValue)
            {
                case 0: return true;// MessageBox.Show("短信发送成功");
                    break;
                case 17: return true;// MessageBox.Show("发送信息失败");
                    break;
                case 18: return true;//MessageBox.Show("发送定时信息失败");
                    break;
                case 303: return true;// MessageBox.Show("客户端网络故障");
                    break;
                case 305: return true;// MessageBox.Show("服务器端返回错误，错误的返回值");
                    break;
                case 307: return true;//MessageBox.Show("目标电话号码不符合规则，电话号码必须是以0、1开头");
                    break;
                case 997: return true;// MessageBox.Show("平台返回找不到超时的短信，该信息是否成功无法确定");
                    break;
                case 998: return true;//MessageBox.Show("由于客户端网络问题导致信息发送超时，该信息是否成功下发无法确定");
                    break;
                default: return true;// MessageBox.Show("发送出现未知异常");
                    break;
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private void LogOut(SOSOshop.MSG.ServiceReference1.SDKClientClient service)
        {
            /*
             * <params>
             * 参数1：软件序列号
             * 参数2：用户key值(自定义）
             * </params>
            */
            int returnLogOut = service.logout(SERIAL_NUM, REG_KEY);

            if (returnLogOut != 0)
            {
                switch (returnLogOut)
                {
                    case 22: //MessageBox.Show("注销失败");
                        break;
                    case 303:// MessageBox.Show("客户端网络故障");
                        break;
                    case 305: //MessageBox.Show("服务器端返回错误，错误的返回值");
                        break;
                    case 999: //MessageBox.Show("操作频繁");
                        break;
                    default: //MessageBox.Show("注销出现未知异常");
                        break;
                }
            }
        }

        /// <summary>
        /// 判断余额是否足够
        /// </summary>
        /// <param name="service"></param>
        /// <param name="msgCount">短信条数</param>
        /// <returns></returns>
        private void GetIfEnough(SOSOshop.MSG.ServiceReference1.SDKClientClient service, int msgCount)
        {
            /*
             * <params>
             * 参数1：软件序列号
             * 参数2：用户key值(自定义）
             * </params>
            */
            double balance = service.getBalance(SERIAL_NUM, REG_KEY);//余额
            double eachFee = service.getEachFee(SERIAL_NUM, REG_KEY);//发送每条短信的费用

            if (balance - eachFee * msgCount < 0)
            {
                // MessageBox.Show("余额不足，请联系相关人员");

                return;
            }
        }
    }
}
