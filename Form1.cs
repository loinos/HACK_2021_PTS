using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HACK_PTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string bitarraytoprint (BitArray bits)
        {
            string ans = "";
            foreach(bool item in bits)
            {
                if (item)
                {
                    ans += "1";
                }
                else
                {
                    ans += "0";
                }
            }

            return ans;
        }
        public BitArray addbit(BitArray bits)
        {
            BitArray bit_array = new BitArray(21);
            int j = 0;
            for (int i = 0; i < 21; i++)
            {
                if(i == 0 | i == 1 | i == 3 | i == 7 | i == 15)
                {
                    bit_array[i] = false;
                }
                else
                {
                    bit_array[i] = bits[j];
                    j++;
                }
            }
            int controlsum = 0;
            for(int i = 0; i < 21; i +=2)
            {
                if (bit_array[i])
                {
                    controlsum++;
                }
            }
            if( controlsum % 2 != 0)
            {
                bit_array[0] = true;
            }
            controlsum = 0;
            for(int i = 1; i < 21; i += 3)
            {
                for(int k = 0; k < 2; k++)
                {
                    if (bit_array[i+k])
                    {
                        controlsum++;
                    }
                }
            }
            if (controlsum % 2 != 0)
            {
                bit_array[1] = true;
            }
            controlsum = 0;
            for (int i = 3; i < 21; i += 5)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (k+i<21 && bit_array[i + k])
                    {
                        controlsum++;
                    }
                }
            }
            if (controlsum % 2 != 0)
            {
                bit_array[3] = true;
            }
            controlsum = 0;
            for (int i = 15; i < 21; i ++)
            {
                if (bit_array[i])
                {
                    controlsum++;
                }
            }
            if (controlsum % 2 != 0)
            {
                bit_array[15] = true;
            }
            for (int i = 7; i < 16; i++)
            {
                if (bit_array[i])
                {
                    controlsum++;
                }
            }
            if (controlsum % 2 != 0)
            {
                bit_array[7] = true;
            }
            return bit_array;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = richTextBox1.Text;
            byte[] str_bin = System.Text.Encoding.UTF8.GetBytes(str1);
            BitArray bits = new BitArray(str_bin);

            //код хемминга
           


            richTextBox2.Text = bitarraytoprint(addbit(bits));

        }
    }
}
