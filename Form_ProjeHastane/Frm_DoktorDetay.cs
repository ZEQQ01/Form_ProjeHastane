using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Form_ProjeHastane
{
    public partial class Frm_DoktorDetay : Form
    {
        public Frm_DoktorDetay()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        public string tc;

        private void Frm_DoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            //Label'e Ad Soyad Ekleme
            SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar Where DoktorTC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuDoktor = '" + lblAdSoyad.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            Frm_DoktorBilgiDuzenle _DoktorBilgiDuzenle = new Frm_DoktorBilgiDuzenle();
            _DoktorBilgiDuzenle.TcNO = tc;
            _DoktorBilgiDuzenle.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            Frm_Duyurular _Duyurular = new Frm_Duyurular();
            _Duyurular.Show();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rtxtSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
