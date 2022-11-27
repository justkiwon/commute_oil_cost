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

        int Success = 0;
        public string[,] Request(string url)
        {
            string[,] Data = new string[6,6];
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
                        if (node1["PRODNM"].InnerText == null) { continue; } // 유종 Data[m,0]
                            Data[m, n++] = node1["PRODNM"].InnerText;

                        if (node1["PRICE"].InnerText == null) { continue; }  // 가격 Data[m,1]
                            Data[m, n++] = node1["PRICE"].InnerText;

                        if (node1["DIFF"].InnerText == null) { continue; }  // 전일과 가격 차이 비율 Data[m,1]
                            Data[m, n++] = node1["DIFF"].InnerText;

                        Data[m,n] = node1.InnerText;  // 20221106K015자동차용부탄1032.89-0.33
                        n = 0;
                        m++;
                    } n = 0;
                    for(int i= 0; i< 6; i++){
                        for (int j = 0; j < 6; j++)
                        {
                            Console.WriteLine("Data[{0}][{1}] = {2}", i, j, Data[i, j]);
                        }
                    }
                }
                else { MessageBox.Show("error"); 
                        Data[0,0] = "ERROR";
                    return Data;
                }
                m = 0;
                Console.WriteLine(Data[m, n]);

            }
                return Data;
        }
    }
}
