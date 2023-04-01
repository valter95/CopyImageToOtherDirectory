using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteCopiaDeimagemParaDiretorio
{
    public partial class frmTesteImagem : Form
    {
        private string caminhoDestino, caminhoBase, extensaoArquivo;

        public frmTesteImagem()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text)) 
            {
                MessageBox.Show("Você deve preencher o campo nome do produto para prosseguir com a ação", "Aviso");
                return;
            }
            caminhoDestino = ($"{Application.StartupPath}\\ImagensProduto");
            
            OpenFileDialog openFile = new OpenFileDialog()
            {

                Filter = "Arquivos .jpg e .png|*.jpg; *.png",
                Title = "Selecione o arquivo de imagem"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                caminhoBase = openFile.FileName;
                pictureBox1.Load(caminhoBase);
                txtImagemOriginal.Text = caminhoBase;

                if (!Directory.Exists(caminhoDestino))
                {
                    Directory.CreateDirectory(caminhoDestino);
                }
                extensaoArquivo = Path.GetExtension(caminhoBase);
                caminhoDestino = Path.Combine($"{caminhoDestino}\\{textBox1.Text}{DateTime.Now.ToString("yyyyMMddHHmmss")}{extensaoArquivo}");
                File.Copy(caminhoBase, caminhoDestino);

                txtCaminhoDestino.Text = caminhoDestino;
                pictureBox2.Load(caminhoDestino);
            }

        }
    }
}
