using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using System.Net.Http;
using Newtonsoft.Json;

namespace YTDSSTGenII.Utils.Network.Http
{
    class HttpClientProxy
    {

        public static HttpClientProxy newProxy() {
            return new HttpClientProxy();
        }

        public ResponseData sendRequest<T>(String url)
        {
            HttpClient client = new HttpClient();
            
            ResponseData responseData = new ResponseData();
            HttpResponseMessage response = client.GetAsync(url).Result;

            Console.WriteLine(response.StatusCode);

            if(response.StatusCode == HttpStatusCode.OK) {
                try
                {
                    String content = response.Content.ReadAsStringAsync().Result;
                    T data = JsonConvert.DeserializeObject<T>(content);
                    responseData.Data = data;
                    responseData.HttpStatusCode = response.StatusCode;
                    responseData.Status = ResponseStatus.SUCCESS;
                    responseData.Message = "success";
                }
                catch (Exception exp) {
                    responseData.Status = ResponseStatus.FAULT;
                    responseData.Message = "内容解析失败(" + exp.Message + ")";       
                }
            }
            else
            {
                    responseData.Status = ResponseStatus.FAULT;
                    responseData.Message = "failed";    
            }

            client.Dispose();

            return responseData;
        }
    }
}
