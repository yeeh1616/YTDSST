using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YTDSSTGenII.Service;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Service.Sg
{
    public class ResultMessage
    {
        public static Dictionary<int, string> dic_sgBind;
        public static Dictionary<int, string> dic_sgInit;
        public static Dictionary<int, string> dic_sgSetclaimAmount;
        public static Dictionary<int, string> dic_sgValidation_a;
        public static Dictionary<int, string> dic_sgValidation_b;

        static ResultMessage()
        {
            dic_sgInit = new Dictionary<int, string>();
            dic_sgBind = new Dictionary<int, string>();
            dic_sgValidation_b = new Dictionary<int, string>();
            dic_sgValidation_a = new Dictionary<int, string>();
            dic_sgSetclaimAmount = new Dictionary<int, string>();
        }

        public ResultMessage()
        {}

        public static string getMean(int key, Dictionary<int, string> dicMeans)
        {
            fillDic();
            string str = "兑奖失败，请去网点兑奖或稍后再兑。";
            foreach (KeyValuePair<int, string> pair in dicMeans)
            {
                if (key == pair.Key)
                {
                    str = pair.Value;
                }
            }
            return str;
        }

        public static void fillDic()
        {
            dic_sgInit.Clear();
            dic_sgValidation_a.Clear();
            dic_sgValidation_a.Add(0, "兑奖成功。");
            dic_sgValidation_a.Add(1, "该票已兑奖");
            dic_sgValidation_a.Add(0x12d, "票号不符合规则，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x12e, "票号与兑奖码不匹配，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x12f, "您未中奖，请继续努力！");
            dic_sgValidation_a.Add(0x130, "彩票为在途状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x131, "彩票为打包状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x132, "彩票为阻止状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x133, "彩票为瑕疵状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x134, "彩票为丢失状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x135, "彩票为下架状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(310, "彩票为待销毁状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x137, "彩票为销毁状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x138, "彩票状态异常，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x139, "彩票为库存状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x13a, "系统中未找到彩票包数据，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x13b, "彩票为活动库存状态，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x13c, "补号位验证不通过,请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x13d, "该票已兑奖，如有疑问请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x13e, "非本省销售彩票，不支持跨省兑奖！");
            dic_sgValidation_a.Add(0x13f, "奖金超出最高限额,请去体彩中心兑奖");
            dic_sgValidation_a.Add(320, "奖金超出限额,请去网点或体彩中心兑奖");
            dic_sgValidation_a.Add(0x141, "奖金超出限额,请去网点或体彩中心兑奖");
            dic_sgValidation_a.Add(0x142, "系统不允许兑奖，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x143, "游戏状态未开启，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x144, "未查询到数据，请电话联系" + GlobalParmeters.sgtelephone);
            dic_sgValidation_a.Add(0x145, "您未中奖，请继续努力");
            //dic_sgValidation_a.Add(0x145, "兑奖失败，请稍后再兑！");
            dic_sgValidation_a.Add(0x65, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x66, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x67, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x68, "网络连接失败，暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x69, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x6a, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x6b, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x6c, "暂不支持兑奖，购彩请投币");
            dic_sgValidation_a.Add(0x6d, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x65, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x66, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x67, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x68, "网络连接失败，暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x69, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x6a, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x6b, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x6c, "暂不支持兑奖，购彩请投币");
            dic_sgInit.Add(0x6d, "暂不支持兑奖，购彩请投币");
        }

 

    }
}
