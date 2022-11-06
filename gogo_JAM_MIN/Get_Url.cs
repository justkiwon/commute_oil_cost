using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.IO;
using System.Security.Policy;
using System.Windows;

namespace gogo_JAM_MIN
{
    public class Get_Url
    {
        public struct data
        {
            string PRODNM;
            string PRICE;
            string diff;
        };
        int Success = 0;
        public string[,] Request(string url)
        {
            string[,] Data = new string[4,4];

            // HttpWebRequest 이용한 응답 수신 방법
            var request = (HttpWebRequest)WebRequest.Create(url);   // 요청 URL을 기준으로 HTTP요청 인스턴스 생성
            request.Method = "GET"; // 방법은 GET(요청)으로 설정

            string results = string.Empty;
            HttpWebResponse response;   // HTTP응답 인스턴스 생성
            using (response = request.GetResponse() as HttpWebResponse) // 응답 요청
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());   // stream방식으로 결과 수시을 위한 인스턴스 생성
                results = reader.ReadToEnd();   // 수신된 결과 읽어오기
                Success = 1;
            }
            // 수신된 XML형식의 데이터를 컨트롤하기위해 XmlDocument 인스턴스를 생성
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(results);   // Stream결과를 XML형식으로 읽어오기
            XmlNodeList xmResponse = xml.GetElementsByTagName("RESULT");  // <response></response> 기준으로 node 생성
            XmlNodeList xmlList = xml.GetElementsByTagName("OIL"); // <item></item> 기준으로 node 생성

            int n = 0;
            int m = -1;
            foreach (XmlNode node in xmResponse)    // xml의 <response> 값 읽어 들이기
            {
                if (Success == 1) // 정상 응답일 경우
                {
                    m++;
                    foreach (XmlNode node1 in xmlList)  // <item> 값 읽어 들이기
                    {
                        if (node1["PRODNM"].InnerText != null) { node1["PRODNM"].InnerText = Data[m, n]; }

                        if (node1["PRICE"].InnerText != null) { node1["PRICE"].InnerText = Data[m, n]; }

                        Data[m,n] = node1.InnerText;
                        n++;
                    } n = 0;
                }
                else { MessageBox.Show("error"); }
                m = 0;
                Console.WriteLine(Data[m, n]);

            }
                return Data;
        }
    }
}
