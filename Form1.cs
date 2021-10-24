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
        public BitArray generateXOR(BitArray input_bits)
        {
            BitArray toreturn_bits = new BitArray(input_bits.Length + input_bits.Length / 2);
            List<BitArray> list_bits = new List<BitArray>();
            BitArray b1 = new BitArray(input_bits.Length / 2);
            BitArray b2 = new BitArray(input_bits.Length / 2);
            BitArray b3 = new BitArray(input_bits.Length / 2);
            int j = 0;
            for (int i = 0; i < input_bits.Length / 2; i++)
            {
                b1[i] = input_bits[j];
                j++;
            }
            for (int i = 0; i < input_bits.Length / 2; i++)
            {
                b2[i] = input_bits[j];
                j++;
            }
            for (int i = 0; i < input_bits.Length / 2; i++)
            {
                b3[i] = b1[i] ^ b2[i];
            }
            j = 0;
            for (int i = 0; i < input_bits.Length / 2; i++)
            {
                toreturn_bits[j] = b1[i];
                j++;
            }
            for (int i = 0; i < input_bits.Length / 2; i++)
            {
                toreturn_bits[j] = b2[i];
                j++;
            }
            for (int i = 0; i < input_bits.Length / 2; i++)
            {
                toreturn_bits[j] = b3[i];
                j++;
            }
            return toreturn_bits;
        }
        public BitArray hammingBlockCoder(BitArray bits)
        {
            BitArray bit_array = new BitArray(21);
            int j = 0;
            for (int i = 0; i < 21; i++)
            {
                if (i == 0 | i == 1 | i == 3 | i == 7 | i == 15)
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
            for (int i = 0; i < 21; i += 2)
            {
                if (bit_array[i])
                {
                    controlsum++;
                }
            }
            if (controlsum % 2 != 0)
            {
                bit_array[0] = true;
            }
            controlsum = 0;
            for (int i = 1; i < 21; i += 3)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (bit_array[i + k])
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
                    if (k + i < 21 && bit_array[i + k])
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
            for (int i = 15; i < 21; i++)
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
        public BitArray hammingBlockDecoder(BitArray bits)
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
            bit_array = hammingBlockCoder(bans);

            int index_of_error = 0;
            if (bits[0] != bit_array[0])
            {
                index_of_error++;
            }
            for (int i = 2; i < 21; i = i * 2)
            {
                if (bits[i - 1] != bit_array[i - 1])
                {
                    index_of_error += i;
                }
            }
            if (index_of_error != 0)
            {
                bit_array[index_of_error - 1] = !bit_array[index_of_error - 1];
            }
            BitArray ans = new BitArray(16);
            int k = 0;
            for (int i = 0; i < 21; i++)
            {
                if (i != 0 && i != 1 && i != 3 && i != 7 && i != 15)
                {
                    ans[k] = bit_array[i];
                    k++;
                }
            }

            return ans;
        }
        public BitArray hammingCoder(BitArray input_bits)
        {
            BitArray help_bits = new BitArray(input_bits.Length + (input_bits.Length % 16));
            for(int i = 0; i < input_bits.Length; i++)
            {
                help_bits[i] = input_bits[i];
            }
            BitArray ans_bits = new BitArray((help_bits.Length / 16) * 21);
            int index_in_ans = 0;
            for(int i = 0; i < help_bits.Length; i += 16)
            {
                BitArray b = new BitArray(16);
                for(int j = 0; j < 16; j++)
                {
                    b[j] = help_bits[i + j];
                }
                b = hammingBlockCoder(b);
                for(int j = 0; j < 21; j++)
                {
                    ans_bits[index_in_ans] = b[j];
                    index_in_ans++;
                }
            }
            return ans_bits;
        }
        public BitArray hammincDecoder(BitArray input_bits)
        {
            BitArray ans_bits = new BitArray((input_bits.Length / 21) * 16);
            int index_in_ans = 0;
            for (int i = 0; i < input_bits.Length; i += 21)
            {
                BitArray b = new BitArray(21);
                for(int j = 0; j < 21; j++)
                {
                    b[j] = input_bits[i + j];
                }
                b = hammingBlockDecoder(b);
                for(int j = 0; j < 16; j++)
                {
                    ans_bits[index_in_ans] = b[j];
                    index_in_ans++;
                }
            }
            return ans_bits;
        }
        public string bitarraytoprint(BitArray bits)
        {
            string ans = "";
            foreach (bool item in bits)
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
        public List<BitArray> CodingToWrite(BitArray input_bits)
        {
            int block_len = input_bits.Length / 3;
            List<BitArray> ans = new List<BitArray>();
            BitArray b1 = new BitArray(block_len);
            BitArray b2 = new BitArray(block_len);
            BitArray b3 = new BitArray(block_len);

            int index_in_input_bits = 0;
            for(int i = 0; i < block_len; i++)
            {
                b1[i] = input_bits[index_in_input_bits + i];
            }
            index_in_input_bits += block_len;
            for(int i = 0; i < block_len; i++)
            {
                b2[i] = input_bits[index_in_input_bits + i];
            }
            index_in_input_bits += block_len;
            for (int i = 0; i < block_len; i++)
            {
                b3[i] = input_bits[index_in_input_bits + i];
            }
            b1 = hammingCoder(b1);
            b2 = hammingCoder(b2);
            b3 = hammingCoder(b3);
            ans.Add(b1);
            ans.Add(b2);
            ans.Add(b3);
            return ans;
        }

        public BitArray case0(BitArray input_bits)
        {
            int block_len = input_bits.Length / 3;
            BitArray b1 = new BitArray(block_len);
            BitArray b2 = new BitArray(block_len);
            BitArray b3 = new BitArray(block_len);
            int index_in_input_bits = 0;
            for (int i = 0; i < block_len; i++)
            {
                b1[i] = input_bits[index_in_input_bits + i];
            }
            index_in_input_bits += block_len;
            for (int i = 0; i < block_len; i++)
            {
                b2[i] = input_bits[index_in_input_bits + i];
            }
            b1 = hammincDecoder(b1);
            b2 = hammincDecoder(b2);
            BitArray ans = new BitArray(b1.Length + b2.Length);
            int index_in_ans = 0;
            foreach (bool item in b1)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            foreach (bool item in b2)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            return ans;
        }
        public BitArray case3(BitArray input_bits)
        {
            int block_len = input_bits.Length / 2;
            BitArray b1 = new BitArray(block_len);
            BitArray b2 = new BitArray(block_len);
            int index_in_input_bits = 0;
            for (int i = 0; i < block_len; i++)
            {
                b1[i] = input_bits[index_in_input_bits + i];
            }
            index_in_input_bits += block_len;
            for (int i = 0; i < block_len; i++)
            {
                b2[i] = input_bits[index_in_input_bits + i];
            }
            b1 = hammincDecoder(b1);
            b2 = hammincDecoder(b2);
            BitArray ans = new BitArray(b1.Length + b2.Length);
            int index_in_ans = 0;
            foreach (bool item in b1)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            foreach (bool item in b2)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            return ans;
        }
        public BitArray case1(BitArray input_bits)
        {
            int block_len = input_bits.Length / 2;
            BitArray b2 = new BitArray(block_len);
            BitArray b3 = new BitArray(block_len);
            int index_in_input_bits = 0;
            for (int i = 0; i < block_len; i++)
            {
                b2[i] = input_bits[index_in_input_bits + i];
            }
            index_in_input_bits += block_len;
            for (int i = 0; i < block_len; i++)
            {
                b3[i] = input_bits[index_in_input_bits + i];
            }
            b2 = hammincDecoder(b2);
            b3 = hammincDecoder(b3);
            BitArray b1 = new BitArray(b2.Length);
            for(int i = 0; i< b3.Length; i++)
            {
                if (b3[i])
                {
                    b1[i] = !b2[i];
                }
                else
                {
                    b1[i] = b2[i];
                }
            }
            BitArray ans = new BitArray(b1.Length + b2.Length);
            int index_in_ans = 0;
            foreach (bool item in b1)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            foreach (bool item in b2)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            return ans;
        }
        public BitArray case2(BitArray input_bits)
        {
            int block_len = input_bits.Length / 2;
            BitArray b1 = new BitArray(block_len);
            BitArray b3 = new BitArray(block_len);
            int index_in_input_bits = 0;
            for (int i = 0; i < block_len; i++)
            {
                b1[i] = input_bits[index_in_input_bits + i];
            }
            index_in_input_bits += block_len;
            for (int i = 0; i < block_len; i++)
            {
                b3[i] = input_bits[index_in_input_bits + i];
            }
            b1 = hammincDecoder(b1);
            b3 = hammincDecoder(b3);
            BitArray b2 = new BitArray(b1.Length);
            for (int i = 0; i < b3.Length; i++)
            {
                if (b3[i])
                {
                    b2[i] = !b1[i];
                }
                else
                {
                    b2[i] = b1[i];
                }
            }
            BitArray ans = new BitArray(b1.Length + b2.Length);
            int index_in_ans = 0;
            foreach (bool item in b1)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            foreach (bool item in b2)
            {
                ans[index_in_ans] = item;
                index_in_ans++;
            }
            return ans;
        }
        public BitArray fromRead(BitArray input_bits,int errorcoder =0)
        {
            switch (errorcoder)
            {
                case 0:
                    return case0(input_bits);
                    break;
                case 1:
                    return case1(input_bits);
                    break;
                case 2:
                    return case2(input_bits);
                    break;
                case 3:
                    return case3(input_bits);
                    break;
                default:
                    return case0(input_bits);
                    break;
            }
        }
        public byte[] bitArrayConvertor(BitArray input_bits)
        {
            BitArray a = new BitArray(input_bits.Length + (input_bits.Length & 8));
            for(int i = 0; i < input_bits.Length; i++)
            {
                a[i] = input_bits[i];
            }
            byte[] bytes = new byte[a.Length / 8];
            a.CopyTo(bytes, 0);
            //Array.Reverse(bytes);
            return bytes;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
                
        }

        private void button7_Click(object sender, EventArgs e)
        {
            byte[] str_bin = System.Text.Encoding.UTF8.GetBytes("konlox");
            BitArray bits = new BitArray(str_bin);
            byte[] bytes = bitArrayConvertor(bits);
            richTextBox2.Text = bitarraytoprint(bits);
            richTextBox1.Text = bitarraytoprint(hammincDecoder(hammingCoder(bits)));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //string str1 = richTextBox1.Text;
        //    //byte[] str_bin = System.Text.Encoding.UTF8.GetBytes(str1);
        //    //BitArray bits = new BitArray(str_bin);
        //    //richTextBox2.Text = bitarraytoprint(addbit(bits));

        //    //richTextBox2.Text = bitarraytoprint(mainHammingEncoder(mainHammingCoder(richTextBox1.Text)));
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    string str1 = richTextBox2.Text;
        //    //byte[] str_bin = System.Text.Encoding.UTF8.GetBytes(str1);
        //    BitArray bits = new BitArray(str1.Length);
        //    for (int i = 0; i < str1.Length; i++)
        //    {
        //        if (str1[i] == '1')
        //        {
        //            bits[i] = true;
        //        }
        //        else
        //        {
        //            bits[i] = false;
        //        }
        //    }


        //    //код хемминга



        //    richTextBox1.Text = bitarraytoprint(hammingDecoder(bits));

        //}


    }
}
