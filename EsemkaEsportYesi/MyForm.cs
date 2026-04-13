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
    public partial class MyForm : Form
    {
        private readonly EsemkaEsportEntities db=new EsemkaEsportEntities();
        int? id;
        public MyForm(int? data)
        {
            InitializeComponent();
            id=data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BookForm book=new BookForm();
            this.Hide();
            book.Show();
        }
        private void showdata()
        {
            var data = (from n in db.schedule_detail 
                        join s in db.schedules on n.schedule_id equals s.id
                        join h in db.teams on s.home_team_id equals h.id
                        join a in db.teams on s.away_team_id equals a.id
                        where n.id == id
                        select new
                        {
                            n.id,
                            Match = h.team_name + " VS " + a.team_name,
                            Time = s.time,
                            TotalTicket = n.total_ticket
                        }).ToList();
            dataGridView1.DataSource = data;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!dataGridView1.Columns.Contains("edit"))
            {
                DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn()
                {
                    Name = "print",
                    Text = "Print",
                    HeaderText = "",
                    UseColumnTextForLinkValue = true
                };
                dataGridView1.Columns.Add(linkColumn);
            }
        }
        private void MyForm_Load(object sender, EventArgs e)
        {
            showdata();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            id = (int)row.Cells["id"].Value;


            var data = (from n in db.schedule_detail
                        join s in db.schedules on n.schedule_id equals s.id
                        join h in db.teams on s.home_team_id equals h.id
                        join a in db.teams on s.away_team_id equals a.id
                        where n.id == id
                        select new
                        {
                            n.id,
                            HomeTeam = h.team_name ,
                            AwayTeam=a.team_name ,
                            time = s.time,
                            TotalTicket = n.total_ticket
                        }).FirstOrDefault();
            if (e.ColumnIndex == dataGridView1.Columns["print"].Index)
            {
                string tiketInfo =
            "--------------------------------------\n" +
            $"{data.HomeTeam} vs {data.AwayTeam}\n" +
            $"Time : {data.time:dddd, dd MMMM yyyy (HH:mm)}\n" +
            $"Total Ticket : {data.TotalTicket}\n" +
            "--------------------------------------";

                MessageBox.Show(tiketInfo, "Tiket Anda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
