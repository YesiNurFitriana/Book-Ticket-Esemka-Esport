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
    public partial class CreateAccountForm : Form
    {
        private readonly EsemkaEsportEntities db=new EsemkaEsportEntities();
        bool gender;
        public CreateAccountForm()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                gender = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                gender = false;
            }
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            string username=textBox1.Text;
            string password=textBox2.Text;
            DateTime datebirth = dateTimePicker1.Value;
            
            if(db.users.Any(x => x.username == username))
            {
                Alert.error("username telah dipakai");
                return;
            }
            else if (textBox1.Text == string.Empty)
            {
                Alert.error("Silahkan isi username terlebih dahulu");
                return ;
            }
            else if (textBox2.Text == string.Empty)
            {
                Alert.error("Silahkan isi password terlebih dahulu");
                return ;
            }
            else if (textBox2.Text.Length < 6)
            {
                Alert.error("Password minimal 6 karakter");
                return ;
            }
            else if (textBox3.Text == string.Empty)
            {
                Alert.error("Silahkan isi konfirmasi password terlebih dahulu");
                return ;
            }
            else if (textBox2.Text != textBox3.Text)
            {
                Alert.error("Password dan Retype Password harus sama");
                return ;
            }
            bool role = true;
            var data = new user()
            {
                username = username,
                password = password,
                birthdate = datebirth,
                gender = gender,
                Role = role,
                created_at = DateTime.Now,
            };
            db.users.Add(data);
            db.SaveChanges();
            Alert.info("Data berhasil disimpan");
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {
            gender = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.ShowDialog();
        }
    }
}
