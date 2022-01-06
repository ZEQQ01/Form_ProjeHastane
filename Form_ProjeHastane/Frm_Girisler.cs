using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_ProjeHastane
{
    public partial class Frm_Girisler : Form
    {
        public Frm_Girisler()
        {
            InitializeComponent();
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Frm_HastaGiris _HastaGiris = new Frm_HastaGiris();
            _HastaGiris.Show();
            this.Hide();
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            Frm_DoktorGiris _DoktorGiris = new Frm_DoktorGiris();
            _DoktorGiris.Show();
            this.Hide();
        }

        private void btnSekreter_Click(object sender, EventArgs e)
        {
            Frm_SekreterGiris _SekreterGiris = new Frm_SekreterGiris();
            _SekreterGiris.Show();
            this.Hide();
        }
    }
}
