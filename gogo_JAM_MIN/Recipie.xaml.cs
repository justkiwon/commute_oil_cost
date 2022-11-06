using System;
using System.Collections.Generic;
using System.Linq;
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

namespace gogo_JAM_MIN
{
    /// <summary>
    /// Recipie.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Recipie : Window
    {
        public Recipie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Empty_string();
        }

        public bool Empty_string()                        
        {
            int temp = 0;
            if (Car_Name.Text == "" || Car_Name.Text == "Car Name")
            {
                MessageBox.Show("Please, Retry insert Car Name");
            }
            else if (Destination.Text == "" || Destination.Text == "Destination" || !(int.TryParse(Destination.Text, out temp)))
            {                                                                //int.TryParse(int로 형변환 할 값, 만약 형 변환 안될때 값을 뭘로 바꾸는가) --> return 값은 bool
                if (Destination.Text == "" || Destination.Text == "Destination")
                {
                    MessageBox.Show("Please, Retry insert Destination");
                }
                if (!(int.TryParse(Destination.Text, out temp))) { MessageBox.Show("Insert answer to property type"); }
            }
            else if (mileage.Text == "" || mileage.Text == "How to long" || !(int.TryParse(mileage.Text, out temp)))
            {
                if (mileage.Text == "" || mileage.Text == "How to long")
                {
                    MessageBox.Show("Please, Retry insert mileage");
                }
                if(!(int.TryParse(mileage.Text, out temp)))
                {
                    MessageBox.Show("Insert answer to property type");
                }
            }
            else if (oil.Text == null)
            {
                    MessageBox.Show("Please, Retry insert Oil Type");
            }
            else { this.DialogResult = true; } // DialogResult 가 true면 꺼짐
            return true;
        }   
    }
}
