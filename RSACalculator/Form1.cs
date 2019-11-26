﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSACalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox4.Text = "1";
            textBox2.Text = "1";
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
            int number = 0;
            string text = "";
            double temp;
            for(int i=0; i< chars.Length; i++)
            {
                temp = 1;
                number = (chars[i] % 65);
                for(int j=1; j<=numericUpDown4.Value; j++)
                {
                    temp *= number;
                }
                string current_char = (temp % int.Parse(textBox2.Text)).ToString();
                text += current_char + Environment.NewLine;
            }
            textBox3.Text = text;
        }
    }
}
