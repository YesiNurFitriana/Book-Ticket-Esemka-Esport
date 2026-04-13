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
    public partial class MyTicketForm : Form
    {
        private readonly EsemkaEsportEntities db= new EsemkaEsportEntities();
        int? selectedId;
        public MyTicketForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookForm book=new BookForm();
            this.Hide();
            book.Show();
        }
    }
}
