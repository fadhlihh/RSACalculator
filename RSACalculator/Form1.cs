using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
namespace RSACalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox4.Text = "150";
            textBox2.Text = "176";
        }

        private bool isRelatifPrima(int n, int m)
        {
            int q, r = 1;
            while (r != 0)
            {
                q = m / n;
                r = m % n;
                m = n;
                n = r;
            }
            return (m == 1);
        }

        private int inverse(int n, int m)
        {
            int t0 = 0, t1 = 1, invers = 1, q, r = 0, b = m;
            while (r != 1)
            {
                q = m / n;
                r = m % n;
                invers = t0 - q * t1;
                if (invers < 0)
                {
                    invers = b - (Math.Abs(invers) % b);
                }
                else
                {
                    invers %= b;
                }
                t0 = t1;
                t1 = invers;
                m = n;
                n = r;
            }
            return invers;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = ((numericUpDown1.Value - 1) * (numericUpDown2.Value - 1)).ToString();
            textBox2.Text = (numericUpDown1.Value*numericUpDown2.Value).ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = ((numericUpDown1.Value - 1) * (numericUpDown2.Value - 1)).ToString();
            textBox2.Text = (numericUpDown1.Value * numericUpDown2.Value).ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (isRelatifPrima(int.Parse(numericUpDown4.Value.ToString()),int.Parse(textBox4.Text)))
            {
                label12.Text = "";
                textBox5.Text = inverse(int.Parse(numericUpDown4.Value.ToString()), int.Parse(textBox4.Text)).ToString();
                button1.Enabled = true;
            }
            else
            {
                label12.Text = "Harus relatif prima terhadap m";
                button1.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = (int.Parse(textBox5.Text) % int.Parse(textBox4.Text)).ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (isRelatifPrima(int.Parse(numericUpDown4.Value.ToString()), int.Parse(textBox4.Text)))
            {
                label12.Text = "";
                textBox5.Text = inverse(int.Parse(numericUpDown4.Value.ToString()), int.Parse(textBox4.Text)).ToString();
                button1.Enabled = true;
            }
            else
            {
                label12.Text = "Harus relatif prima terhadap m";
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] chars = textBox1.Text.ToCharArray();
            BigInteger number = 0;
            string text = "";
            for(int i=0; i< chars.Length; i++)
            {
                number = chars[i];
                number = number - 65;
                BigInteger exponent = BigInteger.Parse(numericUpDown4.Value.ToString());
                BigInteger divisor = BigInteger.Parse(textBox2.Text);
                number = BigInteger.ModPow(number, exponent, divisor);
                //int current_char = Convert.ToInt32(number);
                text += number.ToString();
                if (i != chars.Length - 1)
                    text += ",";
            }
            textBox3.Text = text;
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 100000000;
            String chars = textBox12.Text;
            String[] spearator = { "," };
            String[] strlist = chars.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);
            string text = "";
            BigInteger number = 0;
            //char[] chars = textBox12.Text.ToCharArray();
            foreach (String s in strlist)
            {
                number = System.Convert.ToInt32(s);
                BigInteger divisor = BigInteger.Parse(numericUpDown5.Value.ToString());
                BigInteger exponent = BigInteger.Parse(numericUpDown3.Value.ToString());
                number = BigInteger.ModPow(number, exponent, divisor);
                number = number + 65;
                int numInt = (int)number;
                char text_string = Convert.ToChar(numInt);
                text += text_string;
            }
            textBox11.Text = text;
            button6.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text File (*.txt)|*.txt|Word Document File (*.doc)|*.doc|Word Document File (*.docx)|*.docx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = dialog.FileName;
                StreamReader reader = new StreamReader(textBox7.Text);
                textBox1.Text = reader.ReadToEnd();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text File (*.txt)|*.txt|Word Document File (*.doc)|*.doc|Word Document File (*.docx)|*.docx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Open(dialog.FileName, FileMode.CreateNew))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(textBox3.Text);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text File (*.txt)|*.txt|Word Document File (*.doc)|*.doc|Word Document File (*.docx)|*.docx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = dialog.FileName;
                StreamReader reader = new StreamReader(textBox8.Text);
                textBox12.Text = reader.ReadToEnd();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text File (*.txt)|*.txt|Word Document File (*.doc)|*.doc|Word Document File (*.docx)|*.docx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Open(dialog.FileName, FileMode.CreateNew))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(textBox11.Text);
                }
            }
        }
    }
}
