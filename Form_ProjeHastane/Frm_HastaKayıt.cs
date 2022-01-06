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
    public partial class Frm_HastaKayıt : Form
    {
        public Frm_HastaKayıt()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btnKayıtYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd, HastaSoyad, HastaTC, HastaTelefon, HastaSifre, HastaCinsiyet) " +
                "values (@p1, @p2, @p3, @p4, @p5, @p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mtxtTC.Text);
            komut.Parameters.AddWithValue("@p4", mtxtTelefon.Text);
            komut.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut.Parameters.AddWithValue("@p6", ctxtCinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Hasta Kayıt İşlemi Tamamlandı!!");
            Temizle();
            
        }

        void Temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            mtxtTC.Text = "";
            mtxtTelefon.Text = "";
            txtSifre.Text = "";
            ctxtCinsiyet.Text = "";
        }
    }
}
