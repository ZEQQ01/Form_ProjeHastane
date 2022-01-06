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
    public partial class Frm_SekreterDetay : Form
    {
        public Frm_SekreterDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void Frm_SekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            //Ad Soyad
            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Bransları Datagride Aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Brans", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Doktorları Datagride Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar' , DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;

            //Bransı combobox'a yazdırma
            SqlCommand komut1 = new SqlCommand("Select BransAd from Tbl_Brans", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                ctxtBrans.Items.Add(dr1[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor, RandevuDurum) values (@p1, @p2, @p3, @p4, @p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mtxtTarih.Text);
            komut.Parameters.AddWithValue("@p2", mtxtSaat.Text);
            komut.Parameters.AddWithValue("@p3", ctxtBrans.Text);
            komut.Parameters.AddWithValue("@p4", ctxtDoktor.Text);
            komut.Parameters.AddWithValue("@p5", cbDurum.Checked);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen Gereken Bilgileri Giriniz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@p1)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", richTextBox1.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru Oluşturuldu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            Frm_DoktorPaneli _DoktorPaneli = new Frm_DoktorPaneli();
            _DoktorPaneli.Show();
        }

        private void btnBranşPaneli_Click(object sender, EventArgs e)
        {
            Frm_Brans _Brans = new Frm_Brans();
            _Brans.Show();
        }

        private void btnRandevuListesi_Click(object sender, EventArgs e)
        {
            Frm_RandevuListesi _RandevuListesi = new Frm_RandevuListesi();
            _RandevuListesi.Show();
        }

        private void btnDuyuruListesi_Click(object sender, EventArgs e)
        {
            Frm_Duyurular _Duyurular = new Frm_Duyurular();
            _Duyurular.Show();
        }
    }
}
