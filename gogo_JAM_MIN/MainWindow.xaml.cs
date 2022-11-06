using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Xml;
//using Url_Key; // 내가 만든 dll name space

namespace gogo_JAM_MIN
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Get_Url get_Url = new Get_Url();
      //  Url_key key = new Url_key();    //key 값만 따로 dll로 만 든 것
        public MainWindow()
        {
            InitializeComponent();
            string[,] a = get_Url.Request("http://www.opinet.co.kr/api/avgAllPrice.do?out=xml&code=F220919345");
            Data_Parsing( a );
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Recipie recipie = new Recipie();
            if(recipie.ShowDialog() == true)
            {
                Car_Name.Text = recipie.Car_Name.Text;
                Destination.Text = recipie.Destination.Text;
                mileage.Text = recipie.mileage.Text;
                oil.Text = recipie.oil.Text;
            }
        }
        public string Make_url()
        {                           //url 기본 형식 --> http://www.opinet.co.kr/api/avgAllPrice.do?out=xml&code= + Key값

            string url2 = "http://www.opinet.co.kr/api/avgAllPrice.do"+"?out=xml&code=" + "F220919345"; // do 뒤는 dll로 키값 hidden 시킴, 따라서 그냥 본인의 키값 넣으면 됨
          //  string url = Url_key.url;
          //  url2 += url;
            return url2;
        }
        public bool Data_Parsing(string[,] a)
        {
            foreach(string s in a)
            {
                if (s == null) { continue; }
                string b;
           //     Console.WriteLine(s);
          //      b = s.IndexOf();
             /*   if (b == "자동차")
                {
                    GaSolin.Text = b;
                }*/
            }
            
         //   Console.WriteLine("\n"+a[1]);
            return true;
        }
    }
}
