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
    public partial class CustomersForm : Form
    {
        private readonly EsemkaEsportEntities db=new EsemkaEsportEntities();
        int? selectedId;
        int? userId;
        public CustomersForm(int? userId2)
        {
            InitializeComponent();
            userId= userId2;
        }
        private void showTeam()
        {
            var data = (from n in db.schedules.AsEnumerable()
                        join h in db.teams on n.home_team_id equals h.id
                        join a in db.teams on n.away_team_id equals a.id
                        select new
                        {
                            n.id,
                            Match = h.team_name + " VS " + a.team_name,
                            Time = n.time.ToString("dddd, dd MMMM yyyy (HH:mm)")
                        }).OrderBy(x=>x.Time).ToList();
            dataGridView1.DataSource = data;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            if (!dataGridView1.Columns.Contains("Book"))
            {
                DataGridViewButtonColumn bookcolumn = new DataGridViewButtonColumn()
                {
                    Name = "book",
                    Text = "Book",
                    HeaderText = "",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(bookcolumn);
            }
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            showTeam();
            label2.Text=userId.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            selectedId = (int)row.Cells["Id"].Value;
            
            if (e.ColumnIndex == dataGridView1.Columns["book"].Index)
            {
                BookForm show=new BookForm(selectedId,userId);
                this.Hide();
                show.ShowDialog();
            }
        }
    }
}
