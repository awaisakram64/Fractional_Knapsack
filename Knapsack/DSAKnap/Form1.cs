using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace DSAKnap
{
    public partial class Form1 : Form
    {
         double[] weight;
         double[] benefit;
         double[] ratio;
         string[] ItemName;
         double W;
         double[] ben, wei;
         int len;
         double[] weightPercnt ;
         double[] benifitPercnt;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            button2.Visible = true;
            dataGridView1.Refresh();
            Listbox1.Items.Clear();
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "Items Name";
            dataGridView1.Columns[1].Name = "Items Weights";
            dataGridView1.Columns[2].Name = "Items Benifits";
            dataGridView1.Columns[3].Name = "Items Ratio";
            dataGridView1.Columns[4].Name = "Selected Weight";
            dataGridView1.Columns[5].Name = "Selected Benifit";
            dataGridView1.Columns[6].Name = "select Weight %";
            dataGridView1.Columns[7].Name = "select Benifit %";
            getcall();
            dataGridView1.ClearSelection();
        }
        public void getcall()
        {
            try
            {
                string input3 = textBox5.Text;
                ItemName = input3.Split(',').ToArray(); 
                string input1 = (textBox1.Text);
                weight = input1.Split(',').Select(Double.Parse).ToArray();
                string input2 = (textBox2.Text);
                benefit = input2.Split(',').Select(Double.Parse).ToArray();
                W = Convert.ToDouble(textBox3.Text);
                len = weight.Length;
                ratio = new double[weight.Length];
                for (int i = 0; i < weight.Length; ++i)
                {
                    ratio[i] = benefit[i] / weight[i];
                }
                int k= 0;
                while (k <len)
                {
                    dataGridView1.Rows.Add();
                    k++;
                }
                int j= 0;
                int v =len;
                while (j < v)
                {
                    dataGridView1.Rows[j].Cells[0].Value = ItemName[j];
                    dataGridView1.Rows[j].Cells[1].Value = weight[j].ToString();
                    dataGridView1.Rows[j].Cells[2].Value = benefit[j].ToString();
                    dataGridView1.Rows[j].Cells[3].Value = ratio[j].ToString();
                    
                    j++;
                }
                fill();
            }
            catch (Exception)
            {
                MessageBox.Show("Please make sure input is correct");
            }
        }
        public  int getNext(/*double[] benefit, double[] ratio*/)
        {
            double highest = 0;
            int index = -1;
            for (int i = 0; i < benefit.Length; ++i)
            {
                if (ratio[i] > highest)
                {
                    highest = ratio[i];
                    index = i;
                }
            }
            return index;
        }
         void fill()
        {
            double www, bbb;
            //www save temperory weight of item which will add in array*(double[] wei)  & sum in last 
            //bbb save temperory benifit of item which will add in array*(double[] ben)  & sum in last
            wei = new double[weight.Length];
            //wei save value of weights
            ben = new double[weight.Length];
            //ben save value of benifits
            double cW = 0; //current weight
            double cB = 0; //current benefit 
            int index = 0;
            int [] itemarray=new int[len];   // this save which item is selected

          //  Console.WriteLine("\nItems considered: ");
            while (cW < W)
            {
                int item = getNext();        //next highest ratio
                if (item == -1)
                {
                    //No items left
                    break;
                }
                itemarray[index] = (item + 1);
                index++;
                if (cW + weight[item] <= W)
                {
                    www = weight[item];
                    wei[item] = www;
                    bbb = benefit[item];
                    ben[item] = bbb;
                    cW += www;
                    cB += bbb;
                    //mark as used for the getNext() (ratio) function
                    ratio[item] = 0;
                }
                else
                {
                    Console.Beep();
                    Console.Beep();
                    bbb = (ratio[item] * (W - cW));
                    ben[item] = bbb;
                    www = (W - cW);
                    wei[item] = www;
                    cB += bbb;
                    cW += www;
                    break;  //the knapsack is full
                }
            }
            weightPercnt = new double[weight.Length];
            benifitPercnt = new double[weight.Length];
            for (int i = 0; i < len; i++)
            {
                weightPercnt[i] = (wei[i] / cW) * 100;
                benifitPercnt[i] = (ben[i] / cB) * 100;
            }
            for (int i = 0; i < index; i++)
            {
                if (itemarray[i]!=0)
                {
                    string[] aaa = Array.ConvertAll(itemarray, element => element.ToString()).ToArray();
                    Listbox1.Items.Add(aaa[i]).ToString();
                }
            }
            label6.Text = cB.ToString();
            label7.Text = cW.ToString();
            for (int i = 0; i < len; i++)
            {
                dataGridView1.Rows[i].Cells[4].Value = wei[i].ToString();
                dataGridView1.Rows[i].Cells[5].Value = ben[i].ToString();
                dataGridView1.Rows[i].Cells[7].Value = benifitPercnt[i].ToString();
                dataGridView1.Rows[i].Cells[6].Value = weightPercnt[i].ToString();
            }
         }

         private void button2_Click(object sender, EventArgs e)
         {
             Form2 obj = new Form2(ItemName, weightPercnt, benifitPercnt);
             obj.ShowDialog();
         }  
         private void linkLabel1_MouseHover(object sender, EventArgs e)
         {
             Form3 ob = new Form3();
             ob.ShowDialog();
         }
         private void linkLabel2_MouseHover(object sender, EventArgs e)
         {
             MessageBox.Show("About me will be added soon");
         }

         private void Form1_Load(object sender, EventArgs e)
         {

         }         
    }
}
