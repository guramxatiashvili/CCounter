using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;

namespace Ccounter_gurami_oop
{
    
    public partial class Ccounter : Form
    {
        DataClasses1DataContext myDB = new DataClasses1DataContext();
        public void Cwoman(double weight,double height,int age) 
        {
            double bmr=1655 + 9.6 * weight + 1.8 * height - 4.5 * age;
            label7.Text = "თქვენი დღიური ნორმა არის "+Convert.ToString(bmr)+" კალორია";
        }
        public void  cman(double weight, double height, int age) 
        {
            double bmr = 1066 + 13.7 * weight + 5 * height - 6.8 * age;
            label7.Text = "თქვენი დღიური ნორმა არის " + Convert.ToString(bmr) + " კალორია";
        }
        public Ccounter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* label1.Visible = false; label2.Visible = false; label3.Visible = false; label4.Visible = false; label5.Visible = false;
            textBox1.Visible = false; textBox2.Visible = false; textBox3.Visible = false; textBox4.Visible = false; textBox5.Visible = false;
            radioButton1.Visible = false; radioButton2.Visible = false;
            button1.Visible = false; */
            label6.Text = "გამარჯობა,"+textBox1.Text;
            int asaki= Convert.ToInt32(textBox3.Text);
            double wona = Convert.ToDouble(textBox5.Text);
            double simagle = Convert.ToDouble(textBox4.Text);
            if (radioButton1.Checked)
            {
                cman(wona, simagle, asaki);

            }
            if (radioButton2.Checked)
            {
                Cwoman(wona, simagle, asaki);
            }
            double bmi = wona / ((simagle / 100) * (simagle / 100));
            label8.Text = "თქვენი BMI არის  "+Convert.ToString(bmi);
            if (bmi < 19) label10.Text = "- გამხდარი";
            else if (bmi > 19 && bmi < 25) label10.Text = "- ნორმალური";
            else if (bmi > 25 && bmi < 31) label10.Text = "- ოდნავ ჭარბწონიანი";
            else label10.Text = "- ჭარბწონიანი";

            double dasaklebi = wona - 22 * ((simagle/100) * (simagle/100));
            double sasurveliwona = wona - dasaklebi;
            label9.Text= "თქვენთვის საუკეთესო წონა არის  " + Convert.ToString(sasurveliwona);
        }

        private void Ccounter_Load(object sender, EventArgs e)
        {
            //make combobox searchable
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            updatelComboBox();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EatenFoodsTextBox.Text += comboBox1.SelectedItem +","+textBox7.Text +","+ Environment.NewLine;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            EatenFoodsTextBox.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int weight = Convert.ToInt32(textBox5.Text);
            int height = Convert.ToInt32(textBox4.Text);
            int age = Convert.ToInt32(textBox3.Text);
            double eaten, norma;
            if (radioButton1.Checked)
            {
                norma = 1066 + 13.7 * weight + 5 * height - 6.8 * age;
            }
            else
            {
                norma = 1655 + 9.6 * weight + 1.8 * height - 4.5 * age;
            }

            string[] foods = EatenFoodsTextBox.Text.Split(',');
            Dictionary<string, int> catalogi = new Dictionary<string, int>();
            var res = from f in myDB.Tables
                      select f;
            foreach (var item in res)
            {
                catalogi.Add(item.Name, item.Calories);
            }

            eaten = 0;
            for (int i = 1; i < foods.Length ; i +=2)
            {
                eaten += (Convert.ToInt32(foods[i]) * catalogi[foods[i-1]]) / 100;
            }
            double left = norma - eaten; 
            MessageBox.Show("თქვენი დარჩენილი კალორიების რაოდენობაა "+left.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = textBox6.Text;
            int cal = Convert.ToInt32(textBox8.Text);
            Table newFood = new Table();
            newFood.Calories = cal;
            newFood.Name = name;

            myDB.Tables.InsertOnSubmit(newFood);
            myDB.SubmitChanges();
            updatelComboBox();
        }

        void updatelComboBox()
        {
            comboBox1.Items.Clear();
            var res = from f in myDB.Tables
                      select f.Name;

            foreach (var item in res)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
