using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteCopiaDeimagemParaDiretorio
{
    public partial class frmTelaInicial : Form
    {
        public frmTelaInicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmTesteImagem TestImagem = new frmTesteImagem();
            TestImagem.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmAjusteDeImagem AjustImg = new FrmAjusteDeImagem();
            AjustImg.Show();
        }
    }
}
