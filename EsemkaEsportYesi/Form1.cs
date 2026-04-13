using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaEsportYesi
{
    public partial class Form1 : Form
    {
        private readonly EsemkaEsportEntities db=new EsemkaEsportEntities();
        int? selectedId;

        public Form1()
        {
            InitializeComponent();
        }
        private bool validasi()
        {
            if (textBox1.Text == string.Empty)
            {
                Alert.error("Isi username terlebih dahulu");
            }
            else if (textBox2.Text == string.Empty)
            {
                Alert.error("Isi password terlebih dahulu");
            }
            
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (validasi())
            {
                string username=textBox1.Text;
                string password=textBox2.Text;
                var data = db.users.FirstOrDefault(x => x.username == username && x.password == password);
                if (data != null)
                {
                    selectedId = data.id;
                    CustomersForm form = new CustomersForm(selectedId);
                    this.Hide();
                    form.ShowDialog();
                }
                else
                {
                    Alert.error("Data tidak valid");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBox2.UseSystemPasswordChar = true;
            }
            else if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar= true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateAccountForm form=new CreateAccountForm();
            form.ShowDialog(this);
        }
    }
}
