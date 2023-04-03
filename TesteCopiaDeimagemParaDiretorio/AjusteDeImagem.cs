using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteCopiaDeimagemParaDiretorio
{
    public partial class FrmAjusteDeImagem : Form
    {
        string caminhoDestino, caminhoBase, extensaoArquivo;
        int altura, largura;
        Stream NovaImagemStream = null;

        private void AjusteDeImagem_Load(object sender, EventArgs e)
        {

        }

        public FrmAjusteDeImagem()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtNomeDaImagem.Text))
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
                //textbox1.Text = caminhoBase;

                if (!Directory.Exists(caminhoDestino))
                {
                    Directory.CreateDirectory(caminhoDestino);
                }
                extensaoArquivo = Path.GetExtension(caminhoBase);
                caminhoDestino = Path.Combine($"{caminhoDestino}\\{txtNomeDaImagem.Text}{DateTime.Now.ToString("yyyyMMddHHmmss")}{extensaoArquivo}");
                File.Copy(caminhoBase, caminhoDestino);

                
                largura = Convert.ToInt32(txtLArgura.Text);
                altura = Convert.ToInt32(txtAltura.Text);

                if ((NovaImagemStream = openFile.OpenFile()) != null)
                {
                    using (NovaImagemStream)
                    {
                        var image = Image.FromStream(NovaImagemStream);

                        var NovaImagemRedimencionada = ResizeImage(image, largura, altura);
                        NovaImagemRedimencionada.Save(caminhoDestino, ImageFormat.Png);
                    }
                }
                txtCaminhoDestino.Text = caminhoDestino;
                pictureBox2.Load(caminhoDestino);
            }

        }
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
