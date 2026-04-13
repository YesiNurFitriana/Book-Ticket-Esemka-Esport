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
    public partial class BookForm : Form
    {
        private readonly EsemkaEsportEntities db= new EsemkaEsportEntities();
        int? selectedId;

        int? teamId;

        int? homeId;
        int? awayId;
        int? totaltiket = 60;
        int? totalNew;
        int? IdnyaUser;
        public BookForm(int? scheduleId,int? userId)
        {
            InitializeComponent();
            teamId = scheduleId;
            IdnyaUser = userId;
        }
        public BookForm()
        {
            InitializeComponent();
           
        }
        private void TiketRemaining()
        {
            var data = db.schedule_detail.Where(x=>x.schedule_id==teamId).Sum(x=>(int?)x.total_ticket);
            if (data != null)
            {
                totalNew = totaltiket - data;
                label8.Text = totalNew.ToString();
            }
            else
            {
                totalNew = 60;
                label8.Text = totalNew.ToString();
            }
          
            
        }
        private void BookForm_Load(object sender, EventArgs e)
        {
            var data = db.schedules.FirstOrDefault(x => x.id == teamId);
            if (data != null)
            {
                homeId=data.home_team_id;
                awayId=data.away_team_id;
            }
            else
            {
                Alert.error("data kosong");
            }

            label10.Text=teamId.ToString();

            
            TiketRemaining();
            showAway();
            showHome();
        }
        private void showHome()
        {
            var data=db.teams.FirstOrDefault(x=>x.id == homeId);
            label1.Text=data.team_name;
            label2.Text=data.company_name;
            var data1 = (from n in db.team_detail
                         join p in db.players on n.player_id equals p.id
                         where n.team_id == homeId
                         select new
                         {
                             Nickname = p.name
                         }).ToList();
            dataGridView1.DataSource = data1;
            dataGridView1.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void showAway()
        {
            var data = db.teams.FirstOrDefault(x => x.id == awayId);
            label3.Text = data.team_name;
            label4.Text = data.company_name;
            var data1 = (from n in db.team_detail
                         join p in db.players on n.player_id equals p.id
                         where n.team_id == awayId
                         select new
                         {
                             Nickname = p.name
                         }).ToList();
            dataGridView2.DataSource = data1;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            CustomersForm customersForm = new CustomersForm(selectedId);
            this.Hide();
            customersForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (totalNew == 0)
            {
                Alert.error("Maaf tiket untuk pertandingan tim ini telah habis");
            }
            else
            {
                int IDschedule = (int)teamId;
                int IDuser = (int)IdnyaUser;
                int buytiket= (int)numericUpDown1.Value;
                var data = new schedule_detail()
                {
                    schedule_id = IDschedule,
                    user_id = IDuser,
                    total_ticket = buytiket,
                    created_at = DateTime.Now,
                };
                db.schedule_detail.Add(data);
              
                db.SaveChanges();
                Alert.info("pembelian tiket berhasil");
                numericUpDown1.Value = 1;
                TiketRemaining();

                //var pembelian = db.schedule_detail
                //          .Where(x => x.user_id == IDuser)
                //          .ToList();
                var dataid=data.id;
                MyForm myForm = new MyForm(dataid);
                this.Hide();
                myForm.Show();
            }
        }
    }
}
