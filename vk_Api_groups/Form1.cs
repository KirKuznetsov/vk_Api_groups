using System;
using System.Windows.Forms;

namespace vk_Api_groups
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Auth();
        }

        public void Auth()
        {
            //id приложения
            const string appid = "5090732";
            //права доступа
            const string scope = "friends";
            const string url = "http://api.vk.com/oauth/authorize?client_id=" + appid + "&scope=" + scope +
                               "&redirect_uri=http://api.vkontakte.ru/blank.html&display=popup&response_type=token";
            
            var f2 = new Form2();
            f2.Show();
            var browser = (WebBrowser) f2.Controls["webBrowser1"];
            browser.Navigate(url);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            var getterPosts = new GetterTopPosts(Settings1.Default.token, idTextBox.Text);
            getterPosts.FindTop10Posts();
        }
    }
}