using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSAKnap
{
    public partial class Form2 : Form
    {
        string[] ItemName=new string[20];
        double[] ben=new double[20];
        double[] wei = new double[20];
        public Form2(string [] items ,double [] weight,double [] benifit)
        {
           
            InitializeComponent();
            try
            {
                chart1.Series["Benifit"].Points.Clear();
                chart1.Series["Weight"].Points.Clear();
                for (int i = 0; i < items.Length; i++)
                {
                    this.chart1.Series["Benifit"].Points.AddXY(items[i], benifit[i]);

                    this.chart2.Series["Weight"].Points.AddXY(items[i], weight[i]);
                }
            }
            catch (Exception)
            {
               MessageBox.Show("Enter data to view Charts values");
                
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
