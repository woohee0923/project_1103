using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form2().ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {

            }
            else if (textBox2.Text.Trim() == "")
            {

            }
            else
            {
                try
                {
                    User user = DataManager.Users.Single(x => x.Id == textBox1.Text);
                    String id = user.Id;
                    int pwd = user.Pwd;
                    int count = user.Count;
                    Login.login_id = id;
                    Login.login_count = count;
                    Login.result_count = count;

                    if (id == textBox1.Text && pwd == int.Parse(textBox2.Text))
                    {
                        MessageBox.Show("로그인 성공!");
                        this.Hide();
                        new GuessTheWord.Form6().ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("로그인 실패! 비밀번호가 틀립니다.");
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("로그인 실패! 아이디가 존재하지 않습니다.");
                }
            }
        }
    }
}
