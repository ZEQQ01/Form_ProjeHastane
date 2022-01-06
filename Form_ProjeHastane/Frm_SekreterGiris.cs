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
    public partial class Frm_SekreterGiris : Form
    {
        public Frm_SekreterGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btnGiriş_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Sekreter where SekreterTC = @p1 and SekreterSifre = @p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mtxtTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dt = komut.ExecuteReader();
            if (dt.Read())
            {
                Frm_SekreterDetay _SekreterDetay = new Frm_SekreterDetay();
                _SekreterDetay.tc = mtxtTC.Text;
                _SekreterDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
            bgl.baglanti().Close();
        }
    }
}
