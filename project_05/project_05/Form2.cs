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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (DataManager.Users.Exists(x => x.Id == textBox1.Text))
                {
                    MessageBox.Show("이미 존재하는 회원입니다.");
                }
                else
                {
                    User user = new User()
                    {
                        Id = textBox1.Text,
                        Pwd = int.Parse(textBox2.Text)
                    };
                    DataManager.Users.Add(user);
                    MessageBox.Show("회원가입 완료! 로그인 창에서 로그인 해주세요.");
                    this.Hide();
                    new Form1().ShowDialog();
                }
                DataManager.Save();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
