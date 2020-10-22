using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LICENSE_GEN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("쿠팡");
            comboBox1.Items.Add("네이버");
            comboBox1.SelectedIndex = 0;
            
        }

        //AES_128 암호화
        public String AESEncrypt128(String Input, String key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(Input);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);


            return EncryptedData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (comboBox1.SelectedIndex == 0)
            {
                sb.Append("CPM^");
            }
            else {
                sb.Append("NVM^");
            }
            sb.Append(textBox2.Text.Replace("-", ""));
            sb.Append("^");
            sb.Append(dateTimePicker1.Value.ToString("yyyyMMdd") + "235959");
            //textBox1.Text = sb.ToString();
            textBox1.Text = AESEncrypt128( sb.ToString(), "rg9gDHJtjfpuJ4FZ");
        }
    }
}
