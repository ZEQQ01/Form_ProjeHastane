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
    public partial class Frm_HastaDetay : Form
    {
        public Frm_HastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void Frm_HastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            
            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("Select HastaAd, HastaSoyad From Tbl_Hastalar Where HastaTC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where HastaTC = " + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Çekme
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Brans", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                ctxtBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void ctxtBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            ctxtDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar where DoktorBrans = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", ctxtBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ctxtDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void ctxtDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans = '" + ctxtBrans.Text + "'" + "and RandevuDoktor = '" + ctxtDoktor.Text + "' and RandevuDurum = 0" , bgl.baglanti());
             da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_HastaBilgiDuzenle _HastaBilgiDuzenle = new Frm_HastaBilgiDuzenle();
            _HastaBilgiDuzenle.TcNO = tc;
            _HastaBilgiDuzenle.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtİD.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular Set RandevuDurum = 1, HastaTC = @p1, HastaSikayet = @p2 where RandevuİD = @p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            komut.Parameters.AddWithValue("@p2", rtxtSikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtİD.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
