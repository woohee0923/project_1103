using GuessTheWord;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Results;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            new Form6().ShowDialog();
        }
    }
}
