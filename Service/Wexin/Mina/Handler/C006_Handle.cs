using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils.Newtonsoft;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
   public class C006_Handle : IProtocolHandle
    {
        public C006_Handle()
        {
        }

        public RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            C006ContentAck ack6 = DeserializeJsonToObject<C006ContentAck>(packet.jsonContent);

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            //String test = "{\"order_id\":\"z09131201110020161019130940\",\"qr_code_url\":\"weixin://wxpay/bizpayurl?pr=5RW7kvB\",\"tickets\":[{\"head_no\":\"1\",\"lottery_type_id\":\"1\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-1-1\"},{\"head_no\":\"2\",\"lottery_type_id\":\"2\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-2-2\"},{\"head_no\":\"3\",\"lottery_type_id\":\"3\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-3-3\"},{\"head_no\":\"4\",\"lottery_type_id\":\"4\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-4-4\"},{\"head_no\":\"5\",\"lottery_type_id\":\"5\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-5-5\"},{\"head_no\":\"6\",\"lottery_type_id\":\"6\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-6-6\"},{\"head_no\":\"7\",\"lottery_type_id\":\"7\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-7-7\"},{\"head_no\":\"8\",\"lottery_type_id\":\"8\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-8-8\"}]}";
            //Object o = JsonConvert.DeserializeObject(test, typeof(C006ContentAck), jsetting);
            //string test2 = "{\"order_id\":\"z09131201110020161019130940\",\"qr_code_url\":\"weixin://wxpay/bizpayurl?pr=5RW7kvB\",\"tickets\":[{\"head_no\":\"1\",\"lottery_type_id\":\"1\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-1-1\"},{\"head_no\":\"2\",\"lottery_type_id\":\"2\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-2-2\"},{\"head_no\":\"3\",\"lottery_type_id\":\"3\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-3-3\"},{\"head_no\":\"4\",\"lottery_type_id\":\"4\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-4-4\"},{\"head_no\":\"5\",\"lottery_type_id\":\"5\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-5-5\"},{\"head_no\":\"6\",\"lottery_type_id\":\"6\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-6-6\"},{\"head_no\":\"7\",\"lottery_type_id\":\"7\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-7-7\"},{\"head_no\":\"8\",\"lottery_type_id\":\"8\",\"nums\":1,\"ticket_ids\":\"z09131201110020161019130940-8-8\"}]}";


            C006ContentAck ack = JsonConvert.DeserializeObject<C006ContentAck>(packet.jsonContent, jsetting);

            


            remoteData.Content = ack;

            return remoteData;
        }


        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }
    }
}
