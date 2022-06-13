using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Forms.model;

namespace YTDSSTGenII.Service.Context
{
    public class UserContext
    {
        public string OrderIdTempWechat { get; set; }
        public string OrderIdTempAlipay { get; set; }

        private int balance;
        private UserMotor[] user_motor_array = new UserMotor [ 8 ];

        private UserContext ( ) {}
        private static UserContext instance;        
        public static UserContext getInstance ( ) {
            if ( null == instance )
            {
                instance = new UserContext ( );
            }
            return instance;
        }

        public Order UserOrder { get; set; }

        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public UserMotor [ ] UserMotorArray
        {
            get { return user_motor_array; }
        }

        public int getBuyMoney ( ) {
            int money = 0;
            foreach ( UserMotor item in UserMotorArray )
            {
                money += item.BuyLotteryNum * item.Lottery.UnitPrice;
            }

            return money;
        }

        /// <summary>
        /// 清空购买信息——出完票或是清空购物车时调用
        /// </summary>
        public void clearBuyInfo() {
            foreach (UserMotor item in UserMotorArray)
            {
                item.BuyLotteryNum = 0;
            }
        }

        public int getBuyLotteryNum()
        {
            int num = 0;
            foreach (UserMotor item in UserMotorArray)
            {
                num += item.BuyLotteryNum;
            }

            return num;
        }
    }
}
