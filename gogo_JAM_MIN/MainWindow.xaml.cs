using System;
using System.Windows;
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
        public string Selected_Destination { get; set; }
        public string Selected_mileage { get; set; } //그냥 get { return Selected_mileage; } set { Selected_mileage = value; }과 동일 함
        public string Selected_oil { get; set; }
        public string Final_SUM { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)  // My go-arrived info
        {
            Recipie recipie = new Recipie();
            if(recipie.ShowDialog() == true)
            {
                Car_Name.Text = recipie.Car_Name.Text;
                mileage.Text = recipie.mileage.Text;
                Selected_mileage = mileage.Text;
                Destination.Text = recipie.Destination.Text;
                Selected_Destination = Destination.Text;
                oil.Text = recipie.oil.Text;
                switch (oil.Text)
                {
                    case "경유":
                        Selected_oil = Diesel.Text;
                        break;
                    case "휘발유":
                        Selected_oil = GaSolin.Text;
                        break;
                    case "LPG":
                        Selected_oil = LPG.Text;
                        break;
                }
            }
            Calculate();
        }

        public void Calculate()
        {
            Double sum;
            Double oil = Convert.ToDouble(Selected_oil);
            Double Destination = Convert.ToDouble(Selected_Destination );
            Double mileage = Convert.ToDouble(Selected_mileage);
            sum = (oil / mileage) * Destination;
            Final_SUM = Convert.ToString(sum);
            SUM.Text = Final_SUM;
        }

        public string Make_url()   //Url Http parsing key value and connecting
        {                           //url 기본 형식 --> http://www.opinet.co.kr/api/avgAllPrice.do?out=xml&code= + Key값

            string url2 = "http://www.opinet.co.kr/api/avgAllPrice.do"+"?out=xml&code=" + "F220919345"; // do 뒤는 dll로 키값 hidden 시킴, 따라서 그냥 본인의 키값 넣으면 됨
          //  string url = Url_key.url;
          //  url2 += url;
            return url2;
        }
        public string[,] Data_Parsing(string[,] a)   // oil set, price info
        {

            if (a[0, 0] != "ERROR")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)  // 0-> 유종, 1-> 가격 2-> 전일가비교 3->전체
                    {
                        if (a[i, j] == "휘발유") { GaSolin.Text = a[i, j + 1]; }
                        if (a[i, j] == "자동차용경유") { Diesel.Text = a[i, j + 1]; }
                        if (a[i, j] == "자동차용부탄") { LPG.Text = a[i, j + 1]; } //else{ MessageBox.Show("NOOOOOOOO"); }

                        Console.WriteLine(a[i, j]);
                    }
                }
                //   Console.WriteLine("\n"+a[1]);
            }
            else
            {
                MessageBox.Show("Please check Network!!!");
            }
                return a;
        }
    }
}
