using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace MyCharFolder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString() == "https://www.ff14.co.kr/login/login/?GSPath=90688CE52E1192414EA0FD276133DA3C&ReturnUrl=F56677026F789CCAE7D763AC0ED05C8F")
            {
                HtmlElement he = webBrowser1.Document.CreateElement("script");
                HtmlElement st = webBrowser1.Document.CreateElement("style");
                he.InnerText = "function windowFixed() {$('#loginForm>fieldset>.inp_wrap').css({'position':'fixed', 'left':'0px', 'top':'0px', 'background':'#030D1D', 'margin':'0px', 'padding':'0px', 'width':'530px', 'height':'330px', 'color':'#FFF', 'display':'block', 'padding-top':'40px', 'padding-left':'40px'}); $('#loginForm>fieldset>.inp_wrap>.btn_login').css({'position':'absolute', 'top':'40px', 'right':'134px'});} $('#loginForm>fieldset>.inp_wrap input').css({'background':'#1D2634', 'color':'#FFF', 'border-color':'transparent'}); $('#loginForm>fieldset>.inp_wrap dt>label').css({'color':'#FFF'}); $('#memberID').attr('placeholder', '아이디를 입력하세요'); $('#passWord').attr('placeholder', '비밀번호를 입력하세요');";
                st.InnerText = "#memberID::-ms-input-placeholder, #passWord::-ms-input-placeholder {color:#616871;}";
                webBrowser1.Document.Body.AppendChild(st);
                webBrowser1.Document.Body.AppendChild(he);
                webBrowser1.Document.InvokeScript("windowFixed");
            }
            else if (webBrowser1.Url.ToString() == "https://www.ff14.co.kr/login/?GSPath=90688CE52E1192414EA0FD276133DA3C&ReturnUrl=F56677026F789CCAE7D763AC0ED05C8F")
            {
                webBrowser1.Navigate("https://www.ff14.co.kr/login/login/?GSPath=90688CE52E1192414EA0FD276133DA3C&ReturnUrl=F56677026F789CCAE7D763AC0ED05C8F");
            }
            else if (webBrowser1.Url.ToString() == "https://www.ff14.co.kr/shop/myshop")
            {
                dataGridView1.Visible = true;
                webBrowser1.Navigate("https://www.ff14.co.kr/shop/home/detail/34");
            }
            else if (webBrowser1.Url.ToString() == "https://www.ff14.co.kr/shop/home/detail/34")
            {
                string item = webBrowser1.Document.GetElementById("addOption").OuterHtml;
                webBrowser1.Visible = false;
                //MessageBox.Show(item);
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(item);

                foreach(XmlElement xe in xd.SelectSingleNode("/*").ChildNodes)
                {
                    string[] i = xe.GetAttribute("value").Split('/');
                    try
                    {
                        string serv = "모그리";
                        if (i[1].StartsWith("KrCh"))
                            serv = "초코보";
                        else if (i[1].StartsWith("KrCa"))
                            serv = "카벙클";

                        long code = Convert.ToInt64(i[0]);

                        string f = "FFXIV_CHR00" + code.ToString("X");
                        string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\FINAL FANTASY XIV - KOREA", f);
                        string open = "";
                        string zip = "";

                        if (Directory.Exists(p))
                        {
                            open = "열기";
                            zip = "압축";
                        }


                        dataGridView1.Rows.Add(new string[] { open, zip, f, i[2], serv });
                        //listView1.Items.Add(new ListViewItem(new string[] { "FFXIV_CHR00"+code.ToString("X"), i[2], serv }));
                    }
                    catch { }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView d = (DataGridView)sender;
            if (e.ColumnIndex == 0)
            {
                string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games\\FINAL FANTASY XIV - KOREA", d.Rows[e.RowIndex].Cells[2].Value.ToString());

                Console.WriteLine(p);

                if (Directory.Exists(p))
                {
                    Process.Start(p);
                }
            }
        }
    }
}
