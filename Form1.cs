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
        public BitArray hammingDecoder(BitArray bits)
        {
            BitArray bit_array = new BitArray(21);
            bit_array = bits;

            BitArray bans = new BitArray(16);
            int j = 0;
            for (int i = 0; i < 21; i++)
            {
                if (i != 0 && i != 1 && i != 3 && i != 7 && i != 15)
                {
                    bans[j] = bit_array[i];
                    j++;
                }
            }
            bit_array = addbit(bans);

            int index_of_error = -1;
            if(bits[0] != bit_array[0])
            {
                index_of_error+=2;
            }
            for (int i = 2;i< 21; i=i*2)
            {
                if (bits[i - 1] != bit_array[i - 1])
                {
                    index_of_error += i;
                }
            }
            if (index_of_error != -1)
            {
                bit_array[index_of_error - 1] = !bit_array[index_of_error - 1];
            }
            BitArray ans = new BitArray(16);
            int k = 0;
            for(int i = 0; i < 21; i++)
            {
                if (i != 0 && i != 1 && i != 3 && i != 7 && i != 15)
                {
                    ans[k] = bit_array[i];
                    k++;
                }
            }

            return ans;
        }
        static List<string> SplitString(string str)
        {
            List<string> list = new List<string>();
            int i = 0;
            while (i < str.Length - 1)
            {
                list.Add(str.Substring(i, 2));
                i += 2;
            }
            return list;
        }
        public BitArray mainHammingCoder(string str)
        {
            if(str.Length % 2 !=0)
            {
                str += "\0";
            }
            List<string> list_string = SplitString(str);
            BitArray ans = new BitArray(list_string.Count*21);
            int i = 0;
            foreach(string str1 in list_string)
            {
                byte[] str_bin = System.Text.Encoding.UTF8.GetBytes(str1);
                BitArray bits = new BitArray(str_bin);
                bits = addbit(bits);
                for(int j = 0; j < bits.Length; j++)
                {
                    ans[i] = bits[j];
                    i++;
                }
            }
            return ans;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string str1 = richTextBox1.Text;
            //byte[] str_bin = System.Text.Encoding.UTF8.GetBytes(str1);
            //BitArray bits = new BitArray(str_bin);
            //richTextBox2.Text = bitarraytoprint(addbit(bits));

            richTextBox2.Text = bitarraytoprint(mainHammingCoder(richTextBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str1 = richTextBox2.Text;
            //byte[] str_bin = System.Text.Encoding.UTF8.GetBytes(str1);
            BitArray bits = new BitArray(str1.Length);
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == '1')
                {
                    bits[i] = true;
                }
                else
                {
                    bits[i] = false;
                }
            }
            

            //код хемминга



            richTextBox1.Text = bitarraytoprint(hammingDecoder(bits));

        }
    }
}
